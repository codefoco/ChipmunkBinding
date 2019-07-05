using System;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct cpPolyline
    {
        public int count;
        public int capacity;
        public IntPtr verts;
    }
}
