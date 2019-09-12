namespace ChipmunkBinding
{
    /// <summary>
    /// <see cref="RotaryLimitJoint"/> constrains the relative rotations of two bodies.
    /// </summary>
    public class RotaryLimitJoint : Constraint
    {
        /// <summary>
        /// Check if a constraint is a <see cref="RotaryLimitJoint"/>.
        /// </summary>
        public static bool IsRotaryLimitJoint(Constraint constraint) => NativeMethods.cpConstraintIsRotaryLimitJoint(constraint.Handle) != 0;

        /// <summary>
        /// Constrains the relative rotations of two bodies.
        /// </summary>
        /// <param name="bodyA"></param>
        /// <param name="bodyB"></param>
        /// <param name="mininum">
        /// The minimum angular limit in radians. May be greater than 1 backwards revolution.
        /// </param>
        /// <param name="maximum">
        /// The maximum angular limit in radians. May be greater than 1 revolution.
        /// </param>
        public RotaryLimitJoint(Body bodyA, Body bodyB, double mininum, double maximum)
            : base(NativeMethods.cpRotaryLimitJointNew(bodyA.Handle, bodyB.Handle, mininum, maximum))
        {
        }

        /// <summary>
        /// The minimum distance the joint will maintain between the two anchors.
        /// </summary>
        public double Minimum
        {
            get => NativeMethods.cpRotaryLimitJointGetMin(Handle);
            set => NativeMethods.cpRotaryLimitJointSetMin(Handle, value);
        }

        /// <summary>
        /// The maximum distance the joint will maintain between the two anchors.
        /// </summary>
        public double Maximum
        {
            get => NativeMethods.cpRotaryLimitJointGetMax(Handle);
            set => NativeMethods.cpRotaryLimitJointSetMax(Handle, value);
        }
    }
}
