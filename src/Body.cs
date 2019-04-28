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

        public static Body FromHandle(cpBody space)
        {
            cpDataPointer handle = NativeMethods.cpSpaceGetUserData(space);
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
        /// Type of the body (Dynamic, Kinematic, Static). 
        /// </summary>
        public BodyType Type
        {
            get => (BodyType)NativeMethods.cpBodyGetType(Handle);
            set => NativeMethods.cpBodySetType(Handle, (int)value);
        }

        /// <summary>
        /// Mass of the rigid body. Mass does not have to be expressed in any particular units, but relative masses should be consistent. 
        /// </summary>
        public double Mass
        {
            get => NativeMethods.cpBodyGetMass(Handle);
            set => NativeMethods.cpBodySetMass(Handle, value);
        }

        /// <summary>
        /// Moment of inertia of the body. The mass tells you how hard it is to push an object, the MoI tells you how hard it is to spin the object.
        /// Don't try to guess the MoI, use the MomentFor*() functions to try and estimate it. 
        /// </summary>
        public double Moment
        {
            get => NativeMethods.cpBodyGetMoment(Handle);
            set => NativeMethods.cpBodySetMoment(Handle, value);
        }

        /// <summary>
        /// The Space this body is currently added to, or null if it is not currently added to a space.
        /// </summary>
        public Space Space
        {
            get
            {
                cpSpace space = NativeMethods.cpBodyGetSpace(Handle);
                return Space.FromHandleSafe(space);
            }
        }
    }
}
