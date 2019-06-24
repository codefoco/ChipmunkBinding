using System;

using cpConstraint = System.IntPtr;
using cpDataPointer = System.IntPtr;

namespace ChipmunkBinding
{
    public abstract class Constraint
    {
        cpConstraint constraint;

        public cpConstraint Handle => constraint;

        public static Constraint FromHandle(cpConstraint constraint)
        {
            cpDataPointer handle = NativeMethods.cpConstraintGetUserData(constraint);
            return NativeInterop.FromIntPtr<Constraint>(handle);
        }

        protected Constraint(cpConstraint handle)
        {
            constraint = handle;
        }
    }
}
