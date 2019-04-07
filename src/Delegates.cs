using System.Runtime.InteropServices;
using System.Security;

using voidptr_t = System.IntPtr;

namespace ChipmunkBinding
{

    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    public delegate int ChipmunkFunction(voidptr_t pointer);

    
}
