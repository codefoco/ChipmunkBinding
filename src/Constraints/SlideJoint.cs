namespace ChipmunkBinding
{
    /// <summary>
    /// SlideJoint is like a PinJoint, but have a minimum and maximum distance.
    /// A chain could be modeled using this joint.It keeps the anchor points from getting to far apart, but will allow them to get closer together.
    /// </summary>
    public class SlideJoint : Constraint
    {
        /// <summary>
        /// Check if a constraint is a slide joint.
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public static bool IsSlideJoint(Constraint constraint) => NativeMethods.cpConstraintIsSlideJoint(constraint.Handle) != 0;

        /// <summary>
        /// a and b are the two bodies to connect, anchor_a and anchor_b are the anchor points on those bodies, and min and max define the allowed distances of the anchor points.
        /// </summary>
        /// <param name="bodyA"></param>
        /// <param name="bodyB"></param>
        /// <param name="anchorA"></param>
        /// <param name="anchorB"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public SlideJoint(Body bodyA, Body bodyB, Vect anchorA, Vect anchorB, double min, double max) :
            base(NativeMethods.cpSlideJointNew(bodyA.Handle, bodyB.Handle, anchorA, anchorB, min, max))
        {

        }

        /// <summary>
        /// The location of the first anchor relative to the first body.
        /// </summary>
        public Vect AnchorA
        {
            get => NativeMethods.cpSlideJointGetAnchorA(Handle);
            set => NativeMethods.cpSlideJointSetAnchorA(Handle, value);
        }

        /// <summary>
        /// The location of the second anchor relative to the second body.
        /// </summary>
        public Vect AnchorB
        {
            get => NativeMethods.cpSlideJointGetAnchorB(Handle);
            set => NativeMethods.cpSlideJointSetAnchorB(Handle, value);
        }

        /// <summary>
        /// The minimum distance the joint will maintain between the two anchors
        /// </summary>
        public double Minimum
        {
            get => NativeMethods.cpSlideJointGetMin(Handle);
            set => NativeMethods.cpSlideJointSetMin(Handle, value);
        }

        /// <summary>
        /// The maximum distance the joint will maintain between the two anchors.
        /// </summary>
        public double Maximum
        {
            get => NativeMethods.cpSlideJointGetMax(Handle);
            set => NativeMethods.cpSlideJointSetMax(Handle, value);
        }
    }
}
