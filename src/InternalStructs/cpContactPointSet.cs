
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct cpContactPointSet
    {
        public int count;
        public Vect normal;

        public cpContactPoint points0;
        public cpContactPoint points1;
    }
}
