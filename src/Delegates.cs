using System.Runtime.InteropServices;
using System.Security;

using voidptr_t = System.IntPtr;
using cpBody = System.IntPtr;
using cpArbiter = System.IntPtr;
using cpConstraint = System.IntPtr;
using cpShape = System.IntPtr;
using cpSpace = System.IntPtr;
using cpBool = System.Byte;

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

    /// <summary>
    /// Delegate method to iterate over constraints
    /// </summary>
    /// <param name="body"></param>
    /// <param name="constraint"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void BodyConstraintIteratorFunction(cpBody body, cpConstraint constraint, voidptr_t data);

    /// <summary>
    /// Delegate method to iterate over shapes
    /// </summary>
    /// <param name="body"></param>
    /// <param name="shape"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void BodyShapeIteratorFunction(cpBody body, cpShape shape, voidptr_t data);

    /// <summary>
    /// Collision pre-solve event function callback type.
    /// Returning false from a pre-step callback causes the collision to be ignored until the next step.
    /// </summary>
    /// <param name="arbiter"></param>
    /// <param name="space"></param>
    /// <param name="userData"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate cpBool CollisionBeginFunction(cpArbiter arbiter, cpSpace space, voidptr_t userData);

    /// <summary>
    /// Post Step callback function type.
    /// </summary>
    /// <param name="space"></param>
    /// <param name="key"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void PostStepFunction(cpSpace space, voidptr_t key, voidptr_t data);

}
