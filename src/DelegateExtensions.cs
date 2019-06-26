using System;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    static class DelegateExtensions
    {
        public static IntPtr ToFunctionPointer(this BodyArbiterIteratorFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<BodyArbiterIteratorFunction>(d);
#endif
        }

        public static IntPtr ToFunctionPointer(this BodyConstraintIteratorFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<BodyConstraintIteratorFunction>(d);
#endif
        }

        public static IntPtr ToFunctionPointer(this BodyShapeIteratorFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<BodyShapeIteratorFunction>(d);
#endif
        }

        public static IntPtr ToFunctionPointer(this BodyVelocityFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<BodyVelocityFunction>(d);
#endif
        }

        public static IntPtr ToFunctionPointer(this BodyPositionFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<BodyPositionFunction>(d);
#endif
        }

        public static IntPtr ToFunctionPointer(this CollisionBeginFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<CollisionBeginFunction>(d);
#endif
        }


        public static IntPtr ToFunctionPointer(this CollisionPreSolveFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<CollisionPreSolveFunction>(d);
#endif
        }


        public static IntPtr ToFunctionPointer(this CollisionPostSolveFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<CollisionPostSolveFunction>(d);
#endif
        }


        public static IntPtr ToFunctionPointer(this CollisionSeparateFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<CollisionSeparateFunction>(d);
#endif
        }


        public static IntPtr ToFunctionPointer(this PostStepFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<PostStepFunction>(d);
#endif
        }


        public static IntPtr ToFunctionPointer(this SpaceSegmentQueryFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<SpaceSegmentQueryFunction>(d);
#endif
        }

        public static IntPtr ToFunctionPointer(this SpacePointQueryFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<SpacePointQueryFunction>(d);
#endif
        }

        public static IntPtr ToFunctionPointer(this SpaceBBQueryFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<SpaceBBQueryFunction>(d);
#endif
        }


        public static IntPtr ToFunctionPointer(this SpaceObjectIteratorFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<SpaceObjectIteratorFunction>(d);
#endif
        }


        public static IntPtr ToFunctionPointer(this SpaceDebugDrawCircleImpl d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<SpaceDebugDrawCircleImpl>(d);
#endif
        }


        public static IntPtr ToFunctionPointer(this SpaceDebugDrawSegmentImpl d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<SpaceDebugDrawSegmentImpl>(d);
#endif
        }

        public static IntPtr ToFunctionPointer(this SpaceDebugDrawFatSegmentImpl d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<SpaceDebugDrawFatSegmentImpl>(d);
#endif
        }

        public static IntPtr ToFunctionPointer(this SpaceDebugDrawPolygonImpl d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<SpaceDebugDrawPolygonImpl>(d);
#endif
        }

        public static IntPtr ToFunctionPointer(this SpaceDebugDrawDotImpl d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<SpaceDebugDrawDotImpl>(d);
#endif
        }

        public static IntPtr ToFunctionPointer(this SpaceDebugDrawColorForShapeImpl d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<SpaceDebugDrawColorForShapeImpl>(d);
#endif
        }

        public static IntPtr ToFunctionPointer(this ConstraintPreSolveFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<ConstraintPreSolveFunction>(d);
#endif
        }

        public static IntPtr ToFunctionPointer(this ConstraintPostSolveFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<ConstraintPostSolveFunction>(d);
#endif
        }

    }
}
