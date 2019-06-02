using System;
using cpArbiter = System.IntPtr;
using cpDataPointer = System.IntPtr;

namespace ChipmunkBinding
{
    public struct Arbiter
    {
#pragma warning disable IDE0032
        readonly cpArbiter arbiter;
#pragma warning restore IDE0032

        internal cpArbiter Handle => arbiter;

        internal Arbiter(cpArbiter handle)
        {
            arbiter = handle;
        }
    }
}
