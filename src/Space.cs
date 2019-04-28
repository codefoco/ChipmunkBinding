using System;

using cpSpace = System.IntPtr;
using cpDataPointer = System.IntPtr;

namespace ChipmunkBinding
{
    public class Space : IDisposable
    {
        readonly cpSpace space;

        public Space()
        {
            space = NativeMethods.cpSpaceNew();
            RegisterUserData();
        }

        public void Destroy()
        {
            ReleaseUserData();
            NativeMethods.cpSpaceDestroy(space);
        }

        public void Dispose()
        {
            Destroy();
        }

        public void AddBody(Body body)
        {
            NativeMethods.cpSpaceAddBody(space, body.Handle);
        }

        void RegisterUserData()
        {
            cpDataPointer pointer = HandleInterop.RegisterHandle(this);
            NativeMethods.cpSpaceSetUserData(space, pointer);
        }

        void ReleaseUserData()
        {
            cpDataPointer pointer = NativeMethods.cpSpaceGetUserData(space);
            HandleInterop.ReleaseHandle(pointer);
        }

        public static Space FromHandle(cpSpace space)
        {
            cpDataPointer handle = NativeMethods.cpSpaceGetUserData(space);
            return HandleInterop.FromIntPtr<Space>(handle);
        }

        public static Space FromHandleSafe(cpSpace space)
        {
            if (space == IntPtr.Zero)
                return null;
            return FromHandle(space);
        }
    }
}
