using System;

namespace ChipmunkBinding
{
    /// <summary>
    /// Flag to enable/disable DebugDrawing
    /// </summary>
    [Flags]
    public enum DebugDrawFlags
    {
        /// <summary>
        /// Draw nothing
        /// </summary>
        None = 0,
        /// <summary>
        /// Draw Shapes
        /// </summary>
        Shapes = 1 << 0,
        /// <summary>
        /// Draw Constraints
        /// </summary>
        Constraints = 1 << 1,
        /// <summary>
        ///  Draw Collision Points
        /// </summary>
        CollisionPoints = 1 << 2,
        /// <summary>
        /// Draw All
        /// </summary>
        All = Shapes | Constraints | CollisionPoints
    }
}
