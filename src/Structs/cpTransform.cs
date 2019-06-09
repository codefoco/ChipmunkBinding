using System;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006
#pragma warning disable IDE0032

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    public struct cpTransform : IEquatable<cpTransform>
    {
        private static readonly cpTransform identity = new cpTransform(1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f);

        private double a;
        private double b;
        private double c;
        private double d;
        private double tx;
        private double ty;

        public cpTransform(double a, double b, double c, double d, double tx, double ty) : this()
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.tx = tx;
            this.ty = ty;
        }


        public static cpTransform Identity => identity;

        public double A { get => a; set => a = value; }
        public double B { get => b; set => b = value; }
        public double C { get => c; set => c = value; }
        public double D { get => d; set => d = value; }
        public double Tx { get => tx; set => tx = value; }
        public double Ty { get => ty; set => ty = value; }

        public override bool Equals(object obj)
        {
            cpTransform? transform = obj as cpTransform?;
            if (!transform.HasValue)
                return false;
            return Equals(transform.Value);
        }

        public bool Equals(cpTransform other)
        {
            return Math.Abs(a - other.a) < double.Epsilon &&
                   Math.Abs(b - other.b) < double.Epsilon &&
                   Math.Abs(c - other.c) < double.Epsilon &&
                   Math.Abs(d - other.d) < double.Epsilon &&
                   Math.Abs(tx - other.tx) < double.Epsilon &&
                   Math.Abs(ty - other.ty) < double.Epsilon;
        }

        public override int GetHashCode()
        {
            var hashCode = -884009331;
            hashCode = hashCode * -1521134295 + a.GetHashCode();
            hashCode = hashCode * -1521134295 + b.GetHashCode();
            hashCode = hashCode * -1521134295 + c.GetHashCode();
            hashCode = hashCode * -1521134295 + d.GetHashCode();
            hashCode = hashCode * -1521134295 + tx.GetHashCode();
            hashCode = hashCode * -1521134295 + ty.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"({a},{b}|{c},{d}|{tx},{ty})";
        }

        public static bool operator ==(cpTransform transform1, cpTransform transform2)
        {
            return transform1.Equals(transform2);
        }

        public static bool operator !=(cpTransform transform1, cpTransform transform2)
        {
            return !(transform1 == transform2);
        }
    }
}

#pragma warning restore IDE1006
#pragma warning restore IDE0032
