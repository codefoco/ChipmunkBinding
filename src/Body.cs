
using cpBody = System.IntPtr;


namespace ChipmunkBinding
{
    /// <summary>
    /// Mass and moment are ignored when body_type is Kinematic or Static.
    /// Guessing the mass for a body is usually fine, but guessing a moment of inertia
    /// can lead to a very poor simulation so it’s recommended to use Chipmunk’s moment
    /// calculations to estimate the moment for you.
    /// </summary>
    public class Body
    {
        readonly cpBody body;

        public cpBody Handle => body;
        /// <summary>
        /// Create a Dynamic Body with no mass and no moment
        /// </summary>
        public Body() : this(BodyType.Dinamic)
        {
        }

        /// <summary>
        ///  Create a Body of the type (Dinamic, Kinematic, Static)
        /// </summary>
        /// <param name="type"></param>
        public Body(BodyType type)
        {
            body = InitializeBody(type);
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
        }

        private static cpBody InitializeBody(BodyType type)
        {
            if (type == BodyType.Kinematic)
                return NativeMethods.cpBodyNewKinematic();
            if (type == BodyType.Static)
                return NativeMethods.cpBodyNewStatic();

            return NativeMethods.cpBodyNew(0.0, 0.0);
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


    }
}
