// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2024 Codefoco LTDA - The above copyright notice and this permission notice shall be
//     included in all copies or substantial portions of the Software.
//
//     Redistribution and use in source and binary forms, with or without
//     modification, are permitted only if explicitly approved by the authors.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
//     EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//     OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//     NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//     HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
//     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//     FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//     OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace ChipmunkBinding
{
    /// <summary>
    ///  Contains information about a contact point. <see cref="PointA"/> and <see cref="PointB"/>
    ///  are the contact positions on the surface of each shape. <see cref="Distance"/> is the
    ///  penetration distance of the two, which is a negative value. This value is calculated as
    ///  dot(point2 - point1), normal) and is ignored when you set the
    ///  <see cref="Arbiter.ContactPointSet"/>.
    /// </summary>
    public sealed class ContactPoint : IEquatable<ContactPoint>
    {
#pragma warning disable IDE0032
        private readonly Vect pointA;
        private readonly Vect pointB;
        private readonly double distance;
#pragma warning restore IDE0032

        /// <summary>
        /// Point A in the contact point.
        /// </summary>
        public Vect PointA => pointA;

        /// <summary>
        ///  Point B in the contact point.
        /// </summary>
        public Vect PointB => pointB;

        /// <summary>
        /// The penetration distance of the two shapes (as a negative value). This value is
        /// calculated as  dot(point2 - point1), normal) and is ignored when you set the
        /// <see cref="Arbiter.ContactPointSet"/>.
        /// </summary>
        public double Distance => distance;

        private ContactPoint(Vect pointA, Vect pointB, double distance)
        {
            this.pointA = pointA;
            this.pointB = pointB;
            this.distance = distance;
        }

        /// <summary>
        /// Returns true if neither <see cref="ContactPoint"/> is null and the points are within
        /// <see cref="float.Epsilon"/> distance of each other.
        /// </summary>
        public bool Equals(ContactPoint other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return other.pointA.Equals(pointA)
                && other.pointB.Equals(pointB)
                && Math.Abs(other.distance - distance) < float.Epsilon;
        }


        /// <summary>
        /// Check if this <see cref="ContactPoint"/> is equal to an object.
        /// </summary>
        public override bool Equals(object obj)
        {
            var other = obj as ContactPoint;

            if (other == null)
            {
                return false;
            }

            return Equals(other);
        }

        /// <summary>
        /// Get the <see cref="ContactPoint"/> hash set.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(pointA, pointB, distance);
        }

        /// <summary>
        /// Returns a string in the format of "a: {pointA}, b: {pointB}, distance: {distance}".
        /// </summary>
        public override string ToString()
        {
            return $"a: {pointA}, b: {pointB}, distance: {distance}";
        }

        /// <summary>
        /// Returns true if both <see cref="ContactPoint"/>s are the same object or the dimensions
        /// are within <see cref="float.Epsilon"/> distance of each other.
        /// </summary>
        public static bool operator ==(ContactPoint left, ContactPoint right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Returns false if both <see cref="ContactPoint"/>s are the same object or the dimensions
        /// are within <see cref="float.Epsilon"/> distance of each other.
        /// </summary>
        public static bool operator !=(ContactPoint left, ContactPoint right)
        {
            return !(left == right);
        }

        internal static ContactPoint Empty => new ContactPoint(Vect.Zero, Vect.Zero, 0.0);

        internal static ContactPoint FromCollidePoint(cpContactPoint contactPoint)
        {
            return new ContactPoint(
                contactPoint.pointA,
                contactPoint.pointB,
                contactPoint.distance);
        }

        internal cpContactPoint ToContactPoint()
        {
            return new cpContactPoint
            {
                pointA = pointA,
                pointB = pointB,
                distance = distance
            };
        }
    }
}