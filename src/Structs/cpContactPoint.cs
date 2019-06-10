
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct cpContactPoint
    {
        public cpVect pointA;
        public cpVect pointB;
        public double distance;
    }
}
