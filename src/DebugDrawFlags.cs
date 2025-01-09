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

namespace ChipmunkBinding
{
    /// <summary>
    /// Flags to enable or disable DebugDrawing.
    /// </summary>
    [Flags]
#pragma warning disable CA1711
    public enum DebugDrawFlags
#pragma warning restore CA1711
    {
        /// <summary>
        /// Draw nothing.
        /// </summary>
        None = 0,

        /// <summary>
        /// Draw Shapes.
        /// </summary>
        Shapes = 1 << 0,

        /// <summary>
        /// Draw Constraints.
        /// </summary>
        Constraints = 1 << 1,

        /// <summary>
        ///  Draw Collision Points.
        /// </summary>
        CollisionPoints = 1 << 2,

        /// <summary>
        /// Draw All.
        /// </summary>
        All = Shapes | Constraints | CollisionPoints
    }
}