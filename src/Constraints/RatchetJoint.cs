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
    /// <see cref="RatchetJoint"/> is a rotary ratchet, which works like a socket wrench.
    /// </summary>
    public class RatchetJoint : Constraint
    {
        /// <summary>
        /// Check if a constraint is a <see cref="RatchetJoint"/>.
        /// </summary>
        public static bool IsRatchetJoint(Constraint constraint) => NativeMethods.cpConstraintIsRatchetJoint(constraint.Handle) != 0;

        /// <summary>
        /// Works like a socket wrench.
        /// </summary>
        /// <param name="bodyA">One of the two bodies to connect.</param>
        /// <param name="bodyB">One of the two bodies to connect.</param>
        /// <param name="phase">
        /// The initial offset to use when deciding where the ratchet angles are.
        /// </param>
        /// <param name="ratchet">
        /// The distance between "clicks" (following the socket wrench analogy).
        /// </param>
        public RatchetJoint(Body bodyA, Body bodyB, double phase, double ratchet)
            : base(NativeMethods.cpRatchetJointNew(bodyA.Handle, bodyB.Handle, phase, ratchet))
        {
        }

        /// <summary>
        /// The angle of the current ratchet tooth.
        /// </summary>
        public double Angle
        {
            get => NativeMethods.cpRatchetJointGetAngle(Handle);
            set => NativeMethods.cpRatchetJointSetAngle(Handle, value);
        }

        /// <summary>
        /// The phase offset of the ratchet.
        /// </summary>
        public double Phase
        {
            get => NativeMethods.cpRatchetJointGetPhase(Handle);
            set => NativeMethods.cpRatchetJointSetPhase(Handle, value);
        }

        /// <summary>
        /// The angular distance of each ratchet.
        /// </summary>
        public double Rachet
        {
            get => NativeMethods.cpRatchetJointGetRatchet(Handle);
            set => NativeMethods.cpRatchetJointSetRatchet(Handle, value);
        }
    }
}