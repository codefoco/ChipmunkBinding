using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipmunkBinding
{
    /// <summary>
    /// Contact point sets make getting contact information simpler.
    /// normal is the normal of the collision
    /// points is the array of contact points.Can be at most 2 points.
    /// </summary>
    public sealed class ContactPointSet : IEquatable<ContactPointSet>
    {
#pragma warning disable IDE0032
        private readonly int count;
        private readonly cpVect normal;
        private readonly ContactPoint[] points;
#pragma warning restore IDE0032

        private ContactPointSet(int count, cpVect normal, ContactPoint[] points)
        {
            this.count = count;
            this.normal = normal;
            this.points = points;
        }

        public int Count => count;
        public cpVect Normal => normal;
        public IReadOnlyList<ContactPoint> Points => points;

        public bool Equals(ContactPointSet other)
        {
            if (count != other.count)
                return false;
            if (normal != other.normal)
                return false;
            if (points.Length != other.points.Length)
                return false;

            return points.SequenceEqual(other.points);
        }

        public override int GetHashCode()
        {
            var hashCode = -475635172;
            hashCode = hashCode * -1521134295 + count.GetHashCode();
            hashCode = hashCode * -1521134295 + normal.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ContactPoint[]>.Default.GetHashCode(points);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ContactPointSet;
            if (other == null)
                return false;

            return Equals(other);
        }

        public static bool operator ==(ContactPointSet left, ContactPointSet right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ContactPointSet left, ContactPointSet right)
        {
            return !(left == right);
        }

        internal static ContactPointSet FromContactPointSet(cpContactPointSet contactPointSet)
        {
            ContactPoint[] points;

            points = new ContactPoint[2];
            points[0] = ContactPoint.FromCollidePoint(contactPointSet.points0);
            points[1] = ContactPoint.FromCollidePoint(contactPointSet.points1);

            return new ContactPointSet(contactPointSet.count,
                                       contactPointSet.normal, points);
        }
    }
}
