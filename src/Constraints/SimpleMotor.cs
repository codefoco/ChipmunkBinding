namespace ChipmunkBinding
{
    /// <summary>
    /// SimpleMotor keeps the relative angular velocity constant.
    /// </summary>
    public class SimpleMotor : Constraint
    {
        /// <summary>
        /// Check if constraint is simple motor constraint
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public static bool IsSimpleMotor(Constraint constraint) => NativeMethods.cpConstraintIsSimpleMotor(constraint.Handle) != 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bodyA"></param>
        /// <param name="bodyB"></param>
        /// <param name="rate"></param>
        public SimpleMotor(Body bodyA, Body bodyB, double rate):
            base(NativeMethods.cpSimpleMotorNew(bodyA.Handle, bodyB.Handle, rate))
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
