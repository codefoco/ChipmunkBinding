using System;
using System.Collections.Generic;
using System.Text;

namespace ChipmunkBinding
{
    /// <summary>
    /// A circle shape defined by a radius
    /// This is the fastest and simplest collision shape
    /// </summary>
    public class Circle : Shape
    {
        public Circle(Body body, double radius): this(body, radius, cpVect.Zero)
        {

        }

        public Circle(Body body, double radius, cpVect offset)
            :base (NativeMethods.cpCircleShapeNew(body.Handle, radius, offset))
        {
        }
    }
}
