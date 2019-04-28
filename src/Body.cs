using System;
using System.Runtime.InteropServices;

using cpBody = System.IntPtr;
using cpSpace = System.IntPtr;
using cpDataPointer = System.IntPtr;


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

        void RegisterUserData()
        {
            cpDataPointer pointer = HandleInterop.RegisterHandle(this);
            NativeMethods.cpBodySetUserData(body, pointer);
        }

        void ReleaseUserData()
        {
            cpDataPointer pointer = NativeMethods.cpBodyGetUserData(body);
            HandleInterop.ReleaseHandle(pointer);
        }

        public static Body FromHandle(cpBody body)
        {
            cpDataPointer handle = NativeMethods.cpBodyGetUserData(body);
            return HandleInterop.FromIntPtr<Body>(handle);
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

        public void Destroy()
        {
            ReleaseUserData();
            NativeMethods.cpBodyDestroy(body);
        }

        public void Dispose()
        {
            Destroy();
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

        
    }
}
