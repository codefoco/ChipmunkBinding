using System;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    public struct cpSegmentQueryInfo
    {
        public IntPtr shape;
        public cpVect point;
        public cpVect normal;
        public double alpha;
    }
}
