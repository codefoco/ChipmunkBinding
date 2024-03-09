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
using System.Collections.Generic;
using System.Linq;

namespace ChipmunkBinding
{
    /// <summary>
    /// Contact point sets make getting contact information simpler. There can be at most 2 contact
    /// points.
    /// </summary>
    public sealed class ContactPointSet : IEquatable<ContactPointSet>
    {
#pragma warning disable IDE0032
        private readonly int count;
        private readonly Vect normal;
        private readonly ContactPoint[] points;
#pragma warning restore IDE0032

        private ContactPointSet(int count, Vect normal, ContactPoint[] points)
        {
            this.count = count;
            this.normal = normal;
            this.points = points;
        }

        /// <summary>
        /// Get the number of contact points in the contact set (maximum of two).
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Get the normal of the collision.
        /// </summary>
        public Vect Normal => normal;

        /// <summary>
        /// List of points in the contact point set
        /// </summary>
        public ContactPoint[] Points => points;

        /// <summary>
        /// Return true if the contact point set is sequence-equal to another.
        /// </summary>
        public bool Equals(ContactPointSet other)
        {
            if (ReferenceEquals(other, null)
                || count != other.count
                || normal != other.normal
                || points.Length != other.points.Length)
            {
                return false;
            }

            return points.SequenceEqual(other.points);
        }

        /// <summary>
        /// Get the hash code.
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = -475635172;

            hashCode = (hashCode * -1521134295) + count.GetHashCode();
            hashCode = (hashCode * -1521134295) + normal.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<ContactPoint[]>.Default.GetHashCode(points);

            return hashCode;
        }

        /// <summary>
        /// Return true if the <see cref="ContactPointSet"/> is sequence-equal to another.
        /// </summary>
        public override bool Equals(object obj)
        {
            var other = obj as ContactPointSet;

            if (other == null)
            {
                return false;
            }

            return Equals(other);
        }

        /// <summary>
        /// Return true if the contact point sets are sequence-equal.
        /// </summary>
        public static bool operator ==(ContactPointSet left, ContactPointSet right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Return true if the contact point sets are sequence-inequal.
        /// </summary>
        public static bool operator !=(ContactPointSet left, ContactPointSet right)
        {
            return !(left == right);
        }

        internal static ContactPointSet FromContactPointSet(cpContactPointSet contactPointSet)
        {
            var points = new ContactPoint[2];

            if (contactPointSet.count > 0)
            {
                points[0] = ContactPoint.FromCollidePoint(contactPointSet.points0);
            }
            else
            {
                points[0] = ContactPoint.Empty;
            }

            if (contactPointSet.count > 1)
            {
                points[1] = ContactPoint.FromCollidePoint(contactPointSet.points1);
            }
            else
            {
                points[1] = ContactPoint.Empty;
            }

            return new ContactPointSet(
                contactPointSet.count,
                contactPointSet.normal,
                points);
        }

        internal cpContactPointSet ToContactPointSet()
        {
            return new cpContactPointSet
            {
                normal = normal,
                points0 = points[0].ToContactPoint(),
                points1 = points[1].ToContactPoint(),
                count = count
            };
        }
    }
}