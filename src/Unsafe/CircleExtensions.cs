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

namespace ChipmunkBinding.Unsafe
{
    /// <summary>
    /// Unsafe extension methods for the <see cref="Circle"/> shape.
    /// </summary>
    public static class CircleExtensions
    {
        /// <summary>
        /// Change the radius of the circle shape.
        /// </summary>
        public static void SetRadius(this Circle circle, double radius)
        {
            NativeMethods.cpCircleShapeSetRadius(circle.Handle, radius);
        }

        /// <summary>
        /// Change the offset of the circle shape.
        /// </summary>
        public static void SetOffset(this Circle circle, Vect offset)
        {
            NativeMethods.cpCircleShapeSetOffset(circle.Handle, offset);
        }

    }
}