namespace ChipmunkBinding
{
    /// <summary>
    /// <see cref="SlideJoint"/> is like a <see cref="PinJoint"/>, but with a minimum and maximum
    /// distance. A chain could be modeled using this joint. It keeps the anchor points from getting
    /// too far apart, but will allow them to get closer together.
    /// </summary>
    public class SlideJoint : Constraint
    {
        /// <summary>
        /// Check if a constraint is a <see cref="SlideJoint"/>.
        /// </summary>
        public static bool IsSlideJoint(Constraint constraint) => NativeMethods.cpConstraintIsSlideJoint(constraint.Handle) != 0;

        /// <summary>
        /// Create a slide constraint between two bodies.
        /// </summary>
        /// <param name="bodyA">One of the two bodies to connect.</param>
        /// <param name="bodyB">One of the two bodies to connect.</param>
        /// <param name="anchorA">The anchor point for <paramref name="bodyA"/>.</param>
        /// <param name="anchorB">The anchor point for <paramref name="bodyB"/>.</param>
        /// <param name="min">The minimum distance the anchor points can get to each other.</param>
        /// <param name="max">The maximum distance the anchor points can be apart.</param>
        public SlideJoint(Body bodyA, Body bodyB, Vect anchorA, Vect anchorB, double min, double max)
            : base(NativeMethods.cpSlideJointNew(bodyA.Handle, bodyB.Handle, anchorA, anchorB, min, max))
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
