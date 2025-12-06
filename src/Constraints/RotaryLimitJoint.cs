// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2026 Codefoco LTDA - The above copyright notice and this permission notice shall be
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
    /// <see cref="RotaryLimitJoint"/> constrains the relative rotations of two bodies.
    /// </summary>
    public class RotaryLimitJoint : Constraint
    {
        /// <summary>
        /// Check if a constraint is a <see cref="RotaryLimitJoint"/>.
        /// </summary>
        public static bool IsRotaryLimitJoint(Constraint constraint) => NativeMethods.cpConstraintIsRotaryLimitJoint(constraint.Handle) != 0;

        /// <summary>
        /// Constrains the relative rotations of two bodies.
        /// </summary>
        /// <param name="bodyA"></param>
        /// <param name="bodyB"></param>
        /// <param name="mininum">
        /// The minimum angular limit in radians. May be greater than 1 backwards revolution.
        /// </param>
        /// <param name="maximum">
        /// The maximum angular limit in radians. May be greater than 1 revolution.
        /// </param>
        public RotaryLimitJoint(Body bodyA, Body bodyB, double mininum, double maximum)
            : base(NativeMethods.cpRotaryLimitJointNew(bodyA.Handle, bodyB.Handle, mininum, maximum))
        {
        }

        /// <summary>
        /// The minimum distance the joint will maintain between the two anchors.
        /// </summary>
        public double Minimum
        {
            get => NativeMethods.cpRotaryLimitJointGetMin(Handle);
            set => NativeMethods.cpRotaryLimitJointSetMin(Handle, value);
        }

        /// <summary>
        /// The maximum distance the joint will maintain between the two anchors.
        /// </summary>
        public double Maximum
        {
            get => NativeMethods.cpRotaryLimitJointGetMax(Handle);
            set => NativeMethods.cpRotaryLimitJointSetMax(Handle, value);
        }
    }
}