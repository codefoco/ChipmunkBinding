
using cpBody = System.IntPtr;


namespace ChipmunkBinding
{
    public class Body
    {
        readonly cpBody body;

        public cpBody Handle => body;

        public Body ():this(BodyType.Dinamic)
        {
        }
        
        public Body (BodyType type)
        {
            body = InitializeBody(type);
        }

        public Body (double mass, double moment, BodyType type)
        {
            body = InitializeBody(type);
            NativeMethods.cpBodySetMass(body, mass);
            NativeMethods.cpBodySetMoment(body, moment);
        }

        private static cpBody InitializeBody (BodyType type)
        {
            if (type == BodyType.Kinematic)
                return NativeMethods.cpBodyNewKinematic();
            if (type == BodyType.Static)
                return NativeMethods.cpBodyNewStatic();

            return NativeMethods.cpBodyNew(0.0, 0.0);
        }
    }
}
