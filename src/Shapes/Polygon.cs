
using System;

namespace ChipmunkBinding
{
    /// <summary>
    /// A Polygonal shape
    /// </summary>
    public class Polygon : Shape
    {
        /// <summary>
        /// Create and initialize a polygon shape with rounded corners.
        /// A convex hull will be created from the vertexes.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="radius"></param>
        public Polygon(Body body, Vect [] verts, Transform transform, double radius) :
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

        static private IntPtr CreatePolygonShape (Body body, Vect[] verts, Transform transform, double radius)
        {
            IntPtr ptrVectors = NativeInterop.StructureArrayToPtr(verts);

            IntPtr handle = NativeMethods.cpPolyShapeNew(body.Handle, verts.Length, ptrVectors, transform, radius);

            NativeInterop.FreeStructure(ptrVectors);

            return handle;
        }

        static private IntPtr CreatePolygonShape(Body body, Vect[] verts, double radius)
        {
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
        /// Get the radius of a polygon shape.
        /// </summary>
        public double Radius => NativeMethods.cpPolyShapeGetRadius(Handle);

    }
}
