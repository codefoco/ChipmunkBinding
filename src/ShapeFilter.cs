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


    public class ShapeFilter : IEquatable<ShapeFilter>
    {
        public int Group { get; }
        public int Categories { get; }
        public int Mask { get; }

        public enum Categorie
        {
            None = 0,
            All = ~0,
        }

#pragma warning disable IDE0032
        private static readonly ShapeFilter filterAll = new ShapeFilter(0, Categorie.All, Categorie.All);
        private static readonly ShapeFilter filterNone = new ShapeFilter(0, Categorie.None, Categorie.None);
#pragma warning restore IDE0032


        public static ShapeFilter All => filterAll;
        public static ShapeFilter None => filterNone;

        public ShapeFilter(int group, Categorie categories, Categorie mask)
        {
            Group = group;
            Categories = (int)categories;
            Mask = (int)mask;
        }
        public ShapeFilter(int group, int categories, int mask)
        {
            Group = group;
            Categories = categories;
            Mask = mask;
        }

        public bool Equals(ShapeFilter other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            var hashCode = 470831370;
            hashCode = hashCode * -1521134295 + Group.GetHashCode();
            hashCode = hashCode * -1521134295 + Categories.GetHashCode();
            hashCode = hashCode * -1521134295 + Mask.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ShapeFilter;
            if (other == null)
                return false;

            return Equals(other);
        }

        public static bool operator == (ShapeFilter a, ShapeFilter b)
        {
            return a.Group == b.Group && 
                   a.Categories == b.Categories &&
                   a.Mask == b.Mask;
        }

        public static bool operator !=(ShapeFilter a, ShapeFilter b)
        {
            return !(a == b);
        }
    }



}
