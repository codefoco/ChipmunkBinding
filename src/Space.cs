﻿// .      ______          __     ____               
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
using System.Collections.Generic;
using System.Runtime.InteropServices;

using cpBody = System.IntPtr;
using cpCollisionHandlerPointer = System.IntPtr;
using cpCollisionType = System.UIntPtr;
using cpConstraint = System.IntPtr;
using cpDataPointer = System.IntPtr;
using cpShape = System.IntPtr;
using cpSpace = System.IntPtr;
using voidptr_t = System.IntPtr;
// ReSharper disable InconsistentNaming
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable IDE0055 // Some bug on VS

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
using ObjCRuntime;
#endif

namespace ChipmunkBinding
{
    /// <summary>
    /// Spaces in Chipmunk are the basic unit of simulation. You add rigid bodies, shapes, and
    /// constraints to the space and then step them all forward through time together.
    /// </summary>
    public class Space : IDisposable
    {
#pragma warning disable IDE0032
        private readonly cpSpace space;
#pragma warning restore IDE0032

        /// <summary>
        /// Native handle cpSpace.
        /// </summary>
        public cpSpace Handle => space;

        /// <summary>
        /// Create a new Space object.
        /// </summary>
        public Space()
        {
            space = NativeMethods.cpSpaceNew();
            RegisterUserData();
        }

        /// <summary>
        /// Create a space from a native Handle (used by derived classes).
        /// </summary>
        /// <param name="handle"></param>
        protected internal Space(cpSpace handle)
        {
            space = handle;
            RegisterUserData();
        }

        /// <summary>
        /// Destroys and frees.
        /// </summary>
        public void Free()
        {
            ReleaseUserData();
            FreeSpace(space);
        }

        /// <summary>
        /// Destroy and free space.
        /// </summary>
        /// <param name="handle"></param>
        protected virtual void FreeSpace(cpSpace handle)
        {
            NativeMethods.cpSpaceFree(handle);
        }

        /// <summary>
        /// Destroy and free space.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            Free();
        }
        /// <summary>
        /// Disposes the Space object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void RegisterUserData()
        {
            cpDataPointer pointer = NativeInterop.RegisterHandle(this);
            NativeMethods.cpSpaceSetUserData(space, pointer);
        }

        private void ReleaseUserData()
        {
            cpDataPointer pointer = NativeMethods.cpSpaceGetUserData(space);
            NativeInterop.ReleaseHandle(pointer);
        }

        /// <summary>
        /// Get a Space object from native cpSpace handle.
        /// </summary>
        public static Space FromHandle(cpSpace space)
        {
            cpDataPointer handle = NativeMethods.cpSpaceGetUserData(space);
            return NativeInterop.FromIntPtr<Space>(handle);
        }

        /// <summary>
        /// Get a Space object from native cpSpace handle, but return null if the handle is 0.
        /// </summary>
        public static Space FromHandleSafe(cpSpace space)
        {
            if (space == cpCollisionHandlerPointer.Zero)
            {
                return null;
            }

            return FromHandle(space);
        }

        // Properties

        /// <summary>
        /// Arbitrary user data.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Number of iterations to use in the impulse solver to solve contacts and other
        /// constraints.
        /// </summary>
        public int Iterations
        {
            get => NativeMethods.cpSpaceGetIterations(space);
            set => NativeMethods.cpSpaceSetIterations(space, value);
        }

        /// <summary>
        /// Gravity to pass to rigid bodies when integrating velocity.
        /// </summary>
        public Vect Gravity
        {
            get => NativeMethods.cpSpaceGetGravity(space);
            set => NativeMethods.cpSpaceSetGravity(space, value);
        }

        /// <summary>
        /// Damping rate expressed as the fraction of velocity that bodies retain each second. A
        /// value of 0.9 would mean that each body's velocity will drop 10% per second. The default
        /// value is 1.0, meaning no damping is applied. Note: This damping value is different than
        /// those of <see cref="DampedSpring"/> and <see cref="DampedRotarySpring"/>.
        /// </summary>
        public double Damping
        {
            get => NativeMethods.cpSpaceGetDamping(space);
            set => NativeMethods.cpSpaceSetDamping(space, value);
        }

        /// <summary>
        /// Speed threshold for a body to be considered idle. The default value of 0 means to let
        /// the space guess a good threshold based on gravity.
        /// </summary>
        public double IdleSpeedThreshold
        {
            get => NativeMethods.cpSpaceGetIdleSpeedThreshold(space);
            set => NativeMethods.cpSpaceSetIdleSpeedThreshold(space, value);
        }

        /// <summary>
        /// Time a group of bodies must remain idle in order to fall asleep. Enabling sleeping also
        /// implicitly enables the the contact graph. The default value of infinity disables the
        /// sleeping algorithm.
        /// </summary>
        public double SleepTimeThreshold
        {
            get => NativeMethods.cpSpaceGetSleepTimeThreshold(space);
            set => NativeMethods.cpSpaceSetSleepTimeThreshold(space, value);
        }

        /// <summary>
        /// Amount of encouraged penetration between colliding shapes. This is used to reduce
        /// oscillating contacts and keep the collision cache warm. Defaults to 0.1. If you have
        /// poor simulation quality, increase this number as much as possible without allowing
        /// visible amounts of overlap.
        /// </summary>
        public double CollisionSlop
        {
            get => NativeMethods.cpSpaceGetCollisionSlop(space);
            set => NativeMethods.cpSpaceSetCollisionSlop(space, value);
        }

        /// <summary>
        /// Determines how fast overlapping shapes are pushed apart.
        /// </summary>
        public double CollisionBias
        {
            get => NativeMethods.cpSpaceGetCollisionBias(space);
            set => NativeMethods.cpSpaceSetCollisionBias(space, value);
        }

        /// <summary>
        /// Number of frames that contact information should persist. Defaults to 3. There is
        /// probably never a reason to change this value.
        /// </summary>
        public int CollisionPersistence
        {
            get => (int)NativeMethods.cpSpaceGetCollisionPersistence(space);
            set => NativeMethods.cpSpaceSetCollisionPersistence(space, (uint)value);
        }

        /// <summary>
        /// The Space provided static body for a given <see cref="Space"/>.
        /// </summary>
        public Body StaticBody
        {
            get
            {
                cpBody bodyHandle = NativeMethods.cpSpaceGetStaticBody(space);
                cpDataPointer gcHandle = NativeMethods.cpBodyGetUserData(bodyHandle);

                if (gcHandle != cpCollisionHandlerPointer.Zero)
                {
                    return NativeInterop.FromIntPtr<Body>(gcHandle);
                }

                return new Body(bodyHandle);
            }
        }

        /// <summary>
        /// Returns the current (or most recent) time step used with the given space.
        /// Useful from callbacks if your time step is not a compile-time global.
        /// </summary>
        public double CurrentTimeStep => NativeMethods.cpSpaceGetCurrentTimeStep(space);

        /// <summary>
        /// Returns true from inside a callback when objects cannot be added/removed.
        /// </summary>
        public bool IsLocked => NativeMethods.cpSpaceIsLocked(space) != 0;


        // Collision Handlers

        /// <summary>
        /// Create or return the existing collision handler that is called for all collisions that are
        /// not handled by a more specific collision handler.
        /// </summary>
        public CollisionHandler GetOrCreateDefaultCollisionHandler()
        {
            cpCollisionHandlerPointer collisionHandle = NativeMethods.cpSpaceAddDefaultCollisionHandler(space);
            return CollisionHandler.GetOrCreate(collisionHandle);
        }


        /// <summary>
        /// Create or return the existing collision handler for the specified pair of collision
        /// types. If wildcard handlers are used with either of the collision types, it's the
        /// responsibility of the custom handler to invoke the wildcard handlers.
        /// </summary>
        public CollisionHandler GetOrCreateCollisionHandler(int typeA, int typeB)
        {
            uint utypeA = unchecked((uint)typeA);
            uint utypeB = unchecked((uint)typeB);

            cpCollisionType collisionTypeA = new cpCollisionType(utypeA);
            cpCollisionType collisionTypeB = new cpCollisionType(utypeB);

            cpCollisionHandlerPointer collisionHandle = NativeMethods.cpSpaceAddCollisionHandler(space, collisionTypeA, collisionTypeB);
            return CollisionHandler.GetOrCreate(collisionHandle);
        }


        /// <summary>
        /// Create or return the existing wildcard collision handler for the specified type.
        /// </summary>
        public CollisionHandler GetOrCreateWildcardHandler(int type)
        {
            cpCollisionHandlerPointer collisionHandle = NativeMethods.cpSpaceAddWildcardHandler(space, (cpCollisionType)type);
            return CollisionHandler.GetOrCreate(collisionHandle);
        }

        /// <summary>
        /// Add a collision shape to the simulation.
        /// </summary>
        public void AddShape(Shape shape)
        {
            _ = NativeMethods.cpSpaceAddShape(space, shape.Handle);
        }

        /// <summary>
        /// Add a rigid body to the simulation.
        /// </summary>
        public void AddBody(Body body)
        {
            _ = NativeMethods.cpSpaceAddBody(space, body.Handle);
        }

        /// <summary>
        /// Add a constraint to the simulation.
        /// </summary>
        public void AddConstraint(Constraint constraint)
        {
            _ = NativeMethods.cpSpaceAddConstraint(space, constraint.Handle);
        }

        /// <summary>
        /// Remove a collision shape from the simulation.
        /// </summary>
        public void RemoveShape(Shape shape)
        {
            NativeMethods.cpSpaceRemoveShape(space, shape.Handle);
        }

        /// <summary>
        /// Remove a rigid body from the simulation.
        /// </summary>
        public void RemoveBody(Body body)
        {
            NativeMethods.cpSpaceRemoveBody(space, body.Handle);
        }

        /// <summary>
        /// Remove a constraint from the simulation.
        /// </summary>
        public void RemoveConstraint(Constraint constraint)
        {
            NativeMethods.cpSpaceRemoveConstraint(space, constraint.Handle);
        }

        /// <summary>
        /// Test if a collision shape has been added to the space.
        /// </summary>
        public bool Contains(Shape shape)
        {
            return NativeMethods.cpSpaceContainsShape(space, shape.Handle) != 0;
        }

        /// <summary>
        /// Test if a rigid body has been added to the space.
        /// </summary>
        public bool Contains(Body body)
        {
            return NativeMethods.cpSpaceContainsBody(space, body.Handle) != 0;
        }

        /// <summary>
        /// Test if a constraint has been added to the space.
        /// </summary>
        public bool Contains(Constraint constraint)
        {
            return NativeMethods.cpSpaceContainsConstraint(space, constraint.Handle) != 0;
        }

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(PostStepFunction))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static void PostStepCallBack(cpSpace handleSpace, voidptr_t handleKey, voidptr_t handleData)
        {
            Space space = FromHandle(handleSpace);
            object key = NativeInterop.FromIntPtr<object>(handleKey);
            PostStepCallbackInfo data = NativeInterop.FromIntPtr<PostStepCallbackInfo>(handleData);

            Action<Space, object, object> callback = data.Callback;

            callback(space, key, data.Data);

            NativeInterop.ReleaseHandle(handleKey);
            NativeInterop.ReleaseHandle(handleData);
        }

        private static readonly PostStepFunction postStepCallBack = PostStepCallBack;

        /// <summary>
        /// Schedule a post-step callback to be called when <see cref="Step"/> finishes. You can
        /// only register one callback per unique value for <paramref name="key"/>. Returns true
        /// only if <paramref name="key"/> has never been scheduled before. It's possible to pass
        /// null for <paramref name="callback"/> if you only want to mark <paramref name="key"/> as
        /// being used.
        /// </summary>
        public bool AddPostStepCallback(Action<Space, object, object> callback, object key, object data)
        {
            var info = new PostStepCallbackInfo(callback, data);

            cpCollisionHandlerPointer dataHandle = NativeInterop.RegisterHandle(info);
            cpCollisionHandlerPointer keyHandle = NativeInterop.RegisterHandle(key);

            return NativeMethods.cpSpaceAddPostStepCallback(space, postStepCallBack.ToFunctionPointer(), keyHandle, dataHandle) != 0;
        }

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(SpacePointQueryFunction))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static void EachPointQuery(cpShape shapeHandle, Vect point, double distance, Vect gradient, voidptr_t data)
        {
            var list = (List<PointQueryInfo>)GCHandle.FromIntPtr(data).Target;

            var shape = Shape.FromHandle(shapeHandle);
            var pointQuery = new PointQueryInfo(shape, point, distance, gradient);

            list.Add(pointQuery);
        }

        private static readonly SpacePointQueryFunction eachPointQuery = EachPointQuery;

        /// <summary>
        /// Get the shapes within a radius of the point location that are part of this space. The
        /// filter is applied to the query and follows the same rules as the collision detection.
        /// If a maxDistance of 0.0 is used, the point must lie inside a shape. Negative
        /// <paramref name="maxDistance"/> is also allowed meaning that the point must be a under a
        /// certain depth within a shape to be considered a match.
        /// </summary>
        /// <param name="point">Where to check for shapes in the space.</param>
        /// <param name="maxDistance">Match only within this distance.</param>
        /// <param name="filter">Only pick shapes matching the filter.</param>
        public PointQueryInfo[] PointQuery(Vect point, double maxDistance, int filter)
        {
            var list = new List<PointQueryInfo>();
            var gcHandle = GCHandle.Alloc(list);

            NativeMethods.cpSpacePointQuery(space, point, maxDistance, (uint)filter, eachPointQuery.ToFunctionPointer(), GCHandle.ToIntPtr(gcHandle));

            gcHandle.Free();
            return list.ToArray();
        }

        /// <summary>
        /// Get the nearest shape within a radius of a point that is part of this space. The filter
        /// is applied to the query and follows the same rules as the collision detection. If a
        /// <paramref name="maxDistance"/> of 0.0 is used, the point must lie inside a shape.
        /// Negative <paramref name="maxDistance"/> is also allowed, meaning that the point must be
        /// under a certain depth within a shape to be considered a match.
        /// </summary>
        /// <param name="point">Where to check for collision in the space.</param>
        /// <param name="maxDistance">Match only within this distance.</param>
        /// <param name="filter">Only pick shapes matching the filter.</param>
        public PointQueryInfo PointQueryNearest(Vect point, double maxDistance, int filter)
        {
            var queryInfo = new cpPointQueryInfo();

            cpShape shape = NativeMethods.cpSpacePointQueryNearest(space, point, maxDistance, (uint)filter, ref queryInfo);
            if (shape == cpCollisionHandlerPointer.Zero)
                return null;

            return PointQueryInfo.FromQueryInfo(queryInfo);
        }

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(SpaceSegmentQueryFunction))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static void EachSegmentQuery(cpShape shapeHandle, Vect point, Vect normal, double alpha, voidptr_t data)
        {
            var list = (List<SegmentQueryInfo>)GCHandle.FromIntPtr(data).Target;

            var shape = Shape.FromHandle(shapeHandle);
            var pointQuery = new SegmentQueryInfo(shape, point, normal, alpha);

            list.Add(pointQuery);
        }

        private static readonly SpaceSegmentQueryFunction eachSegmentQuery = EachSegmentQuery;

        /// <summary>
        /// Get the shapes within a capsule-shaped radius of a line segment that is part of this
        /// space. The filter is applied to the query and follows the same rules as the collision
        /// detection.
        /// </summary>
        public SegmentQueryInfo[] SegmentQuery(Vect start, Vect end, double radius, int filter)
        {
            var list = new List<SegmentQueryInfo>();
            var gcHandle = GCHandle.Alloc(list);

            NativeMethods.cpSpaceSegmentQuery(space, start, end, radius, (uint)filter, eachSegmentQuery.ToFunctionPointer(), GCHandle.ToIntPtr(gcHandle));

            gcHandle.Free();
            return list.ToArray();
        }

        /// <summary>
        /// Get the first shape within a capsule-shaped radius of a line segment that is part of
        /// this space. The filter is applied to the query and follows the same rules as the
        /// collision detection.
        /// </summary>
        public SegmentQueryInfo SegmentQueryFirst(Vect start, Vect end, double radius, int filter)
        {
            var queryInfo = new cpSegmentQueryInfo();

            cpShape shape = NativeMethods.cpSpaceSegmentQueryFirst(space, start, end, radius, (uint)filter, ref queryInfo);
            if (shape == cpCollisionHandlerPointer.Zero)
                return null;

            return SegmentQueryInfo.FromQueryInfo(queryInfo);
        }


#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(SpaceBBQueryFunction))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static void EachBBQuery(cpShape shapeHandle, voidptr_t data)
        {
            var list = (List<Shape>)GCHandle.FromIntPtr(data).Target;

            var shape = Shape.FromHandle(shapeHandle);

            list.Add(shape);
        }

        private static readonly SpaceBBQueryFunction eachBBQuery = EachBBQuery;


        /// <summary>
        /// Get all shapes within the axis-aligned bounding box that are part of this shape. The
        /// filter is applied to the query and follows the same rules as the collision detection.
        /// </summary>
        public Shape[] BoundBoxQuery(BoundingBox bb, int filter)
        {
            var list = new List<Shape>();

            var gcHandle = GCHandle.Alloc(list);

            NativeMethods.cpSpaceBBQuery(space, bb, (uint)filter, eachBBQuery.ToFunctionPointer(), GCHandle.ToIntPtr(gcHandle));

            gcHandle.Free();
            return list.ToArray();
        }

        /// <summary>
        /// Get all bodies in the space.
        /// </summary>
        public Body[] Bodies
        {
            get
            {
                int count = NativeMethods.cpSpaceGetBodyCount(space);
                if (count == 0)
                    return Array.Empty<Body>();

                cpCollisionHandlerPointer ptrBodies = Marshal.AllocHGlobal(cpCollisionHandlerPointer.Size * count);

                NativeMethods.cpSpaceGetBodiesUserDataArray(space, ptrBodies);

                cpCollisionHandlerPointer[] userDataArray = new cpCollisionHandlerPointer[count];

                Marshal.Copy(ptrBodies, userDataArray, 0, count);

                Marshal.FreeHGlobal(ptrBodies);

                Body[] bodies = new Body[count];

                for (int i = 0; i < count; i++)
                {
                    Body b = NativeInterop.FromIntPtr<Body>(userDataArray[i]);
                    bodies[i] = b;
                }

                return bodies;
            }
        }

        /// <summary>
        /// Get dynamic bodies in the space.
        /// </summary>
        public Body[] DynamicBodies
        {
            get
            {
                int count = NativeMethods.cpSpaceGetDynamicBodyCount(space);
                if (count == 0)
                    return Array.Empty<Body>();

                cpCollisionHandlerPointer ptrBodies = Marshal.AllocHGlobal(cpCollisionHandlerPointer.Size * count);
                NativeMethods.cpSpaceGetDynamicBodiesUserDataArray(space, ptrBodies);

                cpCollisionHandlerPointer[] userDataArray = new cpCollisionHandlerPointer[count];

                Marshal.Copy(ptrBodies, userDataArray, 0, count);

                Marshal.FreeHGlobal(ptrBodies);

                Body[] bodies = new Body[count];

                for (int i = 0; i < count; i++)
                {
                    Body b = NativeInterop.FromIntPtr<Body>(userDataArray[i]);
                    bodies[i] = b;
                }

                return bodies;
            }
        }

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(SpaceObjectIteratorFunction))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static void EachShape(cpShape shapeHandle, voidptr_t data)
        {
            var list = (List<Shape>)GCHandle.FromIntPtr(data).Target;

            var shape = Shape.FromHandle(shapeHandle);

            list.Add(shape);
        }

        private static readonly SpaceObjectIteratorFunction eachShape = EachShape;

        /// <summary>
        /// Get all shapes in the space.
        /// </summary>
        public Shape[] Shapes
        {
            get
            {
                var list = new List<Shape>();

                var gcHandle = GCHandle.Alloc(list);

                NativeMethods.cpSpaceEachShape(space, eachShape.ToFunctionPointer(), GCHandle.ToIntPtr(gcHandle));

                gcHandle.Free();

                return list.ToArray();
            }
        }

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(SpaceShapeQueryFunction))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static void ShapeQueryCallback(cpShape shape, cpCollisionHandlerPointer pointsPointer, voidptr_t data)
        {
            var list = (List<ContactPointSet>)GCHandle.FromIntPtr(data).Target;

            cpContactPointSet pointSet = NativeInterop.PtrToStructure<cpContactPointSet>(pointsPointer);

            list.Add(ContactPointSet.FromContactPointSet(pointSet));
        }

        private static readonly SpaceShapeQueryFunction shapeQueryCallback = ShapeQueryCallback;

        /// <summary>
        /// Get all shapes in the space that are overlapping the given shape.
        /// </summary>
        public ContactPointSet[] ShapeQuery(Shape shape)
        {
            var list = new List<ContactPointSet>();
            var gcHandle = GCHandle.Alloc(list);

            _ = NativeMethods.cpSpaceShapeQuery(space, shape.Handle, shapeQueryCallback.ToFunctionPointer(), GCHandle.ToIntPtr(gcHandle));

            gcHandle.Free();
            return list.ToArray();
        }

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(SpaceObjectIteratorFunction))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static void EachConstraint(cpConstraint constraintHandle, voidptr_t data)
        {
            var list = (List<Constraint>)GCHandle.FromIntPtr(data).Target;

            var constraint = Constraint.FromHandle(constraintHandle);

            list.Add(constraint);
        }

        private static readonly SpaceObjectIteratorFunction eachConstraint = EachConstraint;

        /// <summary>
        /// Get all constraints in the space.
        /// </summary>
        public Constraint[] Constraints
        {
            get
            {
                var list = new List<Constraint>();

                var gcHandle = GCHandle.Alloc(list);

                NativeMethods.cpSpaceEachConstraint(space, eachConstraint.ToFunctionPointer(), GCHandle.ToIntPtr(gcHandle));

                gcHandle.Free();

                return list.ToArray();
            }
        }

        /// <summary>
        /// Update the collision detection info for the static shapes in the space.
        /// </summary>
        public void ReindexStatic()
        {
            NativeMethods.cpSpaceReindexStatic(space);
        }

        /// <summary>
        /// Update the collision detection data for a specific shape in the space.
        /// </summary>
        public void ReindexShape(Shape shape)
        {
            NativeMethods.cpSpaceReindexShape(space, shape.Handle);
        }

        /// <summary>
        /// Update the collision detection data for all shapes attached to a body.
        /// </summary>
        public void ReindexShapesForBody(Body body)
        {
            NativeMethods.cpSpaceReindexShapesForBody(space, body.Handle);
        }

        /// <summary>
        /// Switch the space to use a spatial hash as its spatial index.
        /// </summary>
        public void UseSpatialHash(double dim, int count)
        {
            NativeMethods.cpSpaceUseSpatialHash(space, dim, count);
        }

        /// <summary>
        /// Update the space for the given time step. Using a fixed time step is highly recommended.
        /// Doing so will increase the efficiency of the contact persistence, requiring an order of
        /// magnitude fewer iterations to resolve the collisions in the usual case. It is not the
        /// same to call step 10 times with a dt of 0.1, or 100 times with a dt of 0.01 even if the
        /// end result is that the simulation moved forward 100 units. Performing multiple calls
        /// with a smaller dt creates a more stable and accurate simulation. Therefore, it sometimes
        /// makes sense to have a little for loop around the step call.
        /// </summary>
#pragma warning disable CA1716
        public virtual void Step(double dt)
        {
            NativeMethods.cpSpaceStep(space, dt);
        }
#pragma warning restore CA1716

        /// <summary>
        /// Draw all objects in the space for debugging purposes.
        /// </summary>
        public void DebugDraw(IDebugDraw debugDraw)
        {
            DebugDraw(debugDraw, DebugDrawFlags.All, DebugDrawColors.Default);
        }

        /// <summary>
        /// Draw all objects in the space for debugging purposes using flags.
        /// </summary>
        public void DebugDraw(IDebugDraw debugDraw, DebugDrawFlags flags)
        {
            DebugDraw(debugDraw, flags, DebugDrawColors.Default);
        }

        /// <summary>
        /// Draw all objects in the space for debugging purposes using flags and colors.
        /// </summary>
        public void DebugDraw(IDebugDraw debugDraw, DebugDrawFlags flags, DebugDrawColors colors)
        {
            var debugDrawOptions = new cpSpaceDebugDrawOptions();
            cpCollisionHandlerPointer debugDrawOptionsPointer = debugDrawOptions.AcquireDebugDrawOptions(debugDraw, flags, colors);

            NativeMethods.cpSpaceDebugDraw(space, debugDrawOptionsPointer);

            debugDrawOptions.ReleaseDebugDrawOptions(debugDrawOptionsPointer);
        }

    }
}

#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore IDE0055 // Some bug on VS