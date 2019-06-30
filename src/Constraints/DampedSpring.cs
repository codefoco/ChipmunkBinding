using System;

using cpConstraint = System.IntPtr;


#if __IOS__ || __TVOS__ || __WATCHOS__
using ObjCRuntime;
#endif

namespace ChipmunkBinding
{
    /// <summary>
    /// DampedSpring is a damped spring.
    /// The spring allows you to define the rest length, stiffness and damping.
    /// </summary>
    public class DampedSpring : Constraint
    {
        /// <summary>
        /// Check if a constraint is a damped string
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public static bool IsDampedSpring(Constraint constraint) => NativeMethods.cpConstraintIsDampedSpring(constraint.Handle) != 0;

        /// <summary>
        /// Defined much like a slide joint.
        /// </summary>
        /// <param name="bodyA">Body b</param>
        /// <param name="bodyB">Body a</param>
        /// <param name="anchorA">Anchor point a, relative to body a</param>
        /// <param name="anchorB"> Anchor point b, relative to body b</param>
        /// <param name="restLength">The distance the spring wants to be.</param>
        /// <param name="stiffness">The spring constant (Young’s modulus).</param>
        /// <param name="damping">How soft to make the damping of the spring.</param>
        public DampedSpring(Body bodyA,
                            Body bodyB,
                            Vect anchorA,
                            Vect anchorB,
                            double restLength,
                            double stiffness,
                            double damping):
            base(NativeMethods.cpDampedSpringNew(bodyA.Handle,
                                                 bodyB.Handle,
                                                 anchorA,
                                                 anchorB,
                                                 restLength,
                                                 stiffness,
                                                 damping))
        {

        }

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(DampedSpringForceFunction))]
#endif
        private static double DampedSpringForceCallback(cpConstraint springHandle, double distance)
        {
            var constraint = (DampedSpring)Constraint.FromHandle(springHandle);

            Func<DampedSpring, double, double> dampedSpringForceFunction = constraint.forceFunction;

            return dampedSpringForceFunction(constraint, distance);
        }

        private static DampedSpringForceFunction dampedSpringForceCallback = DampedSpringForceCallback;

        /// <summary>
        /// The location of the first anchor relative to the first body.
        /// </summary>
        public Vect AnchorA
        {
            get => NativeMethods.cpDampedSpringGetAnchorA(Handle);
            set => NativeMethods.cpDampedSpringSetAnchorA(Handle, value);
        }

        /// <summary>
        /// The location of the second anchor relative to the second body.
        /// </summary>
        public Vect AnchorB
        {
            get => NativeMethods.cpDampedSpringGetAnchorB(Handle);
            set => NativeMethods.cpDampedSpringSetAnchorB(Handle, value);
        }

        /// <summary>
        /// The rest length of the spring.
        /// </summary>
        public double RestLength
        {
            get => NativeMethods.cpDampedSpringGetRestLength(Handle);
            set => NativeMethods.cpDampedSpringSetRestLength(Handle, value);
        }

        /// <summary>
        /// The stiffness of the spring in force/distance.
        /// </summary>
        public double Stiffness
        {
            get => NativeMethods.cpDampedSpringGetStiffness(Handle);
            set => NativeMethods.cpDampedSpringSetStiffness(Handle, value);
        }

        /// <summary>
        /// The damping of the spring.
        /// </summary>
        public double Damping
        {
            get => NativeMethods.cpDampedSpringGetDamping(Handle);
            set => NativeMethods.cpDampedSpringSetDamping(Handle, value);
        }

        private Func<DampedSpring, double, double> forceFunction;

        /// <summary>
        /// Damped spring force custom function callback.
        /// </summary>
        public Func<DampedSpring, double, double> ForceFunction
        {
            get => forceFunction;
            set
            {
                forceFunction = value;

                IntPtr callbackPointer;

                if (value == null)
                    callbackPointer = IntPtr.Zero;
                else
                    callbackPointer = dampedSpringForceCallback.ToFunctionPointer();

                NativeMethods.cpDampedSpringSetSpringForceFunc(Handle, callbackPointer);
            }
        }
    }
}
