using System;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    public struct cpShapeFilter
    {
        public UIntPtr group;
        public uint categories;
        public uint mask;
    }
}
