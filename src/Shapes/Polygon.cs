
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChipmunkBinding
{
    /// <summary>
    /// A Polygonal shape
    /// </summary>
    public class Polygon : Shape
    {
        /// <summary>
        /// A convex polygon shape
        /// Slowest, but most flexible collision shape.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="verts"></param>
        /// <param name="transform"></param>
        /// <param name="radius"></param>
        public Polygon(Body body, IReadOnlyList<Vect> verts, Transform transform, double radius) :
            base(CreatePolygonShape(body, verts, transform, radius))
        {

        }

        /// <summary>
        /// Allocate and initialize a polygon shape with rounded corners.
        /// The vertexes must be convex with a counter-clockwise winding.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="verts"></param>
        /// <param name="radius"></param>
        public Polygon(Body body, Vect[] verts, double radius) :
            base(CreatePolygonShape(body, verts, radius))
        {

        }

        static private IntPtr CreatePolygonShape (Body body, IReadOnlyList<Vect> verts, Transform transform, double radius)
        {
            Debug.Assert(verts.Count > 2);

            IntPtr ptrVectors = NativeInterop.StructureArrayToPtr(verts);

            IntPtr handle = NativeMethods.cpPolyShapeNew(body.Handle, verts.Count, ptrVectors, transform, radius);

            NativeInterop.FreeStructure(ptrVectors);

            return handle;
        }

        static private IntPtr CreatePolygonShape(Body body, Vect[] verts, double radius)
        {
            Debug.Assert(verts.Length > 2);

            IntPtr ptrVectors = NativeInterop.StructureArrayToPtr(verts);

            IntPtr handle = NativeMethods.cpPolyShapeNewRaw(body.Handle, verts.Length, ptrVectors, radius);

            NativeInterop.FreeStructure(ptrVectors);

            return handle;
        }

        /// <summary>
        /// Get the number of verts in a polygon shape.
        /// </summary>
        public int Count => NativeMethods.cpPolyShapeGetCount(Handle);

        /// <summary>
        /// Get the  ith vertex of a polygon shape.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Vect GetVertex(int i)
        {
            return NativeMethods.cpPolyShapeGetVert(Handle, i);
        }

        /// <summary>
        /// Vertices of polygon
        /// </summary>
        public IReadOnlyList<Vect> Vertices
        {
            get
            {
                int count = Count;
                var vertices = new List<Vect>(Count);

                for (int i = 0; i < count; i++)
                    vertices.Add(GetVertex(i));

                return vertices;
            }
        }

        /// <summary>
        /// Get the radius of a polygon shape.
        /// </summary>
        public double Radius => NativeMethods.cpPolyShapeGetRadius(Handle);

        /// <summary>
        ///  Area of this polygon
        ///  This is probably backwards from what you expect, but matches Chipmunk's the winding for poly shapes
        /// </summary>

        public new double Area => AreaForPoly(Vertices, Radius);

        /// <summary>
        /// Centroid of this polygon
        /// </summary>
        public Vect Centroid => CentroidForPoly(Vertices);

        /// <summary>
        /// Calculate the moment of inertia for a solid polygon shape assuming it's center of gravity is at it's centroid. The offset is added to each vertex.
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="vertices"></param>
        /// <param name="offset"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static double MomentForPolygon(double mass, IReadOnlyList<Vect> vertices, Vect offset, double radius)
        {
            IntPtr verticesPtr = NativeInterop.StructureArrayToPtr(vertices);

            double moment = NativeMethods.cpMomentForPoly(mass, vertices.Count, verticesPtr, offset, radius);

            NativeInterop.FreeStructure(verticesPtr);

            return moment;
        }

        /// <summary>
        /// Calculate the signed area of a polygon. A Clockwise winding gives positive area.
        /// This is probably backwards from what you expect, but matches Chipmunk's the winding for poly shapes.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static double AreaForPoly(IReadOnlyList<Vect> vertices, double radius)
        {
            IntPtr verticesPtr = NativeInterop.StructureArrayToPtr(vertices);

            double area = NativeMethods.cpAreaForPoly(vertices.Count, verticesPtr, radius);

            NativeInterop.FreeStructure(verticesPtr);

            return area;
        }

        /// <summary>
        /// Calculate the natural centroid of a polygon.
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        public static Vect CentroidForPoly(IReadOnlyList<Vect> vertices)
        {
            IntPtr verticesPtr = NativeInterop.StructureArrayToPtr(vertices);

            Vect centroid = NativeMethods.cpCentroidForPoly(vertices.Count, verticesPtr);

            NativeInterop.FreeStructure(verticesPtr);

            return centroid;
        }
    }
}
