using System.Runtime.InteropServices;
using System.Security;

using voidptr_t = System.IntPtr;
using cpBody = System.IntPtr;
using cpArbiter = System.IntPtr;

namespace ChipmunkBinding
{
    /// <summary>
    /// Delegate method to iterate over arbiters
    /// </summary>
    /// <param name="body"></param>
    /// <param name="arbiter"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    public delegate void BodyArbiterIteratorFunction(cpBody body, cpArbiter arbiter, voidptr_t data);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="body"></param>
    /// <param name="gravity"></param>
    /// <param name="damping"></param>
    /// <param name="dt"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void BodyVelocityFunction(cpBody body, cpVect gravity, double damping, double dt);



    /// <summary>
    /// Rigid body position update function type.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="dt"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void BodyPositionFunction(cpBody body, double dt);

}
