using System;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct cpSegmentQueryInfo
    {
        public IntPtr shape;
        public Vect point;
        public Vect normal;
        public double alpha;
    }
}
