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