using System;

namespace ChipmunkBinding.Unsafe
{
    /// <summary>
    /// Unsafe extension methods for the <see cref="Polygon"/> shape.
    /// </summary>
    public static class PolygonExtensions
    {
        /// <summary>
        /// Set the vertexes of the polygon.
        /// </summary>
        public static void SetVertexes(this Polygon polygon, Vect [] vertexes, Transform transform)
        {
            IntPtr ptrVectors = NativeInterop.StructureArrayToPtr(vertexes);
            NativeMethods.cpPolyShapeSetVerts(polygon.Handle, vertexes.Length, ptrVectors, transform);
            NativeInterop.FreeStructure(ptrVectors);
        }

        /// <summary>
        /// Set the vertexes of the polygon.
        /// </summary>
        public static void SetVertexes(this Polygon polygon, Vect[] vertexes)
        {
            IntPtr ptrVectors = NativeInterop.StructureArrayToPtr(vertexes);
            NativeMethods.cpPolyShapeSetVertsRaw(polygon.Handle, vertexes.Length, ptrVectors);
            NativeInterop.FreeStructure(ptrVectors);
        }

        /// <summary>
        /// Set the radius of a poly shape 
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="radius"></param>
        public static void SetRadius(this Polygon polygon, double radius)
        {
            NativeMethods.cpPolyShapeSetRadius(polygon.Handle, radius);
        }
    }
}
