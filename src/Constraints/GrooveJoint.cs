
namespace ChipmunkBinding
{
    /// <summary>
    /// GrooveJoint is similar to a PivotJoint, but with a linear slide.
    /// One of the anchor points is a line segment that the pivot can slide in instead of being fixed.
    /// </summary>
    public class GrooveJoint : Constraint
    {
        /// <summary>
        /// Check if a constraint is a Groove Joint.
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public static bool IsGrooveJoint(Constraint constraint) => NativeMethods.cpConstraintIsGrooveJoint(constraint.Handle) != 0;

        /// <summary>
        /// The groove goes from groove_a to groove_b on body a, and the pivot is attached to anchor_b on body b.
        /// All coordinates are body local.
        /// </summary>
        /// <param name="bodyA"></param>
        /// <param name="bodyB"></param>
        /// <param name="grooveA"></param>
        /// <param name="grooveB"></param>
        /// <param name="anchorB"></param>
        public GrooveJoint(Body bodyA, Body bodyB, Vect grooveA, Vect grooveB, Vect anchorB):
            base(NativeMethods.cpGrooveJointNew(bodyA.Handle, bodyB.Handle, grooveA, grooveB, anchorB))
        {

        }

        /// <summary>
        /// The first endpoint of the groove relative to the first body.
        /// </summary>
        public Vect GrooveA
        {
            get => NativeMethods.cpGrooveJointGetGrooveA(Handle);
            set => NativeMethods.cpGrooveJointSetGrooveA(Handle, value);
        }

        /// <summary>
        /// The second endpoint of the groove relative to the first body
        /// </summary>
        public Vect GrooveB
        {
            get => NativeMethods.cpGrooveJointGetGrooveB(Handle);
            set => NativeMethods.cpGrooveJointSetGrooveB(Handle, value);
        }

        /// <summary>
        ///  The location of the second anchor relative to the second body.
        /// </summary>
        public Vect AnchorB
        {
            get => NativeMethods.cpGrooveJointGetAnchorB(Handle);
            set => NativeMethods.cpGrooveJointSetAnchorB(Handle, value);
        }
    }
}
