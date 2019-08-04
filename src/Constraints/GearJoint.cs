namespace ChipmunkBinding
{
    /// <summary>
    /// GearJoint keeps the angular velocity ratio of a pair of bodies constant.
    /// </summary>
    public class GearJoint : Constraint
    {
        /// <summary>
        /// Check if a constraint is a GearJoint
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public static bool IsGearJoint(Constraint constraint) => NativeMethods.cpConstraintIsGearJoint(constraint.Handle) != 0;

        /// <summary>
        ///     Keeps the angular velocity ratio of a pair of bodies constant.
        /// ratio is always measured in absolute terms.It is currently not possible to set the ratio in relation to a third body’s angular velocity.phase is the initial angular offset of the two bodies.
        /// </summary>
        /// <param name="bodyA"></param>
        /// <param name="bodyB"></param>
        /// <param name="phase"></param>
        /// <param name="ratio"></param>
        public GearJoint(Body bodyA, Body bodyB, double phase, double ratio):
            base(NativeMethods.cpGearJointNew(bodyA.Handle, bodyB.Handle, phase, ratio))
        {
        }

        /// <summary>
        /// The phase offset of the gears.
        /// </summary>
        public double Phase
        {
            get => NativeMethods.cpGearJointGetPhase(Handle);
            set => NativeMethods.cpGearJointSetPhase(Handle, value);
        }

        /// <summary>
        /// The ratio of a gear joint.
        /// </summary>
        public double Ratio
        {
            get => NativeMethods.cpGearJointGetRatio(Handle);
            set => NativeMethods.cpGearJointSetRatio(Handle, value);
        }
    }
}
