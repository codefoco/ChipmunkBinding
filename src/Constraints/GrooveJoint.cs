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
    /// <see cref="GrooveJoint"/> is similar to a <see cref="PivotJoint"/>, but with a linear slide.
    /// One of the anchor points is a line segment that the pivot can slide on instead of being fixed.
    /// </summary>
    public class GrooveJoint : Constraint
    {
        /// <summary>
        /// Check if a constraint is a <see cref="GrooveJoint"/>.
        /// </summary>
        public static bool IsGrooveJoint(Constraint constraint) => NativeMethods.cpConstraintIsGrooveJoint(constraint.Handle) != 0;

        /// <summary>
        /// Create an anchor where <paramref name="bodyB"/> can rotate similar to a
        /// <see cref="PivotJoint"/>, except it's anchored at <paramref name="anchorB"/>, which is a
        /// point that can slide between <paramref name="grooveA"/> and <paramref name="grooveB"/>.
        /// </summary>
        /// <param name="bodyA">The first connected body.</param>
        /// <param name="bodyB">The second connected body.</param>
        /// <param name="grooveA">
        /// The start of the groove on <paramref name="bodyA"/>. Coordinates are local to the body.
        /// </param>
        /// <param name="grooveB">
        /// The end of the groove on <paramref name="bodyA"/>. Coordinates are local to the body.
        /// </param>
        /// <param name="anchorB">
        /// The location of the pivot on <paramref name="bodyB"/>. Coordinates are local to the
        /// body.
        /// </param>
        public GrooveJoint(Body bodyA, Body bodyB, Vect grooveA, Vect grooveB, Vect anchorB)
            : base(NativeMethods.cpGrooveJointNew(bodyA.Handle, bodyB.Handle, grooveA, grooveB, anchorB))
        {
        }

        /// <summary>
        /// The first endpoint of the groove relative to the first body.
        /// </summary>
        public Vect GrooveA
        {
            get => NativeMethods.cpGrooveJointGetGrooveA(Handle);
            set => NativeMethods.cpGrooveJointSetGrooveA(Handle, value);
        }

        /// <summary>
        /// The second endpoint of the groove relative to the first body.
        /// </summary>
        public Vect GrooveB
        {
            get => NativeMethods.cpGrooveJointGetGrooveB(Handle);
            set => NativeMethods.cpGrooveJointSetGrooveB(Handle, value);
        }

        /// <summary>
        ///  The location of the second anchor relative to the second body.
        /// </summary>
        public Vect AnchorB
        {
            get => NativeMethods.cpGrooveJointGetAnchorB(Handle);
            set => NativeMethods.cpGrooveJointSetAnchorB(Handle, value);
        }
    }
}