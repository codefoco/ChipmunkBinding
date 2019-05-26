using System;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    static class DelegateExtensions
    {
        public static BodyArbiterIteratorFunction ToBodyArbiterIteratorFunction(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (BodyArbiterIteratorFunction) Marshal.GetDelegateForFunctionPointer(ptr, typeof(BodyArbiterIteratorFunction));
#else
            return Marshal.GetDelegateForFunctionPointer<BodyArbiterIteratorFunction>(ptr);
#endif
        }

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

        public static BodyConstraintIteratorFunction ToBodyConstraintIteratorFunction(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (BodyConstraintIteratorFunction) Marshal.GetDelegateForFunctionPointer(ptr, typeof(BodyConstraintIteratorFunction));
#else
            return Marshal.GetDelegateForFunctionPointer<BodyConstraintIteratorFunction>(ptr);
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

        public static BodyShapeIteratorFunction ToBodyShapeIteratorFunction(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (BodyShapeIteratorFunction) Marshal.GetDelegateForFunctionPointer(ptr, typeof(BodyShapeIteratorFunction));
#else
            return Marshal.GetDelegateForFunctionPointer<BodyShapeIteratorFunction>(ptr);
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

        public static BodyVelocityFunction ToBodyVelocityFunction(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (BodyVelocityFunction) Marshal.GetDelegateForFunctionPointer(ptr, typeof(BodyVelocityFunction));
#else
            return Marshal.GetDelegateForFunctionPointer<BodyVelocityFunction>(ptr);
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

        public static BodyPositionFunction ToBodyPositionFunction(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (BodyPositionFunction) Marshal.GetDelegateForFunctionPointer(ptr, typeof(BodyPositionFunction));
#else
            return Marshal.GetDelegateForFunctionPointer<BodyPositionFunction>(ptr);
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

        public static CollisionBeginFunction ToCollisionBeginFunctioin(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (CollisionBeginFunction)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CollisionBeginFunction));
#else
            return Marshal.GetDelegateForFunctionPointer<CollisionBeginFunction>(ptr);
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

        public static PostStepFunction ToPostStepFunction(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (PostStepFunction)Marshal.GetDelegateForFunctionPointer(ptr, typeof(PostStepFunction));
#else
            return Marshal.GetDelegateForFunctionPointer<PostStepFunction>(ptr);
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

        public static SpacePointQueryFunction ToSpacePointQueryFunction(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (SpacePointQueryFunction)Marshal.GetDelegateForFunctionPointer(ptr, typeof(SpacePointQueryFunction));
#else
            return Marshal.GetDelegateForFunctionPointer<SpacePointQueryFunction>(ptr);
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

        public static SpaceBBQueryFunction ToSpaceBBQueryFunction(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (SpaceBBQueryFunction)Marshal.GetDelegateForFunctionPointer(ptr, typeof(SpaceBBQueryFunction));
#else
            return Marshal.GetDelegateForFunctionPointer<SpaceBBQueryFunction>(ptr);
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

        public static SpaceConstraintIteratorFunction ToSpaceConstraintIteratorFunction(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (SpaceConstraintIteratorFunction)Marshal.GetDelegateForFunctionPointer(ptr, typeof(SpaceConstraintIteratorFunction));
#else
            return Marshal.GetDelegateForFunctionPointer<SpaceConstraintIteratorFunction>(ptr);
#endif
        }

        public static IntPtr ToFunctionPointer(this SpaceConstraintIteratorFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<SpaceConstraintIteratorFunction>(d);
#endif
        }

        public static SpaceDebugDrawCircleImpl ToSpaceDebugDrawCircleImpl(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (SpaceDebugDrawCircleImpl)Marshal.GetDelegateForFunctionPointer(ptr, typeof(SpaceDebugDrawCircleImpl));
#else
            return Marshal.GetDelegateForFunctionPointer<SpaceDebugDrawCircleImpl>(ptr);
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

        public static SpaceDebugDrawSegmentImpl ToSpaceDebugDrawSegmentImpl(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (SpaceDebugDrawSegmentImpl)Marshal.GetDelegateForFunctionPointer(ptr, typeof(SpaceDebugDrawSegmentImpl));
#else
            return Marshal.GetDelegateForFunctionPointer<SpaceDebugDrawSegmentImpl>(ptr);
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

        public static SpaceDebugDrawFatSegmentImpl ToSpaceDebugDrawFatSegmentImpl(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (SpaceDebugDrawFatSegmentImpl)Marshal.GetDelegateForFunctionPointer(ptr, typeof(SpaceDebugDrawFatSegmentImpl));
#else
            return Marshal.GetDelegateForFunctionPointer<SpaceDebugDrawFatSegmentImpl>(ptr);
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

        public static SpaceDebugDrawPolygonImpl ToSpaceDebugDrawPolygonImpl(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (SpaceDebugDrawPolygonImpl)Marshal.GetDelegateForFunctionPointer(ptr, typeof(SpaceDebugDrawPolygonImpl));
#else
            return Marshal.GetDelegateForFunctionPointer<SpaceDebugDrawPolygonImpl>(ptr);
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

        public static SpaceDebugDrawDotImpl ToSpaceDebugDrawDotImpl(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (SpaceDebugDrawDotImpl)Marshal.GetDelegateForFunctionPointer(ptr, typeof(SpaceConstraintIteratorFunction));
#else
            return Marshal.GetDelegateForFunctionPointer<SpaceDebugDrawDotImpl>(ptr);
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

        public static SpaceDebugDrawColorForShapeImpl ToSpaceDebugDrawColorForShapeImpl(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (SpaceDebugDrawColorForShapeImpl)Marshal.GetDelegateForFunctionPointer(ptr, typeof(SpaceDebugDrawColorForShapeImpl));
#else
            return Marshal.GetDelegateForFunctionPointer<SpaceDebugDrawColorForShapeImpl>(ptr);
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

    }
}
