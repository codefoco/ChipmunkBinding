namespace ChipmunkBinding
{
    /// <summary>
    /// <see cref="PivotJoint"/> acts like a swivel, allowing two objects to pivot about a single
    /// point.
    /// </summary>
    public class PivotJoint : Constraint
    {
        /// <summary>
        /// Check if a constraint is a <see cref="PinJoint"/>.
        /// </summary>
        public static bool IsPivotJoint(Constraint constraint) => NativeMethods.cpConstraintIsPivotJoint(constraint.Handle) != 0;

        /// <summary>
        /// Initialize a pivot joint with two anchors. Since the anchors are provided in world
        /// coordinates, the bodies must already be correctly positioned. The joint is fixed as soon
        /// as the containing space is simulated.
        /// </summary>
        /// <param name="bodyA">One of the two bodies to connect.</param>
        /// <param name="bodyB">One of the two bodies to connect.</param>
        /// <param name="anchorA">
        /// The location of one of the anchors, specified in world coordinates.
        /// </param>
        /// <param name="anchorB">
        /// The location of one of the anchors, specified in world coordinates.
        /// </param>
        public PivotJoint(Body bodyA, Body bodyB, Vect anchorA, Vect anchorB)
            : base(NativeMethods.cpPivotJointNew2(bodyA.Handle, bodyB.Handle, anchorA, anchorB))
        {
        }

        /// <summary>
        /// Initialize a pivot joint with one anchor. Since the pivot is provided in world
        /// coordinates, the bodies must already be correctly positioned.
        /// </summary>
        /// <param name="bodyA">One of the two bodies to connect.</param>
        /// <param name="bodyB">One of the two bodies to connect.</param>
        /// <param name="anchor">The location of the pivot, specified in world coordinates.</param>
        public PivotJoint(Body bodyA, Body bodyB, Vect anchor) :
        base(NativeMethods.cpPivotJointNew(bodyA.Handle, bodyB.Handle, anchor))
        {

        }

        /// <summary>
        /// The location of the first anchor relative to the first body.
        /// </summary>
        public Vect AnchorA
        {
            get => NativeMethods.cpPivotJointGetAnchorA(Handle);
            set => NativeMethods.cpPivotJointSetAnchorA(Handle, value);
        }

        /// <summary>
        /// The location of the second anchor relative to the second body.
        /// </summary>
        public Vect AnchorB
        {
            get => NativeMethods.cpPivotJointGetAnchorB(Handle);
            set => NativeMethods.cpPivotJointSetAnchorB(Handle, value);
        }

    }
}
