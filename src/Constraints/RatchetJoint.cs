namespace ChipmunkBinding
{
    /// <summary>
    /// RatchetJoint is a rotary ratchet, it works like a socket wrench.
    /// </summary>
    public class RatchetJoint : Constraint
    {
        /// <summary>
        /// Check if Constraint  is a RatchetJoint
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public static bool IsRatchetJoint(Constraint constraint) => NativeMethods.cpConstraintIsRatchetJoint(constraint.Handle) != 0;
        /// <summary>
        ///     Works like a socket wrench.
        /// ratchet is the distance between “clicks”, phase is the initial offset to use when deciding where the ratchet angles are.
        /// </summary>
        /// <param name="bodyA"></param>
        /// <param name="bodyB"></param>
        /// <param name="phase"></param>
        /// <param name="ratchet"></param>
        public RatchetJoint(Body bodyA, Body bodyB, double phase, double ratchet):
            base(NativeMethods.cpRatchetJointNew(bodyA.Handle, bodyB.Handle, phase, ratchet))
        {
        }

        /// <summary>
        /// The angle of the current ratchet tooth.
        /// </summary>
        public double Angle
        {
            get => NativeMethods.cpRatchetJointGetAngle(Handle);
            set => NativeMethods.cpRatchetJointSetAngle(Handle, value);
        }

        /// <summary>
        /// The phase offset of the ratchet.
        /// </summary>
        public double Phase
        {
            get => NativeMethods.cpRatchetJointGetPhase(Handle);
            set => NativeMethods.cpRatchetJointSetPhase(Handle, value);
        }

        /// <summary>
        /// The angular distance of each ratchet.
        /// </summary>
        public double Rachet
        {
            get => NativeMethods.cpRatchetJointGetRatchet(Handle);
            set => NativeMethods.cpRatchetJointSetRatchet(Handle, value);
        }
    }
}
