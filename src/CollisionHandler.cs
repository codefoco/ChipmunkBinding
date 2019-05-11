using System;

using cpCollisionHandler = System.IntPtr;

namespace ChipmunkBinding
{
    public class CollisionHandler
    {
        cpCollisionHandler collisionHandler;

        public CollisionHandler(cpCollisionHandler handle)
        {
            collisionHandler = handle;
        }


    }
}
