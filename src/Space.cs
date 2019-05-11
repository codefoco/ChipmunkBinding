using System;

using cpSpace = System.IntPtr;
using cpDataPointer = System.IntPtr;
using cpBody = System.IntPtr;
using cpCollisionHandler = System.IntPtr;
using cpCollisionType = System.UIntPtr;

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

        /// <summary>
        /// Disposes the Space object
        /// </summary>
        public void Dispose()
        {
            Free();
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



    }
}
