namespace ChipmunkBinding
{
    public class GearJoint : Constraint
    {
        public static bool IsGearJoint(Constraint constraint) => NativeMethods.cpConstraintIsGearJoint(constraint.Handle) != 0;

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
