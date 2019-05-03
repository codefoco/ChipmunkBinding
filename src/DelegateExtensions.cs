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
    }
}
