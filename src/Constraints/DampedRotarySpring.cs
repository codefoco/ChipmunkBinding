using System;

using cpConstraint = System.IntPtr;


#if __IOS__ || __TVOS__ || __WATCHOS__
using ObjCRuntime;
#endif

namespace ChipmunkBinding
{
    /// <summary>
    /// DampedRotarySpring works like the DammpedSpring but in a angular fashion.
    /// </summary>
    public class DampedRotarySpring : Constraint
    {
        /// <summary>
        /// Check if a constraint is a damped rotary string
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public static bool IsDampedRotarySpring(Constraint constraint) => NativeMethods.cpConstraintIsDampedRotarySpring(constraint.Handle) != 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bodyA"></param>
        /// <param name="bodyB"></param>
        /// <param name="restAngle"></param>
        /// <param name="stiffness"></param>
        /// <param name="damping"></param>
        public DampedRotarySpring(Body bodyA,
                                  Body bodyB,
                                  double restAngle,
                                  double stiffness,
                                  double damping) :
            base(NativeMethods.cpDampedRotarySpringNew(bodyA.Handle,
                                                 bodyB.Handle,
                                                 restAngle,
                                                 stiffness,
                                                 damping))
        {

        }

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(DampedRotarySpringTorqueFunction))]
#endif
        private static double DampedRotarySpringTorqueCallback(cpConstraint springHandle, double relativeAngle)
        {
            var constraint = (DampedRotarySpring)Constraint.FromHandle(springHandle);

            Func<DampedRotarySpring, double, double> dampedRotarySpringTorqueFunction = constraint.TorqueFunction;

            return dampedRotarySpringTorqueFunction(constraint, relativeAngle);
        }

        private static DampedRotarySpringTorqueFunction DampedRotarySpringForceCallback = DampedRotarySpringTorqueCallback;

        /// <summary>
        /// The rest angle of the spring.
        /// </summary>
        public double RestAngle
        {
            get => NativeMethods.cpDampedRotarySpringGetRestAngle(Handle);
            set => NativeMethods.cpDampedRotarySpringSetRestAngle(Handle, value);
        }

        /// <summary>
        /// The stiffness of the spring in force/distance.
        /// </summary>
        public double Stiffness
        {
            get => NativeMethods.cpDampedRotarySpringGetStiffness(Handle);
            set => NativeMethods.cpDampedRotarySpringSetStiffness(Handle, value);
        }

        /// <summary>
        /// The damping of the spring.
        /// </summary>
        public double Damping
        {
            get => NativeMethods.cpDampedRotarySpringGetDamping(Handle);
            set => NativeMethods.cpDampedRotarySpringSetDamping(Handle, value);
        }

        private Func<DampedRotarySpring, double, double> torqueFunction;

        /// <summary>
        /// Damped rotary spring torque custom function callback.
        /// </summary>
        public Func<DampedRotarySpring, double, double> TorqueFunction
        {
            get => torqueFunction;
            set
            {
                torqueFunction = value;

                IntPtr callbackPointer;

                if (value == null)
                    callbackPointer = IntPtr.Zero;
                else
                    callbackPointer = DampedRotarySpringForceCallback.ToFunctionPointer();

                NativeMethods.cpDampedRotarySpringSetSpringTorqueFunc(Handle, callbackPointer);
            }
        }
    }
}
