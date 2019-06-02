using System.Runtime.InteropServices;
using System.Security;

using voidptr_t = System.IntPtr;
using cpBody = System.IntPtr;
using cpArbiter = System.IntPtr;
using cpConstraint = System.IntPtr;
using cpHandle = System.IntPtr;
using cpShape = System.IntPtr;
using cpSpace = System.IntPtr;
using cpBool = System.Byte;


using cpVertPointer = System.IntPtr;

#if __IOS__ || __TVOS__ || __WATCHOS__
using ObjCRuntime;
#endif

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
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void BodyArbiterIteratorFunction(cpBody body, cpArbiter arbiter, voidptr_t data);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="body"></param>
    /// <param name="gravity"></param>
    /// <param name="damping"></param>
    /// <param name="dt"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    public delegate void BodyVelocityFunction(cpBody body, cpVect gravity, double damping, double dt);



    /// <summary>
    /// Rigid body position update function type.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="dt"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    public delegate void BodyPositionFunction(cpBody body, double dt);

    /// <summary>
    /// Delegate method to iterate over constraints
    /// </summary>
    /// <param name="body"></param>
    /// <param name="constraint"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void BodyConstraintIteratorFunction(cpBody body, cpConstraint constraint, voidptr_t data);

    /// <summary>
    /// Delegate method to iterate over shapes
    /// </summary>
    /// <param name="body"></param>
    /// <param name="shape"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void BodyShapeIteratorFunction(cpBody body, cpShape shape, voidptr_t data);

    /// <summary>
    /// Collision begin event function callback type.
    /// Returning false from a begin callback causes the collision to be ignored until
    /// the the separate callback is called when the objects stop colliding.
    /// </summary>
    /// <param name="arbiter"></param>
    /// <param name="space"></param>
    /// <param name="userData"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate cpBool CollisionBeginFunction(cpArbiter arbiter, cpSpace space, voidptr_t userData);

    /// <summary>
    /// Collision pre-solve event function callback type.
    /// Returning false from a pre-step callback causes the collision to be ignored until the next step.
    /// </summary>
    /// <param name="arbiter"></param>
    /// <param name="space"></param>
    /// <param name="userData"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate cpBool CollisionPreSolveFunction(cpArbiter arbiter, cpSpace space, voidptr_t userData);

    /// <summary>
    /// Collision Post-Solve 
    /// </summary>
    /// <param name="arbiter"></param>
    /// <param name="space"></param>
    /// <param name="userData"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void CollisionPostSolveFunction(cpArbiter arbiter, cpSpace space, voidptr_t userData);

    /// <summary>
    /// Collision separate event function callback type
    /// </summary>
    /// <param name="arbiter"></param>
    /// <param name="space"></param>
    /// <param name="userData"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void CollisionSeparateFunction(cpArbiter arbiter, cpSpace space, voidptr_t userData);

    /// <summary>
    /// Post Step callback function type.
    /// </summary>
    /// <param name="space"></param>
    /// <param name="key"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void PostStepFunction(cpSpace space, voidptr_t key, voidptr_t data);

    /// <summary>
    /// Nearest point query callback function type.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="point"></param>
    /// <param name="distance"></param>
    /// <param name="gradient"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void SpacePointQueryFunction(cpShape shape, cpVect point, double distance, cpVect gradient, voidptr_t data);

    /// <summary>
    /// Segment query callback function type.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="point"></param>
    /// <param name="normal"></param>
    /// <param name="alpha"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void SpaceSegmentQueryFunction(cpShape shape, cpVect point, cpVect normal, double alpha, voidptr_t data);



    /// <summary>
    /// Rectangle Query callback function type.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void SpaceBBQueryFunction(cpShape shape, voidptr_t data);

    /// <summary>
    /// Space/object iterator callback function type.
    /// </summary>
    /// <param name="constraint"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void SpaceObjectIteratorFunction(cpHandle handle, voidptr_t data);

    /// <summary>
    /// Callback type for a function that draws a filled, stroked circle.
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="angle"></param>
    /// <param name="radius"></param>
    /// <param name="outlineColor"></param>
    /// <param name="fillColor"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void SpaceDebugDrawCircleImpl(cpVect pos, double angle, double radius, cpSpaceDebugColor outlineColor, cpSpaceDebugColor fillColor, voidptr_t data);

    /// <summary>
    /// Callback type for a function that draws a line segment.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="color"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void SpaceDebugDrawSegmentImpl(cpVect a, cpVect b, cpSpaceDebugColor color, voidptr_t data);

    /// <summary>
    /// Callback type for a function that draws a thick line segment.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="radius"></param>
    /// <param name="outlineColor"></param>
    /// <param name="fillColor"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void SpaceDebugDrawFatSegmentImpl(cpVect a, cpVect b, double radius, cpSpaceDebugColor outlineColor, cpSpaceDebugColor fillColor, voidptr_t data);

    /// <summary>
    /// Callback type for a function that draws a convex polygon.
    /// </summary>
    /// <param name="count"></param>
    /// <param name=""></param>
    /// <param name="verts"></param>
    /// <param name="radius"></param>
    /// <param name="outlineColor"></param>
    /// <param name="fillColor"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void SpaceDebugDrawPolygonImpl(int count, cpVertPointer verts, double radius, cpSpaceDebugColor outlineColor, cpSpaceDebugColor fillColor, voidptr_t data);

    /// <summary>
    /// Callback type for a function that draws a dot.
    /// </summary>
    /// <param name="size"></param>
    /// <param name="pos"></param>
    /// <param name="color"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void SpaceDebugDrawDotImpl(double size, cpVect pos, cpSpaceDebugColor color, voidptr_t data);

    /// <summary>
    /// Callback type for a function that returns a color for a given shape. This gives you an opportunity to color shapes based on how they are used in your engine.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate cpSpaceDebugColor SpaceDebugDrawColorForShapeImpl(cpShape shape, voidptr_t data);

}
