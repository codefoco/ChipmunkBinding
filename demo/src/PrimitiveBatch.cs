using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestMac
{
    /// <summary>
    /// Batcher that draws vertex colored triangles (from https://github.com/prime31/Nez/blob/a3229fa9d30898e48769f0a29ce3992fc14bcd6a/Nez.Portable/Graphics/PrimitiveBatch.cs )
    /// </summary>
    public class PrimitiveBatch : IDisposable
    {
        private BasicEffect basicEffect;
        private readonly GraphicsDevice graphicsDevice;

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

        public void LoadContent()
        {
            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.World = Matrix.Identity;
        }

        public void Begin()
        {
            var projection = Matrix.CreateOrthographicOffCenter(0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, 0, 0, -1);
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.Forward, Vector3.Up);

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

            triangleVertices.Clear();
        }

        public void DrawCircle(Vector2 center, float radius, Color color, int circleSegments = 32)
        {
            float increment = MathHelper.Pi * 2.0f / circleSegments;
            float theta = 0.0f;

            var v0 = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
            theta += increment;

            for (int i = 1; i < circleSegments - 1; i++)
            {
                var v1 = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                var v2 = center + radius * new Vector2((float)Math.Cos(theta + increment), (float)Math.Sin(theta + increment));

                AddTriangleVertex(v0, color);
                AddTriangleVertex(v1, color);
                AddTriangleVertex(v2, color);

                theta += increment;
            }
        }

        public void DrawPolygon(Vector2[] vertices, Color color)
        {
            int count = vertices.Length;

            Debug.Assert(count >= 3);

            Vector2 v0 = vertices[0];
            Vector2 v1 = vertices[1];
            Vector2 v2;

            for (var i = 2; i < count; i++)
            {
                v2 = vertices[i];

                AddTriangleVertex(v0, color);
                AddTriangleVertex(v1, color);
                AddTriangleVertex(v2, color);

                v1 = v2;
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
