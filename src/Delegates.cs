﻿using System.Runtime.InteropServices;
using System.Security;

using voidptr_t = System.IntPtr;
using cpBody = System.IntPtr;
using cpArbiter = System.IntPtr;
using cpConstraint = System.IntPtr;
using cpHandle = System.IntPtr;
using cpShape = System.IntPtr;
using cpSpace = System.IntPtr;
using cpBool = System.Byte;
using cpContactPointSetPointer = System.IntPtr;

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
    public delegate void BodyVelocityFunction(cpBody body, Vect gravity, double damping, double dt);



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
    internal delegate void SpacePointQueryFunction(cpShape shape, Vect point, double distance, Vect gradient, voidptr_t data);

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
    internal delegate void SpaceSegmentQueryFunction(cpShape shape, Vect point, Vect normal, double alpha, voidptr_t data);



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
    internal delegate void SpaceDebugDrawCircleImpl(Vect pos, double angle, double radius, DebugColor outlineColor, DebugColor fillColor, voidptr_t data);

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
    internal delegate void SpaceDebugDrawSegmentImpl(Vect a, Vect b, DebugColor color, voidptr_t data);

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
    internal delegate void SpaceDebugDrawFatSegmentImpl(Vect a, Vect b, double radius, DebugColor outlineColor, DebugColor fillColor, voidptr_t data);

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
    internal delegate void SpaceDebugDrawPolygonImpl(int count, cpVertPointer verts, double radius, DebugColor outlineColor, DebugColor fillColor, voidptr_t data);

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
    internal delegate void SpaceDebugDrawDotImpl(double size, Vect pos, DebugColor color, voidptr_t data);

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
    internal delegate DebugColor SpaceDebugDrawColorForShapeImpl(cpShape shape, voidptr_t data);


    /// <summary>
    /// Callback function type that gets called after/before solving a joint.
    /// </summary>
    /// <param name="constraint"></param>
    /// <param name="space"></param>
    /// 
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void ConstraintSolveFunction(cpConstraint constraint, cpSpace space);

    /// <summary>
    /// Function type used for damped spring force callbacks.
    /// </summary>
    /// <param name="spring"></param>
    /// <param name="dist"></param>
    /// <returns></returns>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate double DampedSpringForceFunction(cpConstraint spring, double dist);

    /// <summary>
    /// Function type used for damped rotary spring force callbacks
    /// </summary>
    /// <param name="spring"></param>
    /// <param name="relativeAngle"></param>
    /// <returns></returns>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate double DampedRotarySpringTorqueFunction(cpConstraint  spring, double relativeAngle);
    
    /// <summary>
    /// Shape query callback function type.
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="points"></param>
    /// <param name="data"></param>
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#if __IOS__ || __TVOS__ || __WATCHOS__
    [MonoNativeFunctionWrapper]
#endif
    internal delegate void SpaceShapeQueryFunction(cpShape shape, cpContactPointSetPointer points, voidptr_t data);
}
