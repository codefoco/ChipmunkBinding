namespace ChipmunkBinding
{
    /// <summary>
    /// PivotJoint allow two objects to pivot about a single point.
    /// Its like a swivel.
    /// </summary>
    public class PivotJoint : Constraint
    {
        /// <summary>
        /// Check if a constraint is a pin joint.
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public static bool IsPivotJoint(Constraint constraint) => NativeMethods.cpConstraintIsPivotJoint(constraint.Handle) != 0;

        /// <summary>
        /// a and b are the two bodies to connect, and pivot is the point in world coordinates of the pivot.
        /// Because the pivot location is given in world coordinates, you must have the bodies moved into the correct positions already.Alternatively you can specify the joint based on a pair of anchor points, but make sure you have the bodies in the right place as the joint will fix itself as soon as you start simulating the space.
        /// That is, either create the joint with PivotJoint(a, b, pivot) or PivotJoint(a, b, anchor_a, anchor_b).
        /// </summary>
        /// <param name="bodyA"></param>
        /// <param name="bodyB"></param>
        /// <param name="anchorA"></param>
        /// <param name="anchorB"></param>
        public PivotJoint(Body bodyA, Body bodyB, Vect anchorA, Vect anchorB) :
            base(NativeMethods.cpPivotJointNew2(bodyA.Handle, bodyB.Handle, anchorA, anchorB))
        {

        }
        /// <summary>
        /// Initialize a pivot joint with one ancor
        /// </summary>
        /// <param name="bodyA"></param>
        /// <param name="bodyB"></param>
        /// <param name="anchor"></param>
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
