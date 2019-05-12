using System;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct cpSegmentQueryInfo
    {
        public IntPtr shape;
        public cpVect point;
        public cpVect normal;
        public double alpha;
    }
}
