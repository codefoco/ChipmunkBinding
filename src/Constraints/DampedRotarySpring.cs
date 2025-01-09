// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2025 Codefoco LTDA - The above copyright notice and this permission notice shall be
//     included in all copies or substantial portions of the Software.
//
//     Redistribution and use in source and binary forms, with or without
//     modification, are permitted only if explicitly approved by the authors.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
//     EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//     OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//     NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//     HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
//     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//     FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//     OTHER DEALINGS IN THE SOFTWARE.

using System;

using cpConstraint = System.IntPtr;


#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
using ObjCRuntime;
#endif

namespace ChipmunkBinding
{
    /// <summary>
    /// DampedRotarySpring works like <see cref="DampedSpring"/>, but in an angular fashion.
    /// </summary>
    public class DampedRotarySpring : Constraint
    {
        /// <summary>
        /// Check if a constraint is a <see cref="DampedRotarySpring"/>.
        /// </summary>
        public static bool IsDampedRotarySpring(Constraint constraint) => NativeMethods.cpConstraintIsDampedRotarySpring(constraint.Handle) != 0;

        /// <summary>
        /// Create a damped rotary spring.
        /// </summary>
        public DampedRotarySpring(
            Body bodyA,
            Body bodyB,
            double restAngle,
            double stiffness,
            double damping)
            : base(
                NativeMethods.cpDampedRotarySpringNew(
                    bodyA.Handle,
                    bodyB.Handle,
                    restAngle,
                    stiffness,
                    damping))
        {
            originalTorqueCallbackPointer = NativeMethods.cpDampedRotarySpringGetSpringTorqueFunc(Handle);
        }

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(DampedRotarySpringTorqueFunction))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static double DampedRotarySpringTorqueCallback(cpConstraint springHandle, double relativeAngle)
        {
            var constraint = (DampedRotarySpring)FromHandle(springHandle);

            Func<DampedRotarySpring, double, double> dampedRotarySpringTorqueFunction = constraint.TorqueFunction;

            return dampedRotarySpringTorqueFunction(constraint, relativeAngle);
        }

        private static readonly DampedRotarySpringTorqueFunction DampedRotarySpringForceCallback = DampedRotarySpringTorqueCallback;

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
        private readonly cpConstraint originalTorqueCallbackPointer;

        /// <summary>
        /// Damped rotary spring torque custom function callback.
        /// </summary>
        public Func<DampedRotarySpring, double, double> TorqueFunction
        {
            get => torqueFunction;
            set
            {
                torqueFunction = value;

                cpConstraint callbackPointer;

                if (value == null)
                {
                    callbackPointer = originalTorqueCallbackPointer;
                }
                else
                {
                    callbackPointer = DampedRotarySpringForceCallback.ToFunctionPointer();
                }

                NativeMethods.cpDampedRotarySpringSetSpringTorqueFunc(Handle, callbackPointer);
            }
        }
    }
}