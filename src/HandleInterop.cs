using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using cpDataPointer = System.IntPtr;

namespace ChipmunkBinding
{
    internal static class HandleInterop
    {
        public static cpDataPointer RegisterHandle(object obj)
        {
            var gcHandle = GCHandle.Alloc(obj);
            return GCHandle.ToIntPtr(gcHandle);
        }

        public static T FromIntPtr<T>(cpDataPointer pointer)
        {
            Debug.Assert(pointer != IntPtr.Zero, "IntPtr parameter should never be Zero");

            var handle = GCHandle.FromIntPtr(pointer);

            Debug.Assert(handle.IsAllocated, "GCHandle not allocated.");
            Debug.Assert(handle.Target is T, "Target is not of type T.");

            return (T)handle.Target;
        }

        public static void ReleaseHandle(cpDataPointer pointer)
        {
            Debug.Assert(pointer != IntPtr.Zero, "IntPtr parameter should never be Zero");

            var handle = GCHandle.FromIntPtr(pointer);

            Debug.Assert(handle.IsAllocated, "GCHandle not allocated.");

            handle.Free();
        }
    }
}
