using System;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    static class DelegateExtensions
    {
        public static ChipmunkFunction ToChipmunkFunction(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
#if NETFRAMEWORK
            return (ChipmunkFunction) Marshal.GetDelegateForFunctionPointer(ptr, typeof(ChipmunkFunction));
#else
            return Marshal.GetDelegateForFunctionPointer<ChipmunkFunction>(ptr);
#endif
        }

        public static IntPtr ToFunctionPointer(this ChipmunkFunction d)
        {
            if (d == null)
                return IntPtr.Zero;

#if NETFRAMEWORK
            return Marshal.GetFunctionPointerForDelegate(d);
#else
            return Marshal.GetFunctionPointerForDelegate<ChipmunkFunction>(d);
#endif
        }
    }
}
