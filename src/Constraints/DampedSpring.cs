// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2024 Codefoco LTDA - The above copyright notice and this permission notice shall be
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
// ReSharper disable InconsistentNaming


#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
using ObjCRuntime;
#endif

#pragma warning disable IDE1006 // Naming Styles

namespace ChipmunkBinding
{
    /// <summary>
    /// <see cref="DampedSpring"/> is a damped spring.
    /// The spring allows you to define the rest length, stiffness and damping.
    /// </summary>
    public class DampedSpring : Constraint
    {
        /// <summary>
        /// Check if a constraint is a <see cref="DampedSpring"/>.
        /// </summary>
        public static bool IsDampedSpring(Constraint constraint) => NativeMethods.cpConstraintIsDampedSpring(constraint.Handle) != 0;

        /// <summary>
        /// Defined much like a slide joint.
        /// </summary>
        /// <param name="bodyA">The first connected body.</param>
        /// <param name="bodyB">The second connected body.</param>
        /// <param name="anchorA">Anchor point a, relative to body a.</param>
        /// <param name="anchorB"> Anchor point b, relative to body b.</param>
        /// <param name="restLength">The distance the spring wants to be.</param>
        /// <param name="stiffness">The spring constant (Young’s modulus).</param>
        /// <param name="damping">How soft to make the damping of the spring.</param>
        public DampedSpring(
            Body bodyA,
            Body bodyB,
            Vect anchorA,
            Vect anchorB,
            double restLength,
            double stiffness,
            double damping)
            : base(NativeMethods.cpDampedSpringNew(
                bodyA.Handle,
                bodyB.Handle,
                anchorA,
                anchorB,
                restLength,
                stiffness,
                damping))
        {
            originalForceCallbackPointer = NativeMethods.cpDampedSpringGetSpringForceFunc(Handle);
        }

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(DampedSpringForceFunction))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static double DampedSpringForceCallback(cpConstraint springHandle, double distance)
        {
            var constraint = (DampedSpring)FromHandle(springHandle);

            Func<DampedSpring, double, double> dampedSpringForceFunction = constraint.forceFunction;

            return dampedSpringForceFunction(constraint, distance);
        }

        private static readonly DampedSpringForceFunction dampedSpringForceCallback = DampedSpringForceCallback;

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

        private readonly cpConstraint originalForceCallbackPointer;

        /// <summary>
        /// Damped spring force custom function callback.
        /// </summary>
        public Func<DampedSpring, double, double> ForceFunction
        {
            get => forceFunction;
            set
            {
                forceFunction = value;

                cpConstraint callbackPointer;

                if (value == null)
                {
                    callbackPointer = originalForceCallbackPointer;
                }
                else
                {
                    callbackPointer = dampedSpringForceCallback.ToFunctionPointer();
                }

                NativeMethods.cpDampedSpringSetSpringForceFunc(Handle, callbackPointer);
            }
        }
    }
}

#pragma warning restore IDE1006 // Naming Styles