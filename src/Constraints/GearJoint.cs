namespace ChipmunkBinding
{
    /// <summary>
    /// <see cref="GearJoint"/> keeps the angular velocity ratio of a pair of bodies constant.
    /// </summary>
    public class GearJoint : Constraint
    {
        /// <summary>
        /// Check if a constraint is a <see cref="GearJoint"/>.
        /// </summary>
        public static bool IsGearJoint(Constraint constraint) => NativeMethods.cpConstraintIsGearJoint(constraint.Handle) != 0;

        /// <summary>
        /// Keeps the angular velocity ratio of a pair of bodies constant.
        /// </summary>
        /// <param name="bodyA">The first connected body.</param>
        /// <param name="bodyB">The second connected body.</param>
        /// <param name="phase">The seconded connected body.</param>
        /// <param name="ratio">
        /// Measured in absolute terms. It is currently not possible to set
        /// the ratio in relation to a third body’s angular velocity.
        /// </param>
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
