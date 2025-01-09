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

namespace ChipmunkBinding
{
    /// <summary>
    /// <see cref="GearJoint"/> keeps the angular velocity ratio of a pair of bodies constant.
    /// </summary>
    public class GearJoint : Constraint
    {
        /// <summary>
        /// Check if a constraint is a <see cref="GearJoint"/>.
        /// </summary>
        public static bool IsGearJoint(Constraint constraint) => NativeMethods.cpConstraintIsGearJoint(constraint.Handle) != 0;

        /// <summary>
        /// Keeps the angular velocity ratio of a pair of bodies constant.
        /// </summary>
        /// <param name="bodyA">The first connected body.</param>
        /// <param name="bodyB">The second connected body.</param>
        /// <param name="phase">The seconded connected body.</param>
        /// <param name="ratio">
        /// Measured in absolute terms. It is currently not possible to set
        /// the ratio in relation to a third body’s angular velocity.
        /// </param>
        public GearJoint(Body bodyA, Body bodyB, double phase, double ratio) :
            base(NativeMethods.cpGearJointNew(bodyA.Handle, bodyB.Handle, phase, ratio))
        {
        }

        /// <summary>
        /// The phase offset of the gears.
        /// </summary>
        public double Phase
        {
            get => NativeMethods.cpGearJointGetPhase(Handle);
            set => NativeMethods.cpGearJointSetPhase(Handle, value);
        }

        /// <summary>
        /// The ratio of a gear joint.
        /// </summary>
        public double Ratio
        {
            get => NativeMethods.cpGearJointGetRatio(Handle);
            set => NativeMethods.cpGearJointSetRatio(Handle, value);
        }
    }
}