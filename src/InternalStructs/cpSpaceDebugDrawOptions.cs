// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2025 Codefoco LTDA - The above copyright notice and this permission notice shall be
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

using System.Runtime.InteropServices;

using cpDataPointer = System.IntPtr;
using cpShape = System.IntPtr;
using cpSpaceDebugDrawCircleImpl = System.IntPtr;
using cpSpaceDebugDrawColorForShapeImpl = System.IntPtr;
using cpSpaceDebugDrawDotImpl = System.IntPtr;
using cpSpaceDebugDrawFatSegmentImpl = System.IntPtr;
using cpSpaceDebugDrawFlags = System.Int32;
using cpSpaceDebugDrawPolygonImpl = System.IntPtr;
using cpSpaceDebugDrawSegmentImpl = System.IntPtr;
using cpVertPointer = System.IntPtr;
using voidptr_t = System.IntPtr;
// ReSharper disable InconsistentNaming

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
using ObjCRuntime;
#endif

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable IDE0055 // Some bug on VS

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]

    internal struct cpSpaceDebugDrawOptions
    {
        /// <summary>
        /// Function that will be invoked to draw circles.
        /// </summary>
        private cpSpaceDebugDrawCircleImpl drawCircle;

        /// <summary>
        /// Function that will be invoked to draw line segments.
        /// </summary>
        private cpSpaceDebugDrawSegmentImpl drawSegment;

        /// <summary>
        /// Function that will be invoked to draw thick line segments.
        /// </summary>
        private cpSpaceDebugDrawFatSegmentImpl drawFatSegment;

        /// <summary>
        /// Function that will be invoked to draw convex polygons.
        /// </summary>
        private cpSpaceDebugDrawPolygonImpl drawPolygon;

        /// <summary>
        /// Function that will be invoked to draw dots.
        /// </summary>
        private cpSpaceDebugDrawDotImpl drawDot;

        /// <summary>
        /// Flags that request which things to draw (collision shapes, constraints, contact points).
        /// </summary>
        private cpSpaceDebugDrawFlags flags;

        /// <summary>
        /// Outline color passed to the drawing function.
        /// </summary>
        private DebugColor shapeOutlineColor;

        /// <summary>
        /// Function that decides what fill color to draw shapes using.
        /// </summary>
        private cpSpaceDebugDrawColorForShapeImpl colorForShape;

        /// <summary>
        /// Color passed to drawing functions for constraints.
        /// </summary>

        private DebugColor constraintColor;

        /// <summary>
        /// Color passed to drawing functions for collision points.
        /// </summary>
        private DebugColor collisionPointColor;

        /// <summary>
        /// User defined context pointer passed to all of the callback functions as the 'data' argument.
        /// </summary>
        private cpDataPointer data;

        private cpVertPointer ToPointer()
        {
            cpVertPointer drawOptionsPtr = NativeInterop.AllocStructure<cpSpaceDebugDrawOptions>();

            Marshal.StructureToPtr(this, drawOptionsPtr, false);

            return drawOptionsPtr;
        }


#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(SpaceDebugDrawCircleImpl))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static void SpaceDebugDrawCircleCallback(Vect pos, double angle, double radius, DebugColor outlineColor, DebugColor fillColor, voidptr_t data)
        {
            IDebugDraw debugDraw = NativeInterop.FromIntPtr<IDebugDraw>(data);

            debugDraw.DrawCircle(pos, angle, radius, outlineColor, fillColor);
        }

        private static readonly SpaceDebugDrawCircleImpl spaceDebugDrawCircleCallback = SpaceDebugDrawCircleCallback;

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(SpaceDebugDrawSegmentImpl))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static void SpaceDebugDrawSegmentCallback(Vect a, Vect b, DebugColor color, voidptr_t data)
        {
            IDebugDraw debugDraw = NativeInterop.FromIntPtr<IDebugDraw>(data);

            debugDraw.DrawSegment(a, b, color);
        }

        private static readonly SpaceDebugDrawSegmentImpl spaceDebugDrawSegmentCallback = SpaceDebugDrawSegmentCallback;

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(SpaceDebugDrawFatSegmentImpl))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static void SpaceDebugDrawFatSegmentCallback(Vect a, Vect b, double radius, DebugColor outlineColor, DebugColor fillColor, voidptr_t data)
        {
            IDebugDraw debugDraw = NativeInterop.FromIntPtr<IDebugDraw>(data);

            debugDraw.DrawFatSegment(a, b, radius, outlineColor, fillColor);
        }

        private static readonly SpaceDebugDrawFatSegmentImpl spaceDebugDrawFatSegmentCallback = SpaceDebugDrawFatSegmentCallback;

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(SpaceDebugDrawPolygonImpl))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static void SpaceDebugDrawPolygonCallback(int count, cpVertPointer verts, double radius, DebugColor outlineColor, DebugColor fillColor, voidptr_t data)
        {
            IDebugDraw debugDraw = NativeInterop.FromIntPtr<IDebugDraw>(data);

            Vect[] vectors = NativeInterop.PtrToStructureArray<Vect>(verts, count);

            debugDraw.DrawPolygon(vectors, radius, outlineColor, fillColor);
        }

        private static readonly SpaceDebugDrawPolygonImpl spaceDebugDrawPolygonCallback = SpaceDebugDrawPolygonCallback;

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(SpaceDebugDrawDotImpl))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static void SpaceDebugDrawDotCallback(double size, Vect pos, DebugColor color, voidptr_t data)
        {
            IDebugDraw debugDraw = NativeInterop.FromIntPtr<IDebugDraw>(data);

            debugDraw.DrawDot(size, pos, color);
        }

        private static readonly SpaceDebugDrawDotImpl spaceDebugDrawDotCallback = SpaceDebugDrawDotCallback;

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(SpaceDebugDrawColorForShapeImpl))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static DebugColor SpaceDebugDrawColorForShapeCallback(cpShape handleShape, voidptr_t data)
        {
            IDebugDraw debugDraw = NativeInterop.FromIntPtr<IDebugDraw>(data);
            var shape = Shape.FromHandle(handleShape);

            return debugDraw.ColorForShape(shape);
        }

        private static readonly SpaceDebugDrawColorForShapeImpl spaceDebugDrawColorForShapeCallback = SpaceDebugDrawColorForShapeCallback;


        public cpVertPointer AcquireDebugDrawOptions(IDebugDraw debugDraw, DebugDrawFlags flags, DebugDrawColors colors)
        {
            this.flags = (int)flags;
            collisionPointColor = colors.CollisionPoint;
            constraintColor = colors.Constraint;
            shapeOutlineColor = colors.ShapeOutline;

            drawCircle = spaceDebugDrawCircleCallback.ToFunctionPointer();
            drawSegment = spaceDebugDrawSegmentCallback.ToFunctionPointer();
            drawFatSegment = spaceDebugDrawFatSegmentCallback.ToFunctionPointer();
            drawPolygon = spaceDebugDrawPolygonCallback.ToFunctionPointer();
            drawDot = spaceDebugDrawDotCallback.ToFunctionPointer();
            colorForShape = spaceDebugDrawColorForShapeCallback.ToFunctionPointer();

            data = NativeInterop.RegisterHandle(debugDraw);

            return ToPointer();
        }

        public void ReleaseDebugDrawOptions(cpVertPointer debugDrawOptionsPointer)
        {
            NativeInterop.ReleaseHandle(data);
            NativeInterop.FreeStructure(debugDrawOptionsPointer);
        }
    }
}

#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore IDE0055 // Some bug on VS