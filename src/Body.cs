using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if __IOS__ || __TVOS__ || __WATCHOS__
using ObjCRuntime;
#endif

using cpBody = System.IntPtr;
using cpArbiter = System.IntPtr;
using cpConstraint = System.IntPtr;
using cpShape = System.IntPtr;
using cpSpace = System.IntPtr;
using cpDataPointer = System.IntPtr;
using System.Diagnostics;

namespace ChipmunkBinding
{
    /// <summary>
    /// Mass and moment are ignored when body_type is Kinematic or Static.
    /// Guessing the mass for a body is usually fine, but guessing a moment of inertia
    /// can lead to a very poor simulation so it’s recommended to use Chipmunk’s moment
    /// calculations to estimate the moment for you.
    /// </summary>
    public class Body : IDisposable
    {
        readonly cpBody body;

        public cpBody Handle => body;
        /// <summary>
        /// Create a Dynamic Body with no mass and no moment
        /// </summary>
        public Body() : this(BodyType.Dinamic)
        {
        }

        internal Body(cpBody handle)
        {
            body = handle;
            RegisterUserData();
        }

        void RegisterUserData()
        {
            cpDataPointer pointer = NativeInterop.RegisterHandle(this);
            NativeMethods.cpBodySetUserData(body, pointer);
        }

        void ReleaseUserData()
        {
            cpDataPointer pointer = NativeMethods.cpBodyGetUserData(body);
            NativeInterop.ReleaseHandle(pointer);
        }

        public static Body FromHandle(cpBody body)
        {
            cpDataPointer handle = NativeMethods.cpBodyGetUserData(body);
            return NativeInterop.FromIntPtr<Body>(handle);
        }

        public static Body FromHandleSafe(cpBody space)
        {
            if (space == IntPtr.Zero)
                return null;
            return FromHandle(space);
        }

        /// <summary>
        ///  Create a Body of the type (Dinamic, Kinematic, Static)
        /// </summary>
        /// <param name="type"></param>
        public Body(BodyType type)
        {
            body = InitializeBody(type);
            RegisterUserData();
        }

        public Body(double mass, double moment):this(mass, moment, BodyType.Dinamic)
        {
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mass">Mass of the body</param>
        /// <param name="moment">Moment of body</param>
        /// <param name="type">Type (Dinamic, Kinematic, Static)</param>
        public Body(double mass, double moment, BodyType type)
        {
            body = InitializeBody(type);
            NativeMethods.cpBodySetMass(body, mass);
            NativeMethods.cpBodySetMoment(body, moment);
            RegisterUserData();
        }

        private static cpBody InitializeBody(BodyType type)
        {
            if (type == BodyType.Kinematic)
                return NativeMethods.cpBodyNewKinematic();
            if (type == BodyType.Static)
                return NativeMethods.cpBodyNewStatic();

            return NativeMethods.cpBodyNew(0.0, 0.0);
        }

        public void Free()
        {
            var space = Space;

            if (space != null)
                space.Remove(this);

            ReleaseUserData();
            NativeMethods.cpBodyFree(body);
        }

        protected virtual void Dispose(bool dispose)
        {
            if (!dispose)
            {
                Debug.WriteLine("Disposing body {0} on finalizer... (consider Dispose explicitly)", body);
            }
            Free();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Properties

        /// <summary>
        /// Rotation of the body in radians. When changing the rotation you may also want to call cpSpaceReindexShapesForBody() to update the collision detection information for the attached shapes if plan to make any queries against the space. A body rotates around its center of gravity, not its position.
        /// </summary>
        public double Angle
        {
            get => NativeMethods.cpBodyGetAngle(body);
            set => NativeMethods.cpBodySetAngle(body, value);
        }

        /// <summary>
        /// Type of the body (Dynamic, Kinematic, Static). 
        /// </summary>
        public BodyType Type
        {
            get => (BodyType)NativeMethods.cpBodyGetType(body);
            set => NativeMethods.cpBodySetType(body, (int)value);
        }

        /// <summary>
        /// Mass of the rigid body. Mass does not have to be expressed in any particular units, but relative masses should be consistent. 
        /// </summary>
        public double Mass
        {
            get => NativeMethods.cpBodyGetMass(body);
            set => NativeMethods.cpBodySetMass(body, value);
        }

        /// <summary>
        /// Moment of inertia of the body. The mass tells you how hard it is to push an object, the MoI tells you how hard it is to spin the object.
        /// Don't try to guess the MoI, use the MomentFor*() functions to try and estimate it. 
        /// </summary>
        public double Moment
        {
            get => NativeMethods.cpBodyGetMoment(body);
            set => NativeMethods.cpBodySetMoment(body, value);
        }

        /// <summary>
        /// The Space this body is currently added to, or null if it is not currently added to a space.
        /// </summary>
        public Space Space
        {
            get
            {
                cpSpace space = NativeMethods.cpBodyGetSpace(body);
                return Space.FromHandleSafe(space);
            }
        }

        /// <summary>
        /// Position of the body. When changing the position you may also want to call Space.ReindexShapesForBody() to update the collision detection information for the attached shapes if plan to make any queries against the space.
        /// </summary>
        public cpVect Position
        {
            get => NativeMethods.cpBodyGetPosition(body);
            set => NativeMethods.cpBodySetPosition(body, value);
        }

        /// <summary>
        /// Location of the center of gravity in body local coordinates. The default value is (0, 0), meaning the center of gravity is the same as the position of the body.
        /// </summary>
        public cpVect CenterOfGravity
        {
            get => NativeMethods.cpBodyGetCenterOfGravity(body);
            set => NativeMethods.cpBodySetCenterOfGravity(body, value);
        }

        /// <summary>
        /// Linear velocity of the center of gravity of the body.
        /// </summary>
        public cpVect Velocity
        {
            get => NativeMethods.cpBodyGetVelocity(body);
            set => NativeMethods.cpBodySetVelocity(body, value);
        }

        /// <summary>
        /// Force applied to the center of gravity of the body. This value is reset for every time step.
        /// </summary>
        public cpVect Force
        {
            get => NativeMethods.cpBodyGetForce(body);
            set => NativeMethods.cpBodySetForce(body, value);
        }

        /// <summary>
        /// The angular velocity of the body in radians per second.
        /// </summary>
        public double AngularVelocity
        {
            get => NativeMethods.cpBodyGetAngularVelocity(body);
            set => NativeMethods.cpBodySetAngularVelocity(body, value);
        }

        /// <summary>
        /// The torque applied to the body.This value is reset for every time step.
        /// </summary>
        public double Torque
        {
            get => NativeMethods.cpBodyGetTorque(body);
            set => NativeMethods.cpBodySetTorque(body, value);
        }

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(BodyArbiterIteratorFunction))]
#endif
        private static void AddEachArbiterToArray(cpBody body, cpArbiter arbiter, IntPtr data)
        {
            var list = (List<Arbiter>)GCHandle.FromIntPtr(data).Target;
            var a = new Arbiter(arbiter);
            list.Add(a);
        }

        private static BodyArbiterIteratorFunction eachArbiterFunc = AddEachArbiterToArray;

        /// <summary>
        /// The rotation vector for the body. Can be used with cpvrotate() or cpvunrotate() to perform fast rotations.
        /// </summary>
        public cpVect Rotation => NativeMethods.cpBodyGetRotation(body);

        public IReadOnlyList<Arbiter> Arbiters
        {
            get
            {
                var list = new List<Arbiter>();
                var gcHandle = GCHandle.Alloc(list);
                NativeMethods.cpBodyEachArbiter(body, eachArbiterFunc.ToFunctionPointer(), GCHandle.ToIntPtr(gcHandle));
                gcHandle.Free();
                return list;
            }
        }

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(BodyArbiterIteratorFunction))]
#endif
        private static void AddEachConstraintToArray(cpBody body, cpConstraint constraint, IntPtr data)
        {
            var list = (List<Constraint>)GCHandle.FromIntPtr(data).Target;
            var c = Constraint.FromHandle(constraint);
            list.Add(c);
        }

        private static BodyConstraintIteratorFunction eachConstraintFunc = AddEachConstraintToArray;

        /// <summary>
        /// All constraints attached to the body
        /// </summary>
        public IReadOnlyList<Constraint> Constraints
        {
            get
            {
                var list = new List<Constraint>();
                var gcHandle = GCHandle.Alloc(list);
                NativeMethods.cpBodyEachConstraint(body, eachConstraintFunc.ToFunctionPointer(), GCHandle.ToIntPtr(gcHandle));
                gcHandle.Free();
                return list.ToArray();
            }
        }

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(BodyShapeIteratorFunction))]
#endif
        private static void AddEachShapeToArray(cpBody body, cpShape shape, IntPtr data)
        {
            var list = (List<Shape>)GCHandle.FromIntPtr(data).Target;
            var s = Shape.FromHandle(shape);
            list.Add(s);
        }

        private static BodyShapeIteratorFunction eachShapeFunc = AddEachShapeToArray;

        /// <summary>
        /// All shapes attached to the body
        /// </summary>
        public IReadOnlyList<Shape> Shapes
        {
            get
            {
                var list = new List<Shape>();
                var gcHandle = GCHandle.Alloc(list);
                NativeMethods.cpBodyEachConstraint(body, eachShapeFunc.ToFunctionPointer(), GCHandle.ToIntPtr(gcHandle));
                gcHandle.Free();
                return list.ToArray();
            }
        }

        /// <summary>
        /// Returns true if body is sleeping.
        /// </summary>
        public bool IsSleeping => NativeMethods.cpBodyIsSleeping(body) != 0;

        // Actions

        /// <summary>
        ///     Reset the idle timer on a body.
        ///     If it was sleeping, wake it and any other bodies it was touching.
        /// </summary>
        public void Activate() => NativeMethods.cpBodyActivate(body);

        /// <summary>
        /// Similar in function to Activate(). Activates all bodies touching body. If filter is not NULL, then only bodies touching through filter will be awoken.
        /// </summary>
        /// <param name="filter"></param>
        public void ActivateStatic(Shape filter) => NativeMethods.cpBodyActivateStatic(body, filter.Handle);

        /// <summary>
        /// Add the local force force to body as if applied from the body local point.
        /// </summary>
        /// <param name="force"></param>
        /// <param name="point"></param>
        public void ApplyForceAtLocalPoint(cpVect force, cpVect point)
        {
            NativeMethods.cpBodyApplyForceAtLocalPoint(body, force, point);
        }

        /// <summary>
        ///     Add the force force to body as if applied from the world point.
        ///     People are sometimes confused by the difference between a force and an impulse.
        ///     An impulse is a very large force applied over a very short period of time.
        ///     Some examples are a ball hitting a wall or cannon firing.
        ///     Chipmunk treats impulses as if they occur instantaneously by adding directly to the velocity of an object.
        ///     Both impulses and forces are affected the mass of an object.
        ///     Doubling the mass of the object will halve the effect.

        /// </summary>
        /// <param name="force"></param>
        /// <param name="point"></param>
        public void ApplyForceAtWorldPoint(cpVect force, cpVect point)
        {
            NativeMethods.cpBodyApplyForceAtWorldPoint(body, force, point);
        }

        /// <summary>
        /// Apply an impulse to a body. Both the impulse and point are expressed in world coordinates.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="impulse"></param>
        /// <param name="point"></param>
        public void ApplyImpulseAtWorldPoint(cpVect impulse, cpVect point)
        {
            NativeMethods.cpBodyApplyImpulseAtWorldPoint(body, impulse, point);
        }

        /// <summary>
        /// Apply an impulse to a body. Both the impulse and point are expressed in body local coordinates.
        /// </summary>
        /// <param name="impulse"></param>
        /// <param name="point"></param>
        public void ApplyImpulseAtLocalPoint(cpVect impulse, cpVect point)
        {
            NativeMethods.cpBodyApplyImpulseAtLocalPoint(body, impulse, point);
        }

        /// <summary>
        /// Forces a body to fall asleep immediately even if it’s in midair. Cannot be called from a callback.
        /// </summary>
        public void Sleep()
        {
            NativeMethods.cpBodySleep(body);
        }

        /// <summary>
        /// When objects in Chipmunk sleep, they sleep as a group of all objects that are touching or jointed together.
        /// When an object is woken up, all of the objects in its group are woken up.
        /// SleepWithGroup() allows you group sleeping objects together. It acts identically to Sleep() if you pass null as
        /// group by starting a new group.
        /// If you pass a sleeping body for group, body will be awoken when group is awoken.
        /// You can use this to initialize levels and start stacks of objects in a pre-sleeping state.
        /// </summary>
        /// <param name="group"></param>
        public void SleepWithGroup(Body group)
        {
            NativeMethods.cpBodySleepWithGroup(body, group != null ? group.Handle : IntPtr.Zero);
        }

        /// <summary>
        /// Set the callback used to update a body's velocity.
        /// Note: The BodyVelocityFunction will be called from the native code.
        /// if you are usingn iOS or tvOS you method will need to be static and 
        /// contain the attribute MonoPInvokeCallback
        /// </summary>
        /// <param name="function"></param>
        public void SetVelocityUpdateFunction(BodyVelocityFunction function)
        {
            NativeMethods.cpBodySetVelocityUpdateFunc(body, function.ToFunctionPointer());
        }

        /// <summary>
        /// Set the callback used to update a body's position.
        /// NOTE: It's not generally recommended to override this unless you call the default position update function.
        /// </summary>
        /// <param name="function"></param>
        public void SetPositionUpdateFunction(BodyPositionFunction function)
        {
            NativeMethods.cpBodySetPositionUpdateFunc(body, function.ToFunctionPointer());
        }

        /// <summary>
        /// Default velocity integration function..
        /// </summary>
        /// <param name="gravity"></param>
        /// <param name="damping"></param>
        /// <param name="dt"></param>
        public void UpdateVelocity(cpVect gravity, double damping, double dt)
        {
            NativeMethods.cpBodyUpdateVelocity(body, gravity, damping, dt);
        }

        /// <summary>
        /// Default position integration function.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="dt"></param>
        public void UpdatePosition(double dt)
        {
            NativeMethods.cpBodyUpdatePosition(body, dt);
        }

        /// <summary>
        /// Convert body relative/local coordinates to absolute/world coordinates.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public cpVect LocalToWorld(cpVect point)
        {
            return NativeMethods.cpBodyLocalToWorld(body, point);
        }

        /// <summary>
        /// Convert body absolute/world coordinates to  relative/local coordinates.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public cpVect WorldToLocal(cpVect point)
        {
            return NativeMethods.cpBodyWorldToLocal(body, point);
        }

        /// <summary>
        /// Get the velocity on a body (in world units) at a point on the body in world coordinates.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public cpVect GetVelocityAtWorldPoint(cpVect point)
        {
            return NativeMethods.cpBodyGetVelocityAtWorldPoint(body, point);
        }

        /// <summary>
        /// Get the velocity on a body (in world units) at a point on the body in local coordinates.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public cpVect GetVelocityAtLocalPoint(cpVect point)
        {
            return NativeMethods.cpBodyGetVelocityAtLocalPoint(body, point);
        }

        /// <summary>
        /// Get the kinetic energy of a body.
        /// </summary>
        public double KineticEnergy => NativeMethods.cpBodyKineticEnergy(body);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mass"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static double MomentForBox(double mass, double width, double height)
        {
            return NativeMethods.cpMomentForBox(mass, width, height);
        }
    }
}
