using System.Runtime.InteropServices;

using cpSpaceDebugDrawCircleImpl = System.IntPtr;
using cpSpaceDebugDrawSegmentImpl = System.IntPtr;
using cpSpaceDebugDrawFatSegmentImpl = System.IntPtr;
using cpSpaceDebugDrawPolygonImpl = System.IntPtr;
using cpSpaceDebugDrawDotImpl = System.IntPtr;
using cpSpaceDebugDrawColorForShapeImpl = System.IntPtr;
using cpShape = System.IntPtr;

using voidptr_t = System.IntPtr;
using cpVertPointer = System.IntPtr;

using cpDataPointer = System.IntPtr;

using cpSpaceDebugDrawFlags = System.Int32;
using System;

#if __IOS__ || __TVOS__ || __WATCHOS__
using ObjCRuntime;
#endif

namespace ChipmunkBinding
{
    [StructLayout (LayoutKind.Sequential)]

    internal struct cpSpaceDebugDrawOptions
    {
        /// Function that will be invoked to draw circles.
        cpSpaceDebugDrawCircleImpl drawCircle;
        /// Function that will be invoked to draw line segments.
        cpSpaceDebugDrawSegmentImpl drawSegment;
        /// Function that will be invoked to draw thick line segments.
        cpSpaceDebugDrawFatSegmentImpl drawFatSegment;
        /// Function that will be invoked to draw convex polygons.
        cpSpaceDebugDrawPolygonImpl drawPolygon;
        /// Function that will be invoked to draw dots.
        cpSpaceDebugDrawDotImpl drawDot;
    
        /// Flags that request which things to draw (collision shapes, constraints, contact points).
        cpSpaceDebugDrawFlags flags;
        /// Outline color passed to the drawing function.
        cpSpaceDebugColor shapeOutlineColor;
        /// Function that decides what fill color to draw shapes using.
        cpSpaceDebugDrawColorForShapeImpl colorForShape;
        /// Color passed to drawing functions for constraints.
        cpSpaceDebugColor constraintColor;
        /// Color passed to drawing functions for collision points.
        cpSpaceDebugColor collisionPointColor;
    
        /// User defined context pointer passed to all of the callback functions as the 'data' argument.
        cpDataPointer data;


        private IntPtr ToPointer()
        {
            IntPtr drawOptionsPtr = NativeInterop.AllocStructure<cpSpaceDebugDrawOptions>();

#if NETFRAMEWORK
            Marshal.StructureToPtr(this, drawOptionsPtr, false);
#else
            Marshal.StructureToPtr<cpSpaceDebugDrawOptions>(this, drawOptionsPtr, false);
#endif
            return drawOptionsPtr;
        }


#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(SpaceDebugDrawCircleImpl))]
#endif
        private static void SpaceDebugDrawCircleCallback(cpVect pos, double angle, double radius, cpSpaceDebugColor outlineColor, cpSpaceDebugColor fillColor, voidptr_t data)
        {
            IDebugDraw debugDraw = NativeInterop.FromIntPtr<IDebugDraw>(data);

            debugDraw.DrawCircle(pos, angle, radius, outlineColor, fillColor);
        }

        private static SpaceDebugDrawCircleImpl spaceDebugDrawCircleCallback = SpaceDebugDrawCircleCallback;

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(SpaceDebugDrawSegmentImpl))]
#endif
        private static void SpaceDebugDrawSegmentCallback(cpVect a, cpVect b, cpSpaceDebugColor color, voidptr_t data)
        {
            IDebugDraw debugDraw = NativeInterop.FromIntPtr<IDebugDraw>(data);

            debugDraw.DrawSegment(a, b, color);
        }

        private static SpaceDebugDrawSegmentImpl spaceDebugDrawSegmentCallback = SpaceDebugDrawSegmentCallback;

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(SpaceDebugDrawFatSegmentImpl))]
#endif
        private static void SpaceDebugDrawFatSegmentCallback(cpVect a, cpVect b, double radius, cpSpaceDebugColor outlineColor, cpSpaceDebugColor fillColor, voidptr_t data)
        {
            IDebugDraw debugDraw = NativeInterop.FromIntPtr<IDebugDraw>(data);

            debugDraw.DrawFatSegment(a, b, radius, outlineColor, fillColor);
        }

        private static SpaceDebugDrawFatSegmentImpl spaceDebugDrawFatSegmentCallback = SpaceDebugDrawFatSegmentCallback;

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(SpaceDebugDrawPolygonImpl))]
#endif
        private static void SpaceDebugDrawPolygonCallback(int count, cpVertPointer verts, double radius, cpSpaceDebugColor outlineColor, cpSpaceDebugColor fillColor, voidptr_t data)
        {
            IDebugDraw debugDraw = NativeInterop.FromIntPtr<IDebugDraw>(data);

            cpVect[] vectors = NativeInterop.PtrToStructureArray<cpVect>(verts, count);

            debugDraw.DrawPolygon(vectors, radius, outlineColor, fillColor);
        }

        private static SpaceDebugDrawPolygonImpl spaceDebugDrawPolygonCallback = SpaceDebugDrawPolygonCallback;

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(SpaceDebugDrawDotImpl))]
#endif
        private static void SpaceDebugDrawDotCallback(double size, cpVect pos, cpSpaceDebugColor color, voidptr_t data)
        {
            IDebugDraw debugDraw = NativeInterop.FromIntPtr<IDebugDraw>(data);

            debugDraw.DrawDot(size, pos, color);
        }

        private static SpaceDebugDrawDotImpl spaceDebugDrawDotCallback = SpaceDebugDrawDotCallback;

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(SpaceDebugDrawColorForShapeImpl))]
#endif
        private static cpSpaceDebugColor SpaceDebugDrawColorForShapeCallback(cpShape handleShape, voidptr_t data)
        {
            IDebugDraw debugDraw = NativeInterop.FromIntPtr<IDebugDraw>(data);
            var shape = Shape.FromHandle(handleShape);

            return debugDraw.ColorForShape(shape);
        }

        private static SpaceDebugDrawColorForShapeImpl spaceDebugDrawColorForShapeCallback = SpaceDebugDrawColorForShapeCallback;


        public IntPtr AcquireDebugDrawOptions(IDebugDraw debugDraw, DebugDrawFlags flags, DebugDrawColors colors)
        {
            this.flags = (int)flags;
            collisionPointColor = colors.CollisionPoint;
            constraintColor = colors.Constraint;
            shapeOutlineColor = colors.ShapeOutline;

            drawCircle = spaceDebugDrawCircleCallback.ToFunctionPointer();
            drawSegment = spaceDebugDrawSegmentCallback.ToFunctionPointer();
            drawFatSegment = spaceDebugDrawFatSegmentCallback.ToFunctionPointer();
            drawPolygon = spaceDebugDrawPolygonCallback.ToFunctionPointer();
            drawDot = spaceDebugDrawPolygonCallback.ToFunctionPointer();
            colorForShape = spaceDebugDrawColorForShapeCallback.ToFunctionPointer();

            data = NativeInterop.RegisterHandle(debugDraw);

            return ToPointer();
        }

        public void ReleaseDebugDrawOptions(IntPtr debugDrawOptionsPointer)
        {
            NativeInterop.ReleaseHandle(data);
            NativeInterop.FreeStructure(debugDrawOptionsPointer);
        }
    }
}
