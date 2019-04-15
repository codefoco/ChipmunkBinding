using System.Runtime.InteropServices;

#pragma warning disable IDE1006

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    public struct cpVect
    {
        public float x;
        public float y;
    }
}

#pragma warning restore IDE1006
