namespace ChipmunkBinding
{
    /// <summary>
    /// RotaryLimitJoint constrains the relative rotations of two bodies.
    /// </summary>
    public class RotaryLimitJoint : Constraint
    {
        /// <summary>
        /// Check if a constraint is a rotary limit joint.
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public static bool IsRotaryLimitJoint(Constraint constraint) => NativeMethods.cpConstraintIsRotaryLimitJoint(constraint.Handle) != 0;

        /// <summary>
        ///     Constrains the relative rotations of two bodies.
        /// min and max are the angular limits in radians.It is implemented so that it’s possible to for the range to be greater than a full revolution.
        /// </summary>
        /// <param name="bodyA"></param>
        /// <param name="bodyB"></param>
        /// <param name="mininum"></param>
        /// <param name="maximum"></param>
        public RotaryLimitJoint(Body bodyA, Body bodyB, double mininum, double maximum) :
            base(NativeMethods.cpRotaryLimitJointNew(bodyA.Handle, bodyB.Handle, mininum, maximum))
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
        /// Rhe maximum distance the joint will maintain between the two anchors.
        /// </summary>
        public double Maximum
        {
            get => NativeMethods.cpRotaryLimitJointGetMax(Handle);
            set => NativeMethods.cpRotaryLimitJointSetMax(Handle, value);
        }
    }
}
