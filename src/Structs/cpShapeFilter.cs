using System;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct cpShapeFilter
    {
        public IntPtr group;
        public int categories;
        public int mask;

        public static cpShapeFilter FromShapeFilter(ShapeFilter filter)
        {
            var cpFilter = new cpShapeFilter();
            cpFilter.group = (IntPtr)filter.Group;
            cpFilter.categories = filter.Categories;
            cpFilter.mask = filter.Mask;
            return cpFilter;
        }
    }
}
