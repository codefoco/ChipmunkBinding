

using System.Runtime.InteropServices;
using cpCollisionFunction = System.IntPtr;
using cpCollisionHandlerPointer = System.IntPtr;
using cpCollisionType = System.UIntPtr;
using cpDataPointer = System.IntPtr;

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
