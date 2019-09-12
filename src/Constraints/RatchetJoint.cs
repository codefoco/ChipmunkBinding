namespace ChipmunkBinding
{
    /// <summary>
    /// <see cref="RatchetJoint"/> is a rotary ratchet, which works like a socket wrench.
    /// </summary>
    public class RatchetJoint : Constraint
    {
        /// <summary>
        /// Check if a constraint is a <see cref="RatchetJoint"/>.
        /// </summary>
        public static bool IsRatchetJoint(Constraint constraint) => NativeMethods.cpConstraintIsRatchetJoint(constraint.Handle) != 0;

        /// <summary>
        /// Works like a socket wrench.
        /// </summary>
        /// <param name="bodyA">One of the two bodies to connect.</param>
        /// <param name="bodyB">One of the two bodies to connect.</param>
        /// <param name="phase">
        /// The initial offset to use when deciding where the ratchet angles are.
        /// </param>
        /// <param name="ratchet">
        /// The distance between "clicks" (following the socket wrench analogy).
        /// </param>
        public RatchetJoint(Body bodyA, Body bodyB, double phase, double ratchet)
            : base(NativeMethods.cpRatchetJointNew(bodyA.Handle, bodyB.Handle, phase, ratchet))
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
