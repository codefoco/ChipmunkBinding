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

using System.Runtime.InteropServices;

using cpCollisionFunction = System.IntPtr;
using cpCollisionHandlerPointer = System.IntPtr;
using cpCollisionType = System.UIntPtr;
using cpDataPointer = System.IntPtr;
// ReSharper disable InconsistentNaming

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct cpCollisionHandler
    {
        public cpCollisionType typeA;
        public cpCollisionType typeB;
        public cpCollisionFunction beginFunction;
        public cpCollisionFunction preSolveFunction;
        public cpCollisionFunction postSolveFunction;
        public cpCollisionFunction separateFunction;
        public cpDataPointer userData;

        public static cpCollisionHandler FromHandle(cpCollisionHandlerPointer handle)
        {
            return Marshal.PtrToStructure<cpCollisionHandler>(handle);
        }

        internal static void ToPointer(cpCollisionHandler handler, cpCollisionFunction handle)
        {
            Marshal.StructureToPtr(handler, handle, false);
        }
    }
}