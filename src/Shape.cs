using System;

using cpShape = System.IntPtr;
using cpDataPointer = System.IntPtr;
using System.Diagnostics;

namespace ChipmunkBinding
{
    public class Shape : IDisposable
    {
#pragma warning disable IDE0032
        private readonly cpShape shape;
#pragma warning restore IDE0032


        public Shape(Body body, double width, double height, double radius)
        {
            shape = NativeMethods.cpBoxShapeNew(body.Handle, width, height, radius);
            RegisterUserData();
        }

        internal protected Shape(cpShape shapeHandle)
        {
            shape = shapeHandle;
            RegisterUserData();
        }

        internal cpShape Handle => shape;

        protected void RegisterUserData()
        {
            cpDataPointer pointer = NativeInterop.RegisterHandle(this);
            NativeMethods.cpShapeSetUserData(shape, pointer);
        }

        void ReleaseUserData()
        {
            cpDataPointer pointer = NativeMethods.cpShapeGetUserData(shape);
            NativeInterop.ReleaseHandle(pointer);
        }

        public static Shape FromHandle(cpShape constraint)
        {
            cpDataPointer handle = NativeMethods.cpShapeGetUserData(constraint);
            return NativeInterop.FromIntPtr<Shape>(handle);
        }

        protected virtual void Dispose(bool dispose)
        {
            if (!dispose)
            {
                Debug.WriteLine("Disposing shape {0} on finalizer... (consider Dispose explicitly)", shape);
            }
            Free();
        }

        public void Free()
        {
            ReleaseUserData();
            NativeMethods.cpBodyFree(shape);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Shape()
        {
            Dispose(false);
        }
    }
}
