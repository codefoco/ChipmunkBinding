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
    /// Mass and moment are ignored when <see cref="BodyType"/> is <see cref="BodyType.Kinematic"/>
    /// or <see cref="BodyType.Static"/>. Guessing the mass for a body is usually fine, but guessing
    /// a moment of inertia can lead to a very poor simulation. It’s recommended to use Chipmunk’s
    /// moment-calculating functions to estimate the moment for you.
    /// </summary>
    public class Body : IDisposable
    {
#pragma warning disable IDE0032
        private readonly cpBody body;
#pragma warning restore IDE0032

        /// <summary>
        /// The native handle.
        /// </summary>
        public cpBody Handle => body;

        /// <summary>
        /// Create a Dynamic Body with no mass and no moment.
        /// </summary>
        public Body()
            : this(BodyType.Dynamic)
        {
        }

        internal Body(cpBody handle)
        {
            body = handle;
            RegisterUserData();
        }

        /// <summary>
        ///  Create a <see cref="Body"/> of the given <see cref="BodyType"/>.
        /// </summary>
        public Body(BodyType type)
        {
            body = InitializeBody(type);
            RegisterUserData();
        }

        /// <summary>
        /// Creates a body with the given mass and moment.
        /// </summary>
        public Body(double mass, double moment) : this(mass, moment, BodyType.Dynamic)
        {
        }

        /// <summary>
        /// Creates a body with the given mass and moment, of the give <see cref="BodyType"/>.
        /// </summary>
        public Body(double mass, double moment, BodyType type)
        {
            body = InitializeBody(type);
            NativeMethods.cpBodySetMass(body, mass);
            NativeMethods.cpBodySetMoment(body, moment);
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

        /// <summary>
        /// Get a <see cref="Body"/> object from a native cpBody handle.
        /// </summary>
        public static Body FromHandle(cpBody body)
        {
            cpDataPointer handle = NativeMethods.cpBodyGetUserData(body);
            return NativeInterop.FromIntPtr<Body>(handle);
        }

        /// <summary>
        /// Get the managed <see cref="Body"/> object from the native handle.
        /// </summary>
        public static Body FromHandleSafe(cpBody nativeBodyHandle)
        {
            if (nativeBodyHandle == IntPtr.Zero)
            {
                return null;
            }

            return FromHandle(nativeBodyHandle);
        }

        private static cpBody InitializeBody(BodyType type)
        {
            if (type == BodyType.Kinematic)
            {
                return NativeMethods.cpBodyNewKinematic();
            }

            if (type == BodyType.Static)
            {
                return NativeMethods.cpBodyNewStatic();
            }

            return NativeMethods.cpBodyNew(0.0, 0.0);
        }

        /// <summary>
        /// Destroy and free the body.
        /// </summary>
        public void Free()
        {
            var space = Space;

            if (space != null)
                space.RemoveBody(this);

            ReleaseUserData();
            NativeMethods.cpBodyFree(body);
        }

        /// <summary>
        /// Dispose the body.
        /// </summary>
        protected virtual void Dispose(bool dispose)
        {
            if (!dispose)
            {
                Debug.WriteLine("Disposing body {0} on finalizer... (consider Dispose explicitly)", body);
            }

            Free();
        }

        /// <summary>
        /// Dispose the body.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Properties

        /// <summary>
        /// Arbitrary user data.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Rotation of the body in radians. When changing the rotation, you may also want to call
        /// <see cref="Space.ReindexShapesForBody"/> to update the collision detection information
        /// for the attached shapes if you plan to make any queries against the space. A body
        /// rotates around its center of gravity, not its position.
        /// </summary>
        public double Angle
        {
            get => NativeMethods.cpBodyGetAngle(body);
            set => NativeMethods.cpBodySetAngle(body, value);
        }

        /// <summary>
        /// Set body position and rotation angle (in radians)
        /// </summary>
        /// <param name="position"></param>
        /// <param name="angle"></param>
        public void SetTransform(Vect position, double angle)
        {
            NativeMethods.cpBodySetTransform(body, position, angle);
        }

        /// <summary>
        /// Get body position and rotation angle (in radians)
        /// </summary>
        /// <param name="position"></param>
        /// <param name="angle"></param>
        public void GetTransform(out Vect position, out double angle)
        {
            NativeMethods.cpBodyGetTransform(body, out position, out angle);
        }

        /// <summary>
        /// The way the body behaves in physics simulations.
        /// </summary>
        public BodyType Type
        {
            get => (BodyType)NativeMethods.cpBodyGetType(body);
            set => NativeMethods.cpBodySetType(body, (int)value);
        }

        /// <summary>
        /// Mass of the rigid body. Mass does not have to be expressed in any particular units, but
        /// relative masses should be consistent. 
        /// </summary>
        public double Mass
        {
            get => NativeMethods.cpBodyGetMass(body);
            set => NativeMethods.cpBodySetMass(body, value);
        }

        /// <summary>
        /// Moment of inertia of the body. The mass tells you how hard it is to push an object,
        /// the MoI tells you how hard it is to spin the object. Don't try to guess the MoI, use the
        /// MomentFor*() functions to estimate it, or the physics may behave strangely. 
        /// </summary>
        public double Moment
        {
            get => NativeMethods.cpBodyGetMoment(body);
            set => NativeMethods.cpBodySetMoment(body, value);
        }

        /// <summary>
        /// Get the space this body is associated with, or null if it is not currently associated.
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
        /// Position of the body. When changing the position, you may also want to call
        /// <see cref="Space.ReindexShapesForBody"/> to update the collision detection information
        /// for the attached shapes if you plan to make any queries against the space.
        /// </summary>
        public Vect Position
        {
            get => NativeMethods.cpBodyGetPosition(body);
            set => NativeMethods.cpBodySetPosition(body, value);
        }

        /// <summary>
        /// Location of the center of gravity in body-local coordinates. The default value is
        /// (0, 0), meaning the center of gravity is the same as the position of the body.
        /// </summary>
        public Vect CenterOfGravity
        {
            get => NativeMethods.cpBodyGetCenterOfGravity(body);
            set => NativeMethods.cpBodySetCenterOfGravity(body, value);
        }

        /// <summary>
        /// Linear velocity of the center of gravity of the body.
        /// </summary>
        public Vect Velocity
        {
            get => NativeMethods.cpBodyGetVelocity(body);
            set => NativeMethods.cpBodySetVelocity(body, value);
        }

        /// <summary>
        /// Force applied to the center of gravity of the body. This value is reset for every time
        /// step.
        /// </summary>
        public Vect Force
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
        /// The torque applied to the body. This value is reset for every time step.
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
        public Vect Rotation => NativeMethods.cpBodyGetRotation(body);

        /// <summary>
        /// Get the list of body Arbiters
        /// </summary>
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
                NativeMethods.cpBodyEachShape(body, eachShapeFunc.ToFunctionPointer(), GCHandle.ToIntPtr(gcHandle));
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
        public void ApplyForceAtLocalPoint(Vect force, Vect point)
        {
            NativeMethods.cpBodyApplyForceAtLocalPoint(body, force, point);
        }

        /// <summary>
        /// Apply torque.
        /// </summary>
        /// <param name="torque"></param>
        public void ApplyTorque(double torque)
        {
            NativeMethods.cpBodyApplyTorque(body, torque);
        }

        /// <summary>
        /// Apply angular impulse.
        /// </summary>
        /// <param name="impulse"></param>
        public void ApplyAngularImpulse(double impulse)
        {
            NativeMethods.cpBodyApplyAngularImpulse(body, impulse);
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
        public void ApplyForceAtWorldPoint(Vect force, Vect point)
        {
            NativeMethods.cpBodyApplyForceAtWorldPoint(body, force, point);
        }

        /// <summary>
        /// Apply an impulse to a body. Both the impulse and point are expressed in world coordinates.
        /// </summary>
        /// <param name="impulse"></param>
        /// <param name="point"></param>
        public void ApplyImpulseAtWorldPoint(Vect impulse, Vect point)
        {
            NativeMethods.cpBodyApplyImpulseAtWorldPoint(body, impulse, point);
        }

        /// <summary>
        /// Apply an impulse to a body. Both the impulse and point are expressed in body local coordinates.
        /// </summary>
        /// <param name="impulse"></param>
        /// <param name="point"></param>
        public void ApplyImpulseAtLocalPoint(Vect impulse, Vect point)
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

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(BodyVelocityFunction))]
#endif
        private static void BodyVelocityFunctionCallback(cpBody bodyHandle, Vect gravity, double damping, double dt)
        {
            var body = FromHandle(bodyHandle);

            body.velocityUpdateFunction(body, gravity, damping, dt);
        }

        private static BodyVelocityFunction BodyVelocityFunctionCallbackDelegate = BodyVelocityFunctionCallback;

        private Action<Body, Vect, double, double> velocityUpdateFunction;
        /// <summary>
        /// Set the callback used to update a body's velocity.
        /// Parameters: body, gravity, damping and deltaTime
        /// </summary>
        public Action<Body, Vect, double, double>  VelocityUpdateFunction
        {
            get => velocityUpdateFunction;
            set
            {
                velocityUpdateFunction = value;

                IntPtr callbackPointer;

                if (value == null)
                    callbackPointer = NativeMethods.cpBodyGetDefaultVelocityUpdateFunc();
                else
                    callbackPointer = BodyVelocityFunctionCallbackDelegate.ToFunctionPointer();

                NativeMethods.cpBodySetVelocityUpdateFunc(body, callbackPointer);
            }
        }

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(BodyPositionFunction))]
#endif
        private static void BodyPositionFunctionCallback(cpBody bodyHandle, double dt)
        {
            var body = FromHandle(bodyHandle);

            body.positionUpdateFunction(body, dt);
        }

        private static BodyPositionFunction BodyUpdateFunctionCallbackDelegate = BodyPositionFunctionCallback;

        private Action<Body, double> positionUpdateFunction;

        /// <summary>
        /// Set the callback used to update a body's position.
        /// Parameters: body, deltaTime
        /// </summary>
        public Action<Body, double> PositionUpdateFunction
        {
            get => positionUpdateFunction;
            set
            {
                positionUpdateFunction = value;

                IntPtr callbackPointer;

                if (value == null)
                    callbackPointer = NativeMethods.cpBodyGetDefaultPositionUpdateFunc();
                else
                    callbackPointer = BodyUpdateFunctionCallbackDelegate.ToFunctionPointer();

                NativeMethods.cpBodySetPositionUpdateFunc(body, callbackPointer);
            }
        }

        /// <summary>
        /// Default velocity integration function..
        /// </summary>
        /// <param name="gravity"></param>
        /// <param name="damping"></param>
        /// <param name="dt"></param>
        public void UpdateVelocity(Vect gravity, double damping, double dt)
        {
            NativeMethods.cpBodyUpdateVelocity(body, gravity, damping, dt);
        }

        /// <summary>
        /// Default position integration function.
        /// </summary>
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
        public Vect LocalToWorld(Vect point)
        {
            return NativeMethods.cpBodyLocalToWorld(body, point);
        }

        /// <summary>
        /// Convert body absolute/world coordinates to  relative/local coordinates.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vect WorldToLocal(Vect point)
        {
            return NativeMethods.cpBodyWorldToLocal(body, point);
        }

        /// <summary>
        /// Get the velocity on a body (in world units) at a point on the body in world coordinates.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vect GetVelocityAtWorldPoint(Vect point)
        {
            return NativeMethods.cpBodyGetVelocityAtWorldPoint(body, point);
        }

        /// <summary>
        /// Get the velocity on a body (in world units) at a point on the body in local coordinates.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vect GetVelocityAtLocalPoint(Vect point)
        {
            return NativeMethods.cpBodyGetVelocityAtLocalPoint(body, point);
        }

        /// <summary>
        /// Get the kinetic energy of a body.
        /// </summary>
        public double KineticEnergy => NativeMethods.cpBodyKineticEnergy(body);

        /// <summary>
        /// Calculate the moment of inertia for a solid box centered on the body.
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static double MomentForBox(double mass, double width, double height)
        {
            return NativeMethods.cpMomentForBox(mass, width, height);
        }

        /// <summary>
        /// Get the list of all bodies in contact with this one
        /// </summary>
        public IReadOnlyList<Body> AllContactedBodies
        {
            get
            {
                int count = NativeMethods.cpBodyGetContactedBodiesCount(body);

                if (count == 0)
                    return Array.Empty<Body>();

                IntPtr ptrBodies = Marshal.AllocHGlobal(IntPtr.Size * count);
                NativeMethods.cpBodyGetUserDataContactedBodies(body, ptrBodies);

                IntPtr[] userDataArray = new IntPtr[count];

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
        /// Check if a Body is in contact with another
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool ContactWith(Body other)
        {
            return NativeMethods.cpBodyContactWith(body, other.body) != 0;
        }
    }
}
