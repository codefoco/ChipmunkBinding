using System.Runtime.InteropServices;

#pragma warning disable IDE1006

namespace ChipmunkBinding
{
    [StructLayout (LayoutKind.Sequential)]
    internal struct cpBB
    {
        public double left;
        public double bottom;
        public double right;
        public double top;
    }
}

#pragma warning restore IDE1006
