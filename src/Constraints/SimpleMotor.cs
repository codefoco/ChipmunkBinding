namespace ChipmunkBinding
{
    /// <summary>
    /// <see cref="SimpleMotor"/> keeps the relative angular velocity constant.
    /// </summary>
    public class SimpleMotor : Constraint
    {
        /// <summary>
        /// Check if constraint is a <see cref="SimpleMotor"/>.
        /// </summary>
        public static bool IsSimpleMotor(Constraint constraint) => NativeMethods.cpConstraintIsSimpleMotor(constraint.Handle) != 0;

        /// <summary>
        /// Rotate with a constant relative angular velocity constant between two bodies.
        /// </summary>
        /// <param name="bodyA">One of the two bodies.</param>
        /// <param name="bodyB">One of the two bodies.</param>
        /// <param name="rate">The rate of rotation.</param>
        public SimpleMotor(Body bodyA, Body bodyB, double rate)
            : base(NativeMethods.cpSimpleMotorNew(bodyA.Handle, bodyB.Handle, rate))
        {
        }

        /// <summary>
        /// The rate of the motor.
        /// </summary>
        public double Rate
        {
            get => NativeMethods.cpSimpleMotorGetRate(Handle);
            set => NativeMethods.cpSimpleMotorSetRate(Handle, value);
        }
    }
}
