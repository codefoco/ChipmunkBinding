using System;
using System.Collections.Generic;
using System.Text;

namespace ChipmunkBinding
{
    public class DebugDrawColors
    {
        #pragma warning disable IDE0032
        static readonly DebugDrawColors defaultColors = new DebugDrawColors()
        {
            ShapeOutline = new DebugColor(0,0,0),
            Constraint = new DebugColor(0, 1, 0),
            CollisionPoint = new DebugColor(1, 0, 1)
        };
        #pragma warning restore IDE0032

        public DebugColor ShapeOutline { get; set; }
        public DebugColor Constraint { get; set; }
        public DebugColor CollisionPoint { get; set; }

        public static DebugDrawColors Default => defaultColors;
      
    }
}
