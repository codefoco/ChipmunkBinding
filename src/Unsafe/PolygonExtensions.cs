using System;

namespace ChipmunkBinding.Unsafe
{
    public static class PolygonExtensions
    {
        public static void SetVertexes(this Polygon polygon, Vect [] vertexes, Transform transform)
        {
            IntPtr ptrVectors = NativeInterop.StructureArrayToPtr(vertexes);

            NativeMethods.cpPolyShapeSetVerts(polygon.Handle, vertexes.Length, ptrVectors, transform);

            NativeInterop.FreeStructure(ptrVectors);
        }

        public static void SetVertexes(this Polygon polygon, Vect[] vertexes)
        {
            IntPtr ptrVectors = NativeInterop.StructureArrayToPtr(vertexes);

            NativeMethods.cpPolyShapeSetVertsRaw(polygon.Handle, vertexes.Length, ptrVectors);

            NativeInterop.FreeStructure(ptrVectors);
        }
    }
}
