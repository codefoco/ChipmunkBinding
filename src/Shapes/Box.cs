
namespace ChipmunkBinding
{
    /// <summary>
    /// A retangular shape shape 
    /// </summary>
    public class Box : Shape
    {
        /// <summary>
        /// Create and initialize a box shaped polygon shape.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="radius"></param>
        public Box(Body body, double width, double height, double radius) :
            base(NativeMethods.cpBoxShapeNew(body.Handle, width, height, radius))
        {

        }

        /// <summary>
        /// Create and initialize an offset box shaped polygon shape.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="box"></param>
        /// <param name="radius"></param>
        public Box(Body body, BoundingBox box, double radius) :
            base(NativeMethods.cpBoxShapeNew2(body.Handle, box, radius))
        {

        }

    }
}
