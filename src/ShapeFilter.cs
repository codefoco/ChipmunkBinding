using System;
using System.Collections.Generic;
using System.Text;

namespace ChipmunkBinding
{
    /// <summary>
    /// Chipmunk has two primary means of ignoring collisions: groups and category masks. Groups are
    /// used to ignore collisions between parts on a complex object. A ragdoll is a good example.
    /// When jointing an arm onto the torso, you’ll want to allow them to overlap, which groups are
    /// good for. Shapes that have the same group don’t generate collisions, so by placing all of
    /// the shapes in a ragdoll in the same group, you’ll prevent it from colliding against other
    /// parts of itself. Category masks allow you to mark which categories an object belongs to and
    /// which categories it collides with. For example, a game has four collision categories: player
    /// (0), enemy (1), player bullet (2), and enemy bullet (3). Players and enemies shouldn't
    /// collide with their own bullets, and bullets shouldn't collide with other bullets. However,
    /// players collide with enemy bullets, and enemies collide with player bullets.
    /// </summary>
    public sealed class ShapeFilter : IEquatable<ShapeFilter>
    {
        /// <summary>
        /// Get the shape filter group.
        /// </summary>
        public int Group { get; }

        /// <summary>
        /// Get the shape filter category.
        /// </summary>
        public int Categories { get; }

        /// <summary>
        /// Get the shape filter mask.
        /// </summary>
        public int Mask { get; }

        /// <summary>
        /// Category enum
        /// </summary>
        public enum Category
        {
            /// <summary>
            /// None
            /// </summary>
            None = 0,

            /// <summary>
            /// All
            /// </summary>
            All = ~0,
        }

#pragma warning disable IDE0032
        private static readonly ShapeFilter filterAll = new ShapeFilter(0, Category.All, Category.All);
        private static readonly ShapeFilter filterNone = new ShapeFilter(0, Category.None, Category.None);
#pragma warning restore IDE0032

        /// <summary>
        /// Shape filter All
        /// </summary>
        public static ShapeFilter All => filterAll;

        /// <summary>
        /// Shape filter None
        /// </summary>
        public static ShapeFilter None => filterNone;

        /// <summary>
        /// Create a ShapeFilter.
        /// </summary>
        public ShapeFilter(int group, Category categories, Category mask)
        {
            Group = group;
            Categories = (int)categories;
            Mask = (int)mask;
        }

        /// <summary>
        /// Create a ShapeFilter with a group, category and mask.
        /// </summary>
        public ShapeFilter(int group, int categories, int mask)
        {
            Group = group;
            Categories = categories;
            Mask = mask;
        }

        /// <summary>
        /// Return true if the fields in both objects are equivalent.
        /// </summary>
        public bool Equals(ShapeFilter other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return Group == other.Group &&
                   Categories == other.Categories &&
                   Mask == other.Mask;
        }

        /// <summary>
        /// Get the hash code.
        /// </summary>
        public override int GetHashCode()
        {
            var hashCode = 470831370;
            hashCode = hashCode * -1521134295 + Group.GetHashCode();
            hashCode = hashCode * -1521134295 + Categories.GetHashCode();
            hashCode = hashCode * -1521134295 + Mask.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Return true if the fields in both objects are equivalent.
        /// </summary>
        public override bool Equals(object obj)
        {
            var other = obj as ShapeFilter;

            if (other == null)
            {
                return false;
            }

            return Equals(other);
        }

        /// <summary>
        /// Return true if both objects are reference-equal or the fields in both objects are
        /// equivalent.
        /// </summary>
        public static bool operator == (ShapeFilter left, ShapeFilter right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Return true if both objects are not reference-equal or the fields in both objects are
        /// equivalent.
        /// </summary>
        public static bool operator !=(ShapeFilter a, ShapeFilter b)
        {
            return !(a == b);
        }
    }
}
