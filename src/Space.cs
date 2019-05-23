using System;

using cpConstraint = System.IntPtr;
using cpSpace = System.IntPtr;
using cpShape = System.IntPtr;
using cpDataPointer = System.IntPtr;
using cpBody = System.IntPtr;
using cpCollisionHandler = System.IntPtr;
using cpCollisionType = System.UIntPtr;
using voidptr_t = System.IntPtr;
using System.Collections.Generic;

using System.Runtime.InteropServices;
using System.Diagnostics;

#if __IOS__ || __TVOS__ || __WATCHOS__
using ObjCRuntime;
#endif

namespace ChipmunkBinding
{
    /// <summary>
    /// Spaces in Chipmunk are the basic unit of simulation. You add rigid bodies, shapes, and constraints to the space and then step them all forward through time together.
    /// </summary>
    public class Space : IDisposable
    {
        readonly cpSpace space;

        public cpSpace Handle => space;

        /// <summary>
        /// Create a new Space object
        /// </summary>
        public Space()
        {
            space = NativeMethods.cpSpaceNew();
            RegisterUserData();
        }

        /// <summary>
        /// Destroys and frees
        /// </summary>
        public void Free()
        {
            ReleaseUserData();
            NativeMethods.cpSpaceFree(space);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                Debug.WriteLine("Disposing space {0} on finalizer... (consider Dispose explicitly)", space);
            }
            Free();
        }
        /// <summary>
        /// Disposes the Space object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Space()
        {
            Dispose(false);
        }

        void RegisterUserData()
        {
            cpDataPointer pointer = HandleInterop.RegisterHandle(this);
            NativeMethods.cpSpaceSetUserData(space, pointer);
        }

        void ReleaseUserData()
        {
            cpDataPointer pointer = NativeMethods.cpSpaceGetUserData(space);
            HandleInterop.ReleaseHandle(pointer);
        }

        /// <summary>
        /// Get a Space object from nativa cpSpace handle
        /// </summary>
        /// <param name="space"></param>
        /// <returns></returns>
        public static Space FromHandle(cpSpace space)
        {
            cpDataPointer handle = NativeMethods.cpSpaceGetUserData(space);
            return HandleInterop.FromIntPtr<Space>(handle);
        }

        /// <summary>
        /// Get a Space object from nativa cpSpace handle, but return null if the handle == 0
        /// </summary>
        /// <param name="space"></param>
        /// <returns></returns>
        public static Space FromHandleSafe(cpSpace space)
        {
            if (space == IntPtr.Zero)
                return null;
            return FromHandle(space);
        }

        // Properties
        /// <summary>
        /// Number of iterations to use in the impulse solver to solve contacts and other constraints. 
        /// </summary>
        public int Iterations
        {
            get => NativeMethods.cpSpaceGetIterations(space);
            set => NativeMethods.cpSpaceSetIterations(space, value);
        }

        /// <summary>
        /// Gravity to pass to rigid bodies when integrating velocity. 
        /// </summary>
        public cpVect Gravity
        {
            get => NativeMethods.cpSpaceGetGravity(space);
            set => NativeMethods.cpSpaceSetGravity(space, value);
        }

        /// <summary>
        /// Damping rate expressed as the fraction of velocity bodies retain each second
        /// A value of 0.9 would mean that each body's velocity will drop 10% per second. The default value is 1.0, meaning no damping is applied.
        /// Note: This damping value is different than those of cpDampedSpring and cpDampedRotarySpring
        /// </summary>
        public double Damping
        {
            get => NativeMethods.cpSpaceGetDamping(space);
            set => NativeMethods.cpSpaceSetDamping(space, value);
        }

        /// <summary>
        /// Speed threshold for a body to be considered idle.
        /// The default value of 0 means to let the space guess a good threshold based on gravity. 
        /// </summary>
        public double IdleSpeedThreshold
        {
            get => NativeMethods.cpSpaceGetIdleSpeedThreshold(space);
            set => NativeMethods.cpSpaceSetIdleSpeedThreshold(space, value);
        }

        /// <summary>
        /// Time a group of bodies must remain idle in order to fall asleep
        /// Enabling sleeping also implicitly enables the the contact graph. The default value of INFINITY disables the sleeping algorithm. 
        /// </summary>
        public double SleepTimeThreshold
        {
            get => NativeMethods.cpSpaceGetSleepTimeThreshold(space);
            set => NativeMethods.cpSpaceSetSleepTimeThreshold(space, value);
        }

        /// <summary>
        /// Amount of encouraged penetration between colliding shapes
        /// Used to reduce oscillating contacts and keep the collision cache warm. Defaults to 0.1. If you have poor simulation quality, increase this number as much as possible without allowing visible amounts of overlap. 
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
        /// Number of frames that contact information should persist.
        /// Defaults to 3. There is probably never a reason to change this value.
        /// </summary>
        public int CollisionPersistence
        {
            get => (int)NativeMethods.cpSpaceGetCollisionPersistence(space);
            set => NativeMethods.cpSpaceSetCollisionPersistence(space, (uint)value);
        }

        /// <summary>
        /// The Space provided static body for a given cpSpace.
        /// </summary>
        public Body StaticBody
        {
            get
            {
                cpBody bodyHandle = NativeMethods.cpSpaceGetStaticBody(space);
                cpDataPointer gcHandle = NativeMethods.cpBodyGetUserData(bodyHandle);
                if (gcHandle != IntPtr.Zero)
                    return HandleInterop.FromIntPtr<Body>(gcHandle);

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

        // Actions

        // Collision Handlers

        /// <summary>
        /// Create or return the existing collision handler that is called for all collisions that are not handled by a more specific collision handler.
        /// </summary>
        /// <returns></returns>
        public CollisionHandler AddDefaultCollisionHandler()
        {
            cpCollisionHandler handler = NativeMethods.cpSpaceAddDefaultCollisionHandler(space);
            return new CollisionHandler(handler);
        }

        /// <summary>
        /// Create or return the existing collision handler for the specified pair of collision types.
        /// If wildcard handlers are used with either of the collision types, it's the responibility of the custom handler to invoke the wildcard handlers.
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="typeB"></param>
        /// <returns></returns>
        public CollisionHandler AddCollisionHandler(int typeA, int typeB)
        {
            cpCollisionHandler handler = NativeMethods.cpSpaceAddCollisionHandler(space, (cpCollisionType) typeA, (cpCollisionType) typeB);
            return new CollisionHandler(handler);
        }

        /// <summary>
        /// Create or return the existing wildcard collision handler for the specified type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public CollisionHandler AddWildcardHandler(int type)
        {
            cpCollisionHandler handler = NativeMethods.cpSpaceAddWildcardHandler(space, (cpCollisionType)type);
            return new CollisionHandler(handler);
        }

        /// <summary>
        /// Add a rigid body to the simulation.
        /// </summary>
        /// <param name="shape"></param>
        public void AddShape(Shape shape)
        {
            NativeMethods.cpSpaceAddShape(space, shape.Handle);
        }

        /// <summary>
        /// Add a rigid body to the simulation. 
        /// </summary>
        /// <param name="body"></param>
        public void AddBody(Body body)
        {
            NativeMethods.cpSpaceAddBody(space, body.Handle);
        }

        /// <summary>
        /// Add a constraint to the simulation.
        /// </summary>
        /// <param name="constraint"></param>
        public void AddConstraint(Constraint constraint)
        {
            NativeMethods.cpSpaceAddConstraint(space, constraint.Handle);
        }


        /// <summary>
        /// Remove a collision shape from the simulation.
        /// </summary>
        /// <param name="shape"></param>
        public void Remove(Shape shape)
        {
            NativeMethods.cpSpaceRemoveShape(space, shape.Handle);
        }

        /// <summary>
        /// Remove a rigid body from the simulation.
        /// </summary>
        /// <param name="body"></param>
        public void Remove(Body body)
        {
            NativeMethods.cpSpaceRemoveBody(space, body.Handle);
        }

        /// <summary>
        /// Remove a constraint from the simulation.
        /// </summary>
        /// <param name="constraint"></param>
        public void Remove(Constraint constraint)
        {
            NativeMethods.cpSpaceRemoveConstraint(space, constraint.Handle);
        }

        /// <summary>
        /// Test if a collision shape has been added to the space.
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public bool Contains(Shape shape)
        {
            return NativeMethods.cpSpaceContainsShape(space, shape.Handle) != 0;
        }

        /// <summary>
        /// Test if a rigid body has been added to the space.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public bool Contains(Body body)
        {
            return NativeMethods.cpSpaceContainsBody(space, body.Handle) != 0;
        }

        /// <summary>
        /// Test if a constraint has been added to the space.
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public bool Contains(Constraint constraint)
        {
            return NativeMethods.cpSpaceContainsConstraint(space, constraint.Handle) != 0;
        }

   #if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(PostStepFunction))]
#endif
        private static void PostStepCallBack(cpSpace handleSpace, voidptr_t handleKey, voidptr_t handleData)
        {
            var space = Space.FromHandle(handleSpace);
            var key = HandleInterop.FromIntPtr<object>(handleKey);
            var data = HandleInterop.FromIntPtr<PostStepCallbackInfo>(handleData);

            Action<Space, object, object> callback = data.Callback;

            callback(space, key, data.Data);

            HandleInterop.ReleaseHandle(handleKey);
            HandleInterop.ReleaseHandle(handleData);
        }

        private static PostStepFunction postStepCallBack = PostStepCallBack;

        /// <summary>
        /// Schedule a post-step callback to be called when Step() finishes.
        /// You can only register one callback per unique value for @c key.
        /// Returns true only if @c key has never been scheduled before.
        /// It's possible to pass @c NULL for @c func if you only want to mark @c key as being used.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AddPostStepCallback(Action<Space, object, object> callback, object key, object data)
        {
            var info = new PostStepCallbackInfo(callback, data);

            IntPtr dataHandle = HandleInterop.RegisterHandle(info);
            IntPtr keyHandle = HandleInterop.RegisterHandle(key);

            return NativeMethods.cpSpaceAddPostStepCallback(space, postStepCallBack.ToFunctionPointer(), keyHandle, dataHandle) != 0;
        }

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(SpacePointQueryFunction))]
#endif
        private static void EachPointQuery(cpShape shapeHandle, cpVect point, double distance, cpVect gradient, voidptr_t data)
        {
            var list = (List<PointQueryInfo>)GCHandle.FromIntPtr(data).Target;

            var shape = Shape.FromHandle(shapeHandle);
            var pointQuery = new PointQueryInfo(shape, point, distance, gradient);

            list.Add(pointQuery);
        }

        private static SpacePointQueryFunction eachPointQuery = EachPointQuery;

        /// <summary>
        /// Query space at point for shapes within the given distance range.
        /// The filter is applied to the query and follows the same rules as the collision detection. If a maxDistance of 0.0 is used, the point must lie inside a shape. Negative max_distance is also allowed meaning that the point must be a under a certain depth within a shape to be considered a match.
        /// </summary>
        /// <param name="point">Where to check for collision in the Space</param>
        /// <param name="maxDistance">Match only within this distance</param>
        /// <param name="filter">Only pick shapes matching the filter</param>
        /// <returns></returns>
        public IReadOnlyCollection<PointQueryInfo> PointQuery(cpVect point, double maxDistance, ShapeFilter filter)
        {
            var list = new List<PointQueryInfo>();
            var gcHandle = GCHandle.Alloc(list);
            var cpFilter = cpShapeFilter.FromShapeFilter(filter);

            NativeMethods.cpSpacePointQuery(space, point, maxDistance, cpFilter, eachPointQuery.ToFunctionPointer(), GCHandle.ToIntPtr(gcHandle));

            gcHandle.Free();
            return list;
        }

        /// <summary>
        /// Query space at point the nearest shape within the given distance range.
        /// The filter is applied to the query and follows the same rules as the collision detection. If a maxDistance of 0.0 is used, the point must lie inside a shape. Negative max_distance is also allowed meaning that the point must be a under a certain depth within a shape to be considered a match.
        /// </summary>
        /// <param name="point">Where to check for collision in the Space</param>
        /// <param name="maxDistance">Match only within this distance</param>
        /// <param name="filter">Only pick shapes matching the filter</param>
        /// <returns></returns>
        public PointQueryInfo PointQueryNearest(cpVect point, double maxDistance, ShapeFilter filter)
        {
            var queryInfo = new cpPointQueryInfo();
            var cpFilter = cpShapeFilter.FromShapeFilter(filter);
            
            cpShape shape = NativeMethods.cpSpacePointQueryNearest(space, point, maxDistance, cpFilter, ref queryInfo);
            if (shape == IntPtr.Zero)
                return null;
            
            return PointQueryInfo.FromQueryInfo(queryInfo);
        }

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(SpaceBBQueryFunction))]
#endif
        private static void EachBBQuery(cpShape shapeHandle, voidptr_t data)
        {
            var list = (List<Shape>)GCHandle.FromIntPtr(data).Target;

            var shape = Shape.FromHandle(shapeHandle);

            list.Add(shape);
        }

        private static SpaceBBQueryFunction eachBBQuery = EachBBQuery;


        /// <summary>
        /// Query space to find all shapes near bb.
        /// </summary>
        /// <param name="bb"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IReadOnlyCollection<Shape> BoundBoxQuery(cpBB bb, ShapeFilter filter)
        {
            var list = new List<Shape>();

            var gcHandle = GCHandle.Alloc(list);
            var cpFilter = cpShapeFilter.FromShapeFilter(filter);

            NativeMethods.cpSpaceBBQuery(space, bb, cpFilter, eachBBQuery.ToFunctionPointer(), GCHandle.ToIntPtr(gcHandle));

            gcHandle.Free();
            return list;
        }



#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(SpaceConstraintIteratorFunction))]
#endif
        private static void EachConstraint(cpConstraint constraintHandle, voidptr_t data)
        {
            var list = (List<Constraint>)GCHandle.FromIntPtr(data).Target;

            var constraint = Constraint.FromHandle(constraintHandle);

            list.Add(constraint);
        }

        private static SpaceConstraintIteratorFunction eachConstraint = EachConstraint;


        /// <summary>
        /// Return all constraints from Space.
        /// </summary>
        public IReadOnlyCollection<Constraint> Constraints
        {
            get
            {
                var list = new List<Constraint>();

                var gcHandle = GCHandle.Alloc(list);

                NativeMethods.cpSpaceEachConstraint(space, eachConstraint.ToFunctionPointer(), GCHandle.ToIntPtr(gcHandle));

                gcHandle.Free();

                return list;
            }
        }

        /// <summary>
        /// Update the collision detection info for the static shapes in the space.
        /// </summary>
        /// <param name="shape"></param>
        public void ReindexStatic(Shape shape)
        {
            NativeMethods.cpSpaceReindexStatic(space);
        }

        /// <summary>
        /// Update the collision detection data for a specific shape in the space.
        /// </summary>
        /// <param name="shape"></param>
        public void ReindexShape(Shape shape)
        {
            NativeMethods.cpSpaceReindexShape(space, shape.Handle);
        }

        /// <summary>
        /// Update the collision detection data for all shapes attached to a body.
        /// </summary>
        /// <param name="body"></param>
        public void ReindexShapesForBody(Body body)
        {
            NativeMethods.cpSpaceReindexShapesForBody(space, body.Handle);
        }

        /// <summary>
        /// Switch the space to use a spatial has as it's spatial index.
        /// </summary>
        /// <param name="dim"></param>
        /// <param name="count"></param>
        public void UseSpatialHash(double dim, int count)
        {
            NativeMethods.cpSpaceUseSpatialHash(space, dim, count);
        }

        /// <summary>
        /// Update the space for the given time step.
        /// Using a fixed time step is highly recommended. Doing so will increase the efficiency of the contact persistence, requiring an order of magnitude fewer iterations to resolve the collisions in the usual case.
        /// It is not the same to call step 10 times with a dt of 0.1 and calling it 100 times with a dt of 0.01 even if the end result is that the simulation moved forward 100 units. Performing multiple calls with a smaller dt creates a more stable and accurate simulation. Therefor it sometimes make sense to have a little for loop around the step call, like in this example:
        /// </summary>
        /// <param name="dt">Time step length</param>
        public void Step(double dt)
        {
            NativeMethods.cpSpaceStep(space, dt);
        }





    }
}
