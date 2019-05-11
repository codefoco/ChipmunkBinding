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

        public static CollisionBeginFunctioin ToCollisionBeginFunctioin(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (CollisionBeginFunctioin)Marshal.GetDelegateForFunctionPointer(ptr, typeof(CollisionBeginFunctioin));
#else
            return Marshal.GetDelegateForFunctionPointer<CollisionBeginFunctioin>(ptr);
#endif
        }

        public static IntPtr ToFunctionPointer(this CollisionBeginFunctioin d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<CollisionBeginFunctioin>(d);
#endif
        }


    }
}
