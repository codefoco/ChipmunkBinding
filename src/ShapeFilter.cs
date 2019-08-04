using System;
using System.Collections.Generic;
using System.Text;

namespace ChipmunkBinding
{
    /// <summary>
    /// Chipmunk has two primary means of ignoring collisions: groups and category masks.
    /// Groups are used to ignore collisions between parts on a complex object. A ragdoll is a good example. When jointing an arm onto the torso, you’ll want them to allow them to overlap. Groups allow you to do exactly that. Shapes that have the same group don’t generate collisions. So by placing all of the shapes in a ragdoll in the same group, you’ll prevent it from colliding against other parts of itself. Category masks allow you to mark which categories an object belongs to and which categories it collides with.
    /// For example, a game has four collision categories: player (0), enemy (1), player bullet (2), and enemy bullet (3). Neither players nor enemies should not collide with their own bullets, and bullets should not collide with other bullets. However, players collide with enemy bullets, and enemies collide with player bullets.
    /// </summary>
    /// 
    public sealed class ShapeFilter : IEquatable<ShapeFilter>
    {
        /// <summary>
        /// Shape filter Group
        /// </summary>
        public int Group { get; }

        /// <summary>
        /// Shape filter Categorie
        /// </summary>
        public int Categories { get; }

        /// <summary>
        /// Shape filter Mask
        /// </summary>
        public int Mask { get; }

        /// <summary>
        /// Categorie enum
        /// </summary>
        public enum Categorie
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
        private static readonly ShapeFilter filterAll = new ShapeFilter(0, Categorie.All, Categorie.All);
        private static readonly ShapeFilter filterNone = new ShapeFilter(0, Categorie.None, Categorie.None);
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
        /// Create a ShapeFilter
        /// </summary>
        /// <param name="group"></param>
        /// <param name="categories"></param>
        /// <param name="mask"></param>
        public ShapeFilter(int group, Categorie categories, Categorie mask)
        {
            Group = group;
            Categories = (int)categories;
            Mask = (int)mask;
        }

        /// <summary>
        /// Create a ShapeFilter with group, categorie and mask
        /// </summary>
        /// <param name="group"></param>
        /// <param name="categories"></param>
        /// <param name="mask"></param>
        public ShapeFilter(int group, int categories, int mask)
        {
            Group = group;
            Categories = categories;
            Mask = mask;
        }

        /// <summary>
        /// Check if a ShapeFilter is equal to another
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ShapeFilter other)
        {
            if (ReferenceEquals(other, null))
                return false;

            return Group == other.Group &&
                   Categories == other.Categories &&
                   Mask == other.Mask;
        }

        /// <summary>
        /// object GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = 470831370;
            hashCode = hashCode * -1521134295 + Group.GetHashCode();
            hashCode = hashCode * -1521134295 + Categories.GetHashCode();
            hashCode = hashCode * -1521134295 + Mask.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// object equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as ShapeFilter;
            if (other == null)
                return false;

            return Equals(other);
        }

        /// <summary>
        /// operator ==
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator == (ShapeFilter left, ShapeFilter right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        /// <summary>
        /// operator !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(ShapeFilter a, ShapeFilter b)
        {
            return !(a == b);
        }
    }
}
