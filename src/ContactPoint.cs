﻿using System;

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

        /// <summary>
        /// Point A in contact point
        /// </summary>
        public Vect PointA => pointA;
        /// <summary>
        ///  Point B in the contact point
        /// </summary>
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

        /// <summary>
        /// Check if the this ContactPoint is equal to another
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ContactPoint other)
        {
            if (ReferenceEquals(other, null))
                return false;

            return other.pointA.Equals(pointA) &&
                   other.pointB.Equals(pointB) &&
                   Math.Abs(other.distance - distance) < float.Epsilon;
        }


        /// <summary>
        /// Check if this ContactPoint is equal to an object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as ContactPoint;
            if (other == null)
                return false;

            return Equals(other);
        }

        /// <summary>
        /// Get ContactPoint hash set
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = -1285340573;
            hashCode = hashCode * -1521134295 + pointA.GetHashCode();
            hashCode = hashCode * -1521134295 + pointB.GetHashCode();
            hashCode = hashCode * -1521134295 + distance.GetHashCode();
            return hashCode;
        }

        /// <summary>
        ///  ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"a: {pointA}, b: {pointB}, distance: {distance}";
        }

        /// <summary>
        /// operator ==
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(ContactPoint left, ContactPoint right)
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
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(ContactPoint left, ContactPoint right)
        {
            return !(left == right);
        }

        internal static ContactPoint Empty => new ContactPoint(Vect.Zero, Vect.Zero, 0.0);

        internal static ContactPoint FromCollidePoint(cpContactPoint contactPoint)
        {
            return new ContactPoint(contactPoint.pointA,
                                    contactPoint.pointB,
                                    contactPoint.distance);
        }

        internal cpContactPoint ToContactPoint()
        {
            var contactPoint = new cpContactPoint();
            contactPoint.pointA = pointA;
            contactPoint.pointB = pointB;
            contactPoint.distance = distance;
            return contactPoint;
        }
    }
}
