namespace ChipmunkBinding
{
    /// <summary>
    /// Default DebugColors for ShapeOutline, Constraint and CollisionPoint
    /// </summary>
    public class DebugDrawColors
    {
        #pragma warning disable IDE0032
        static readonly DebugDrawColors defaultColors = new DebugDrawColors()
        {
            ShapeOutline = new DebugColor(1, 1, 1),
            Constraint = new DebugColor(0, 1, 0),
            CollisionPoint = new DebugColor(1, 0, 1)
        };
        #pragma warning restore IDE0032

        /// <summary>
        /// Shape outline color.
        /// </summary>
        public DebugColor ShapeOutline { get; set; }

        /// <summary>
        /// Constraint color.
        /// </summary>
        public DebugColor Constraint { get; set; }

        /// <summary>
        /// Collision point color.
        /// </summary>
        public DebugColor CollisionPoint { get; set; }

        /// <summary>
        /// The Default DebugDrawColors.
        /// </summary>
        public static DebugDrawColors Default => defaultColors;
      
    }
}
