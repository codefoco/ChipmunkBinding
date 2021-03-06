﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChipmunkDemo
{
    /// <summary>
    /// Batcher that draws vertex colored triangles (from https://github.com/prime31/Nez/blob/a3229fa9d30898e48769f0a29ce3992fc14bcd6a/Nez.Portable/Graphics/PrimitiveBatch.cs )
    /// </summary>
    public class PrimitiveBatch : IDisposable
    {
        private BasicEffect basicEffect;
        private readonly GraphicsDevice graphicsDevice;
        private Matrix view;
        private Matrix projection;

        private readonly List<VertexPositionColor> lineVertices;
        private readonly List<VertexPositionColor> triangleVertices;

        private bool hasBegun;

        public PrimitiveBatch(GraphicsDevice device, int capacity)
        {
            graphicsDevice = device;
            lineVertices = new List<VertexPositionColor>(capacity - capacity % 3);
            triangleVertices = new List<VertexPositionColor>(capacity - capacity % 2);
        }

        public PrimitiveBatch(GraphicsDevice device)
        {
            graphicsDevice = device;
            lineVertices = new List<VertexPositionColor>();
            triangleVertices = new List<VertexPositionColor>();
        }

        public void LoadContent(ref Matrix world)
        {
            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.World = world;

            view = Matrix.CreateLookAt(Vector3.Zero, Vector3.Backward, Vector3.Down);
            projection = Matrix.CreateOrthographic(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, -1, 1);
        }

        public void LoadContent(ref Matrix world, ref Matrix v, ref Matrix p)
        {
            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.World = world;

            view = v;
            projection = p;
        }

        public void Begin()
        {
            Begin(ref projection, ref view);
        }


        public void Begin(ref Matrix projection, ref Matrix view)
        {
            Debug.Assert(!hasBegun, "Invalid state. End must be called before Begin can be called again.");

            // tell our basic effect to begin.
            basicEffect.CurrentTechnique.Passes[0].Apply();

            basicEffect.View = view;
            basicEffect.Projection = projection;

            // flip the error checking boolean. It's now ok to call AddVertex, Flush, and End.
            hasBegun = true;
        }

        public void End()
        {
            Debug.Assert(hasBegun, "Begin must be called before End can be called.");
            // Draw whatever the user wanted us to draw
            FlushTriangles();
            FlushLines();

            hasBegun = false;
        }

        public void AddLineVertex(Vector2 position, Color color)
        {
            var vpc = new VertexPositionColor(new Vector3(position, 0), color);
            lineVertices.Add(vpc);
        }

        public void AddTriangleVertex(Vector2 position, Color color)
        {
            var vpc = new VertexPositionColor(new Vector3(position, 0), color);
            triangleVertices.Add(vpc);
        }

        void FlushTriangles()
        {
            if (triangleVertices.Count < 3)
                return;

            int primitiveCount = triangleVertices.Count / 3;

            // submit the draw call to the graphics card
            graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleVertices.ToArray(), 0, primitiveCount);

            triangleVertices.Clear();
        }

        void FlushLines()
        {
            if (lineVertices.Count < 2)
                return;

            int primitiveCount = lineVertices.Count / 2;

            // submit the draw call to the graphics card
            graphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            graphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, lineVertices.ToArray(), 0, primitiveCount);

            lineVertices.Clear();
        }

        public void DrawDot(Vector2 center, float radius, Color color)
        {
            var top = new Vector2(center.X, center.Y - radius);
            var left = new Vector2(center.X - radius, center.Y);
            var bottom = new Vector2(center.X, center.Y + radius);
            var right = new Vector2(center.X + radius, center.Y);

            AddTriangleVertex(top, color);
            AddTriangleVertex(left, color);
            AddTriangleVertex(bottom, color);

            AddTriangleVertex(bottom, color);
            AddTriangleVertex(right, color);
            AddTriangleVertex(top, color);
        }


        public void DrawCircle(Vector2 center, float radius, Color color, Color? outline = null, int circleSegments = 32)
        {
            float increment = MathHelper.Pi * 2.0f / circleSegments;
            float theta = 0.0f;

            var v0 = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
            theta += increment;

            var v1 = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
            var v2 = center + radius * new Vector2((float)Math.Cos(theta + increment), (float)Math.Sin(theta + increment));

            if (outline.HasValue)
            {
                AddLineVertex(v0, outline.Value);
                AddLineVertex(v1, outline.Value);
            }

            for (int i = 1; i < circleSegments - 1; i++)
            {
                AddTriangleVertex(v0, color);
                AddTriangleVertex(v1, color);
                AddTriangleVertex(v2, color);

                if (outline.HasValue)
                {
                    AddLineVertex(v1, outline.Value);
                    AddLineVertex(v2, outline.Value);
                }

                v1 = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                v2 = center + radius * new Vector2((float)Math.Cos(theta + increment), (float)Math.Sin(theta + increment));

                theta += increment;
            }

            if (outline.HasValue)
            {
                AddLineVertex(v1, outline.Value);
                AddLineVertex(v2, outline.Value);

                AddLineVertex(v2, outline.Value);
                AddLineVertex(v0, outline.Value);
            }
        }

        public void DrawPolygon(Vector2[] vertices, Color color, Color? outline = null)
        {
            int count = vertices.Length;

            Debug.Assert(count >= 3);

            Vector2 v0 = vertices[0];
            Vector2 v1 = vertices[1];
            Vector2 v2;

            if (outline.HasValue)
            {
                AddLineVertex(v0, outline.Value);
                AddLineVertex(v1, outline.Value);
            }

            for (var i = 2; i < count; i++)
            {
                v2 = vertices[i];

                AddTriangleVertex(v0, color);
                AddTriangleVertex(v1, color);
                AddTriangleVertex(v2, color);

                if (outline.HasValue)
                {
                    AddLineVertex(v1, outline.Value);
                    AddLineVertex(v2, outline.Value);
                }

                v1 = v2;
            }

            if (outline.HasValue)
            {
                AddLineVertex(v1, outline.Value);
                AddLineVertex(v0, outline.Value);
            }
        }

        public void DrawLine(Vector2 pos1, Vector2 pos2, Color color)
        {
            AddLineVertex(pos1, color);
            AddLineVertex(pos2, color);
        }

        #region IDisposable Support
        private bool disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
                return;

            if (disposing && basicEffect != null)
            {
                basicEffect.Dispose();
                basicEffect = null;
            }

            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
