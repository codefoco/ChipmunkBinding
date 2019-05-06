using System;

using cpShape = System.IntPtr;
using cpDataPointer = System.IntPtr;

namespace ChipmunkBinding
{
    public class Shape
    {
        cpShape shape;

        public Shape(Body body, double width, double height, double radius)
        {
            shape = NativeMethods.cpBoxShapeNew(body.Handle, width, height, radius);
        }

        public cpShape Handle => shape;

        public static Shape FromHandle(cpShape constraint)
        {
            cpDataPointer handle = NativeMethods.cpShapeGetUserData(constraint);
            return HandleInterop.FromIntPtr<Shape>(handle);
        }
    }
}
