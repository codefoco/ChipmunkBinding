
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct cpContactPoint
    {
        public Vect pointA;
        public Vect pointB;
        public double distance;
    }
}
