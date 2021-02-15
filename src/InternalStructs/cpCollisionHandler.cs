

using System.Runtime.InteropServices;

using cpCollisionType = System.UIntPtr;
using cpCollisionFunction = System.IntPtr;
using cpDataPointer = System.IntPtr;

using cpCollisionHandlerPointer = System.IntPtr;

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
            Marshal.StructureToPtr<cpCollisionHandler>(handler, handle, false);
        }
    }
}
