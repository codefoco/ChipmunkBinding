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
            ShapeOutline = new cpSpaceDebugColor(0,0,0),
            Constraint = new cpSpaceDebugColor(0, 1, 0),
            CollisionPoint = new cpSpaceDebugColor(1, 0, 1)
        };
        #pragma warning restore IDE0032

        public cpSpaceDebugColor ShapeOutline { get; set; }
        public cpSpaceDebugColor Constraint { get; set; }
        public cpSpaceDebugColor CollisionPoint { get; set; }

        public static DebugDrawColors Default => defaultColors;
      
    }
}
