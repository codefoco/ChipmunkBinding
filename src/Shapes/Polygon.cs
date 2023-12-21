// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2024 Codefoco LTDA - The above copyright notice and this permission notice shall be
//     included in all copies or substantial portions of the Software.
//
//     Redistribution and use in source and binary forms, with or without
//     modification, are permitted only if explicitly approved by the authors.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
//     EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//     OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//     NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//     HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
//     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//     FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//     OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChipmunkBinding
{
    /// <summary>
    /// A polygon shape composed of connected vertices.
    /// </summary>
    public class Polygon : Shape
    {
        /// <summary>
        /// A convex polygon shape. It's the slowest, but most flexible collision shape.
        /// </summary>
        public Polygon(Body body, IReadOnlyList<Vect> verts, Transform transform, double radius)
            : base(CreatePolygonShape(body, verts, transform, radius))
        {
        }

        /// <summary>
        /// Allocate and initialize a polygon shape with rounded corners.
        /// The vertexes must be convex with a counter-clockwise winding.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="verts"></param>
        /// <param name="radius"></param>
        public Polygon(Body body, Vect[] verts, double radius)
            : base(CreatePolygonShape(body, verts, radius))
        {

        }

        private static IntPtr CreatePolygonShape(Body body, IReadOnlyList<Vect> verts, Transform transform, double radius)
        {
            Debug.Assert(verts.Count > 2);

            IntPtr ptrVectors = NativeInterop.StructureArrayToPtr(verts);

            IntPtr handle = NativeMethods.cpPolyShapeNew(body.Handle, verts.Count, ptrVectors, transform, radius);

            NativeInterop.FreeStructure(ptrVectors);

            return handle;
        }

        private static IntPtr CreatePolygonShape(Body body, Vect[] verts, double radius)
        {
            Debug.Assert(verts.Length > 2);

            IntPtr ptrVectors = NativeInterop.StructureArrayToPtr(verts);

            IntPtr handle = NativeMethods.cpPolyShapeNewRaw(body.Handle, verts.Length, ptrVectors, radius);

            NativeInterop.FreeStructure(ptrVectors);

            return handle;
        }

        /// <summary>
        /// Get the number of vertices in a polygon shape.
        /// </summary>
        public int Count => NativeMethods.cpPolyShapeGetCount(Handle);

        /// <summary>
        /// Get the <paramref name="i"/>th vertex of a polygon shape.
        /// </summary>
        public Vect GetVertex(int i)
        {
            return NativeMethods.cpPolyShapeGetVert(Handle, i);
        }

        /// <summary>
        /// Get the vertices of the polygon.
        /// </summary>
        public IReadOnlyList<Vect> Vertices
        {
            get
            {
                int count = Count;
                if (count == 0)
                    return Array.Empty<Vect>();

                Vect[] vertices = new Vect[count];

                for (int i = 0; i < count; i++)
                {
                    vertices[i] = GetVertex(i);
                }

                return vertices;
            }
        }

        /// <summary>
        /// Get the radius of the polygon.
        /// </summary>
        public double Radius => NativeMethods.cpPolyShapeGetRadius(Handle);

        /// <summary>
        ///  Get and calculate the signed area of this polygon. Vertices specified such that they connect in
        ///  a clockwise fashion (called winding) give a positive area measurement. This is probably
        ///  backwards to what you might expect.
        /// </summary>

        public new double Area => AreaForPoly(Vertices, Radius);

        /// <summary>
        /// Get and calculate the centroid of the polygon.
        /// </summary>
        public Vect Centroid => CentroidForPoly(Vertices);

        /// <summary>
        /// Calculate the moment of inertia for a solid polygon shape assuming its center of gravity
        /// is at its centroid. The offset is added to each vertex.
        /// </summary>
        public static double MomentForPolygon(double mass, IReadOnlyList<Vect> vertices, Vect offset, double radius)
        {
            IntPtr verticesPtr = NativeInterop.StructureArrayToPtr(vertices);
            double moment = NativeMethods.cpMomentForPoly(mass, vertices.Count, verticesPtr, offset, radius);

            NativeInterop.FreeStructure(verticesPtr);

            return moment;
        }

        /// <summary>
        /// Calculate the signed area of this polygon. Vertices specified such that they connect in
        ///  a clockwise fashion (called winding) give a positive area measurement. This is probably
        ///  backwards to what you might expect.
        /// </summary>
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
        public static Vect CentroidForPoly(IReadOnlyList<Vect> vertices)
        {
            IntPtr verticesPtr = NativeInterop.StructureArrayToPtr(vertices);

            Vect centroid = NativeMethods.cpCentroidForPoly(vertices.Count, verticesPtr);

            NativeInterop.FreeStructure(verticesPtr);

            return centroid;
        }
    }
}