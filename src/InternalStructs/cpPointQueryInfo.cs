using System;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct cpPointQueryInfo
    {
        public IntPtr shape;
        public Vect point;
        public double distance;
        public Vect gradient;
    }
}
