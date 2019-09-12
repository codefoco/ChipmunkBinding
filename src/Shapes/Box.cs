
namespace ChipmunkBinding
{
    /// <summary>
    /// A retangular shape shape 
    /// </summary>
    public class Box : Shape
    {
        /// <summary>
        /// Create and initialize a box polygon shape.
        /// </summary>
        public Box(Body body, double width, double height, double radius)
            : base(NativeMethods.cpBoxShapeNew(body.Handle, width, height, radius))
        {

        }

        /// <summary>
        /// Create and initialize an offset box polygon shape.
        /// </summary>
        public Box(Body body, BoundingBox box, double radius)
            : base(NativeMethods.cpBoxShapeNew2(body.Handle, box, radius))
        {

        }

        /// <summary>
        /// Calculate the moment of inertia for a solid box.
        /// </summary>
        public static double MomentForBox(double mass, double width, double height)
        {
            return NativeMethods.cpMomentForBox(mass, width, height);
        }

        /// <summary>
        /// Calculate the moment of inertia for a solid box.
        /// </summary>
        public static double MomentForBox(double mass, BoundingBox boundingBox)
        {
            return NativeMethods.cpMomentForBox2(mass, boundingBox);
        }
    }
}
