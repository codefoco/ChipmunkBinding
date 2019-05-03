using System;

using cpShape = System.IntPtr;

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
    }
}
