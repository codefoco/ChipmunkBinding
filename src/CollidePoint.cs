using System;

namespace ChipmunkBinding
{
    /// <summary>
    ///  Contains information about a contact point.
    ///  point_a and point_b are the contact position on the surface of each shape.
    ///  distance is the penetration distance of the two shapes.Overlapping means it will be negative.This value is calculated as dot(point2 - point1), normal) and is ignored when you set the Arbiter.contact_point_set.
    /// </summary>
    public sealed class ContactPoint : IEquatable<ContactPoint>
    {
#pragma warning disable IDE0032
        private readonly Vect pointA;
        private readonly Vect pointB;
        private readonly double distance;
#pragma warning restore IDE0032

        public Vect PointA => pointA;
        public Vect PointB => pointB;

        /// <summary>
        /// Distance is the penetration distance of the two shapes. Overlapping means it will be negative. This value is calculated as dot(point2 - point1), normal) and is ignored when you set the Arbiter.contact_point_set.
        /// </summary>
        public double Distance => distance;

        private ContactPoint(Vect pointA, Vect pointB, double distance)
        {
            this.pointA = pointA;
            this.pointB = pointB;
            this.distance = distance;
        }

        public bool Equals(ContactPoint other)
        {
            return other.pointA.Equals(pointA) &&
                   other.pointB.Equals(pointB) &&
                   Math.Abs(other.distance - distance) < float.Epsilon;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ContactPoint;
            if (other == null)
                return false;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = -1285340573;
            hashCode = hashCode * -1521134295 + pointA.GetHashCode();
            hashCode = hashCode * -1521134295 + pointB.GetHashCode();
            hashCode = hashCode * -1521134295 + distance.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"a: {pointA}, b: {pointB}, distance: {distance}";
        }

        public static bool operator ==(ContactPoint left, ContactPoint right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ContactPoint left, ContactPoint right)
        {
            return !(left == right);
        }

        internal static ContactPoint FromCollidePoint(cpContactPoint contactPoint)
        {
            return new ContactPoint(contactPoint.pointA,
                                    contactPoint.pointB,
                                    contactPoint.distance);
        }
    }
}
