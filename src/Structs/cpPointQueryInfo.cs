using System;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct cpPointQueryInfo
    {
        public IntPtr shape;
        public cpVect point;
        public double distance;
        public cpVect gradient;
    }
}
