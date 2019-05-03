using System.Runtime.InteropServices;
using System.Security;

using voidptr_t = System.IntPtr;
using cpBody = System.IntPtr;
using cpArbiter = System.IntPtr;

namespace ChipmunkBinding
{

    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    public delegate void BodyArbiterIteratorFunction(cpBody body, cpArbiter arbiter, voidptr_t data);

}
