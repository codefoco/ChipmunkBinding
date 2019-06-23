using System;
using System.Collections.Generic;
using cpArbiter = System.IntPtr;

namespace ChipmunkBinding
{
    /// <summary>
    /// The Arbiter object encapsulates a pair of colliding shapes and all of
    /// the data about their collision.
    /// </summary>
    public struct Arbiter : IEquatable<Arbiter>
    {
#pragma warning disable IDE0032
        readonly cpArbiter arbiter;
#pragma warning restore IDE0032

        /// <summary>
        /// Native handle of Arbiter
        /// </summary>
        public cpArbiter Handle => arbiter;

        internal Arbiter(cpArbiter handle)
        {
            arbiter = handle;
        }

        /// <summary>
        /// The restitution (elasticity) that will be applied to the pair of colliding objects.
        /// </summary>
        public double Restitution
        {
            get => NativeMethods.cpArbiterGetRestitution(arbiter);
            set => NativeMethods.cpArbiterSetRestitution(arbiter, value);
        }

        /// <summary>
        /// Friction coefficient that will be applied to the pair of colliding objects.
        /// </summary>
        public double Friction
        {
            get => NativeMethods.cpArbiterGetFriction(arbiter);
            set => NativeMethods.cpArbiterSetFriction(arbiter, value);
        }

        /// <summary>
        /// Get/Override the relative surface velocity of the two shapes in contact.
        /// </summary>
        public Vect SurfaceVelocity
        {
            get => NativeMethods.cpArbiterGetSurfaceVelocity(arbiter);
            set => NativeMethods.cpArbiterSetSurfaceVelocity(arbiter, value);
        }

        /// <summary>
        /// Calculate the total impulse including the friction that was applied by this arbiter.
        /// This function should only be called from a post-solve, post-step or cpBodyEachArbiter callback.
        /// </summary>
        public Vect TotalImpulse => NativeMethods.cpArbiterTotalImpulse(arbiter);

        /// <summary>
        /// Calculate the amount of energy lost in a collision including static, but not dynamic friction.
        /// This function should only be called from a post-solve, post-step or cpBodyEachArbiter callback.
        /// </summary>
        public double TotalKE => NativeMethods.cpArbiterTotalKE(arbiter);

        /// <summary>
        /// Mark a collision pair to be ignored until the two objects separate.
        /// Pre-solve and post-solve callbacks will not be called, but the separate callback will be called.
        /// </summary>
        public bool Ignore() => NativeMethods.cpArbiterIgnore(arbiter) != 0;

        /// <summary>
        /// Return the colliding shapes involved for this arbiter.
        /// The order of their cpSpace.collision_type values will match
        /// the order set when the collision handler was registered
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void GetShapes(out Shape a, out Shape b)
        {
            IntPtr ptrA;
            IntPtr ptrB;

            NativeMethods.cpArbiterGetShapes(arbiter, out ptrA, out ptrB);

            a = Shape.FromHandle(ptrA);
            b = Shape.FromHandle(ptrB);
        }

        /// <summary>
        /// Return the colliding bodies involved for this arbiter.
        /// The order of the cpSpace.collision_type the bodies are associated with values will match
        /// the order set when the collision handler was registered.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void GetBodies(out Body a, out Body b)
        {
            IntPtr ptrA;
            IntPtr ptrB;

            NativeMethods.cpArbiterGetBodies(arbiter, out ptrA, out ptrB);

            a = Body.FromHandle(ptrA);
            b = Body.FromHandle(ptrB);
        }

        /// <summary>
        /// Get/Replace the contact point set for an arbiter.
        /// This can be a very powerful feature, but use it with caution!
        /// </summary>
        public ContactPointSet ContactPointSet
        {
            get
            {
                cpContactPointSet pointSet = NativeMethods.cpArbiterGetContactPointSet(arbiter);
                return ContactPointSet.FromContactPointSet(pointSet);
            }
            set
            {
                cpContactPointSet pointSet = value.ToContactPointSet();
                NativeMethods.cpArbiterSetContactPointSet(arbiter, ref pointSet);
            }
        }

        /// <summary>
        /// Returns true if this is the first step a pair of objects started colliding.
        /// </summary>
        public bool IsFirstContact => NativeMethods.cpArbiterIsFirstContact(arbiter) != 0;

        /// <summary>
        /// Returns true if the separate callback is due to a shape being removed from the space.
        /// </summary>
        public bool IsRemoval => NativeMethods.cpArbiterIsRemoval(arbiter) != 0;

        /// <summary>
        /// Get the number of contact points for this arbiter.
        /// </summary>
        public int Count => NativeMethods.cpArbiterGetCount(arbiter);

        /// <summary>
        /// Get the normal of the collision.
        /// </summary>
        public Vect Normal => NativeMethods.cpArbiterGetNormal(arbiter);

        /// <summary>
        /// Get the position of the @c ith contact point on the surface of the first shape.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Vect GetPointA(int i)
        {
            return NativeMethods.cpArbiterGetPointA(arbiter, i);
        }

        /// <summary>
        /// Get the position of the @c ith contact point on the surface of the second shape.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Vect GetPointB(int i)
        {
            return NativeMethods.cpArbiterGetPointB(arbiter, i);
        }

        /// <summary>
        /// Get the depth of the @c ith contact point.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public double GetDepth(int i)
        {
            return NativeMethods.cpArbiterGetDepth(arbiter, i);
        }

        /// <summary>
        /// If you want a custom callback to invoke the wildcard callback for the first collision type, you must call this function explicitly.
        /// You must decide how to handle the wildcard's return value since it may disagree with the other wildcard handler's return value or your own.
        /// </summary>
        /// <param name="space"></param>
        /// <returns></returns>
        public bool CallWildcardBeginA(Space space)
        {
            return NativeMethods.cpArbiterCallWildcardBeginA(arbiter, space.Handle) != 0;
        }

        /// <summary>
        /// If you want a custom callback to invoke the wildcard callback for the second collision type, you must call this function explicitly.
        /// You must decide how to handle the wildcard's return value since it may disagree with the other wildcard handler's return value or your own.
        /// </summary>
        /// <param name="space"></param>
        /// <returns></returns>
        public bool CallWildcardBeginB(Space space)
        {
            return NativeMethods.cpArbiterCallWildcardBeginB(arbiter, space.Handle) != 0;
        }

        /// <summary>
        /// If you want a custom callback to invoke the wildcard callback for the first collision type, you must call this function explicitly.
        /// You must decide how to handle the wildcard's return value since it may disagree with the other wildcard handler's return value or your own.
        /// </summary>
        /// <param name="space"></param>
        /// <returns></returns>
        public bool CallWildcardPreSolveA(Space space)
        {
            return NativeMethods.cpArbiterCallWildcardPreSolveA(arbiter, space.Handle) != 0;
        }

        /// <summary>
        /// If you want a custom callback to invoke the wildcard callback for the second collision type, you must call this function explicitly.
        /// You must decide how to handle the wildcard's return value since it may disagree with the other wildcard handler's return value or your own.
        /// </summary>
        /// <param name="space"></param>
        /// <returns></returns>
        public bool CallWildcardPreSolveB(Space space)
        {
            return NativeMethods.cpArbiterCallWildcardPreSolveB(arbiter, space.Handle) != 0;
        }

        /// <summary>
        /// If you want a custom callback to invoke the wildcard callback for the first collision type, you must call this function explicitly.
        /// </summary>
        /// <param name="space"></param>
        public void CallWildcardPostSolveA(Space space)
        {
            NativeMethods.cpArbiterCallWildcardPostSolveA(arbiter, space.Handle);
        }

        /// <summary>
        /// If you want a custom callback to invoke the wildcard callback for the second collision type, you must call this function explicitly.
        /// </summary>
        /// <param name="space"></param>
        public void CallWildcardPostSolveB(Space space)
        {
            NativeMethods.cpArbiterCallWildcardPostSolveB(arbiter, space.Handle);
        }

        /// <summary>
        /// If you want a custom callback to invoke the wildcard callback for the first collision type, you must call this function explicitly.
        /// </summary>
        /// <param name="space"></param>
        public void CallWildcardSeparateA(Space space)
        {
            NativeMethods.cpArbiterCallWildcardSeparateA(arbiter, space.Handle);
        }

        /// <summary>
        /// If you want a custom callback to invoke the wildcard callback for the second collision type, you must call this function explicitly.
        /// </summary>
        /// <param name="space"></param>
        public void CallWildcardSeparateB(Space space)
        {
            NativeMethods.cpArbiterCallWildcardSeparateB(arbiter, space.Handle);
        }

        /// <summary>
        /// Check if an arbiter is equals to another
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Arbiter other)
        {
            return arbiter == other.arbiter;
        }

        /// <summary>
        /// Check if an arbiter is equals to a object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as Arbiter?;
            if (!other.HasValue)
                return false;

            return Equals(other.Value);
        }

        /// <summary>
        /// return arbiter handle string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Handle: {arbiter}";
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 932982278 + EqualityComparer<cpArbiter>.Default.GetHashCode(arbiter);
        }

        /// <summary>
        /// Check if an arbiter is equals to another
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Arbiter left, Arbiter right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Check if an arbiter is different to another
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Arbiter left, Arbiter right)
        {
            return !(left == right);
        }
    }
}
