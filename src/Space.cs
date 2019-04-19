using System;

using cpSpace = System.IntPtr;


namespace ChipmunkBinding
{
    public class Space : IDisposable
    {
        readonly cpSpace space;

        public Space()
        {
            space = NativeMethods.cpSpaceNew();
        }

        public void Destroy()
        {
            NativeMethods.cpSpaceDestroy(space);
        }

        public void Dispose()
        {
           Destroy();
        }

        public void AddBody(Body body)
        {
            NativeMethods.cpSpaceAddBody(space, body.Handle);
        }
    }
}
