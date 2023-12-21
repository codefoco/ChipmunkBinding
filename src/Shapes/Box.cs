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
    /// A retangular shape shape 
    /// </summary>
    public class Box : Shape
    {
        /// <summary>
        /// Create and initialize a box polygon shape.
        /// </summary>
        public Box(Body body, double width, double height, double radius)
            : base(NativeMethods.cpBoxShapeNew(body.Handle, width, height, radius))
        {

        }

        /// <summary>
        /// Create and initialize an offset box polygon shape.
        /// </summary>
        public Box(Body body, BoundingBox box, double radius)
            : base(NativeMethods.cpBoxShapeNew2(body.Handle, box, radius))
        {

        }

        /// <summary>
        /// Calculate the moment of inertia for a solid box.
        /// </summary>
        public static double MomentForBox(double mass, double width, double height)
        {
            return NativeMethods.cpMomentForBox(mass, width, height);
        }

        /// <summary>
        /// Calculate the moment of inertia for a solid box.
        /// </summary>
        public static double MomentForBox(double mass, BoundingBox boundingBox)
        {
            return NativeMethods.cpMomentForBox2(mass, boundingBox);
        }
    }
}