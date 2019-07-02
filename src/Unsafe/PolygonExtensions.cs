using System;

namespace ChipmunkBinding.Unsafe
{
    public static class PolygonExtensions
    {
        /// <summary>
        /// Set the vertexes of a poly shape.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="vertexes"></param>
        /// <param name="transform"></param>
        public static void SetVertexes(this Polygon polygon, Vect [] vertexes, Transform transform)
        {
            IntPtr ptrVectors = NativeInterop.StructureArrayToPtr(vertexes);

            NativeMethods.cpPolyShapeSetVerts(polygon.Handle, vertexes.Length, ptrVectors, transform);

            NativeInterop.FreeStructure(ptrVectors);
        }

        /// <summary>
        /// Set the vertexes of a poly shape.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="vertexes"></param>
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
