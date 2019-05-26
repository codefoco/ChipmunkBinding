using System;

namespace ChipmunkBinding
{
    [Flags]
    public enum DebugDrawFlags
    {
        None = 0,
        Shapes = 1 << 0,
        Constraints = 1 << 1,
        CollisionPoints = 1 << 2,
        All = Shapes | Constraints | CollisionPoints
    }
}
