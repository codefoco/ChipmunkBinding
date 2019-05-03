using System;
using cpArbiter = System.IntPtr;

namespace ChipmunkBinding
{
    public class Arbiter
    {
        cpArbiter arbiter;

        public Arbiter(cpArbiter handle)
        {
            arbiter = handle;
        }
    }
}
