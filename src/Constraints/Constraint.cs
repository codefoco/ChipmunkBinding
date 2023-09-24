// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2023 Codefoco LTDA - The above copyright notice and this permission notice shall be
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
using System.Diagnostics;

using cpBody = System.IntPtr;
using cpConstraint = System.IntPtr;
using cpDataPointer = System.IntPtr;
using cpSpace = System.IntPtr;
// ReSharper disable InconsistentNaming

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
using ObjCRuntime;
#endif


namespace ChipmunkBinding
{
    /// <summary>
    /// Base class of all constraints.
    /// You usually don’t want to create instances of this class directly, but instead use one of
    /// the specific constraints such as the <see cref="PinJoint"/>.
    /// </summary>
    public abstract class Constraint : IDisposable
    {
#pragma warning disable IDE0032
        private readonly cpConstraint constraint;
#pragma warning restore IDE0032

        /// <summary>
        /// Construct a constraint with the given native handle.
        /// </summary>
        /// <param name="handle"></param>
        protected internal Constraint(cpConstraint handle)
        {
            constraint = handle;
            RegisterUserData();
        }

        /// <summary>
        /// Native handle to constraint.
        /// </summary>
        public cpConstraint Handle => constraint;

        /// <summary>
        /// Register managed object to native user data.
        /// </summary>
        private void RegisterUserData()
        {
            cpDataPointer pointer = NativeInterop.RegisterHandle(this);
            NativeMethods.cpConstraintSetUserData(constraint, pointer);
        }

        private void ReleaseUserData()
        {
            cpDataPointer pointer = NativeMethods.cpConstraintGetUserData(constraint);
            NativeInterop.ReleaseHandle(pointer);
        }

        /// <summary>
        /// Get a Constraint object from a native handle.
        /// </summary>
        public static Constraint FromHandle(cpConstraint constraint)
        {
            cpDataPointer handle = NativeMethods.cpConstraintGetUserData(constraint);
            return NativeInterop.FromIntPtr<Constraint>(handle);
        }

        /// <summary>
        /// Dispose the constraint.
        /// </summary>
        protected virtual void Dispose(bool dispose)
        {
            if (!dispose)
            {
                Debug.WriteLine("Disposing constraint {0} on finalizer... (consider Dispose explicitly)", constraint);
            }

            Free();
        }

        /// <summary>
        /// Destroy and free the constraint.
        /// </summary>
        public void Free()
        {
            ReleaseUserData();
            NativeMethods.cpConstraintFree(constraint);
        }

        /// <summary>
        /// Destroy the constraint
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Get the cpSpace this constraint is added to.
        /// </summary>
        public Space Space
        {
            get
            {
                cpSpace space = NativeMethods.cpConstraintGetSpace(constraint);
                return Space.FromHandleSafe(space);
            }
        }

        /// <summary>
        /// Get the first body the constraint is attached to.
        /// </summary>
        public Body BodyA
        {
            get
            {
                cpBody body = NativeMethods.cpConstraintGetBodyA(constraint);
                return Body.FromHandleSafe(body);
            }
        }

        /// <summary>
        /// Get the second body the constraint is attached to.
        /// </summary>
        public Body BodyB
        {
            get
            {
                cpBody body = NativeMethods.cpConstraintGetBodyB(constraint);
                return Body.FromHandleSafe(body);
            }
        }

        /// <summary>;
        /// The maximum force that this constraint is allowed to use.
        /// </summary>
        public double MaxForce
        {
            get => NativeMethods.cpConstraintGetMaxForce(constraint);
            set => NativeMethods.cpConstraintSetMaxForce(constraint, value);
        }

        /// <summary>
        /// Rate at which joint error is corrected.
        /// Defaults to pow(1.0 - 0.1, 60.0) meaning that it will
        /// correct 10% of the error every 1/60th of a second.
        /// </summary>
        public double ErrorBias
        {
            get => NativeMethods.cpConstraintGetErrorBias(constraint);
            set => NativeMethods.cpConstraintSetErrorBias(constraint, value);
        }


        /// <summary>
        /// The maximum rate at which joint error is corrected.
        /// </summary>
        public double MaxBias
        {
            get => NativeMethods.cpConstraintGetMaxBias(constraint);
            set => NativeMethods.cpConstraintSetMaxBias(constraint, value);
        }

        /// <summary>
        /// Whether the two bodies connected by the constraint are allowed to collide or not.
        ///
        /// When two bodies collide, Chipmunk ignores the collisions if this property is set to
        /// False on any constraint that connects the two bodies. Defaults to True. This can be
        /// used to create a chain that self-collides, but adjacent links in the chain do not collide.
        /// </summary>
        public bool CollideBodies
        {
            get => NativeMethods.cpConstraintGetCollideBodies(constraint) != 0;
            set => NativeMethods.cpConstraintSetCollideBodies(constraint, value ? (byte)1 : (byte)0);
        }

        private static readonly ConstraintSolveFunction preSolveFunctionCallback = ConstraintPreSolveFunctionCallback;
        private static readonly ConstraintSolveFunction postSolveFunctionCallback = ConstraintPostSolveFunctionCallback;

        private Action<Constraint, Space> preSolve;
        private Action<Constraint, Space> postSolve;

        /// <summary>
        /// Pre-solve function that is called before the solver runs.
        /// </summary>
        public Action<Constraint, Space> PreSolve
        {
            get => preSolve;
            set
            {
                preSolve = value;

                cpDataPointer callbackPointer;

                if (value == null)
                    callbackPointer = cpDataPointer.Zero;
                else
                    callbackPointer = preSolveFunctionCallback.ToFunctionPointer();

                NativeMethods.cpConstraintSetPreSolveFunc(constraint, callbackPointer);
            }
        }

        /// <summary>
        /// Post-solve function that is called after the solver runs.
        /// </summary>
        public Action<Constraint, Space> PostSolve
        {
            get => postSolve;
            set
            {
                postSolve = value;

                cpDataPointer callbackPointer;

                if (value == null)
                    callbackPointer = cpDataPointer.Zero;
                else
                    callbackPointer = postSolveFunctionCallback.ToFunctionPointer();

                NativeMethods.cpConstraintSetPostSolveFunc(constraint, callbackPointer);
            }
        }

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(ConstraintSolveFunction))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static void ConstraintPreSolveFunctionCallback(cpConstraint constraintHandle, cpSpace spaceHandle)
        {
            Constraint constraint = FromHandle(constraintHandle);
            var space = Space.FromHandle(spaceHandle);

            Action<Constraint, Space> preSolve = constraint.PreSolve;

            preSolve(constraint, space);
        }

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(ConstraintSolveFunction))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static void ConstraintPostSolveFunctionCallback(cpConstraint constraintHandle, cpSpace spaceHandle)
        {
            Constraint constraint = FromHandle(constraintHandle);
            var space = Space.FromHandle(spaceHandle);

            Action<Constraint, Space> postSolve = constraint.PostSolve;

            postSolve(constraint, space);
        }

        /// <summary>
        /// The user-definable data pointer for this constraint.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Get the last impulse applied by this constraint.
        /// </summary>
        public double Impulse => NativeMethods.cpConstraintGetImpulse(constraint);
    }
}