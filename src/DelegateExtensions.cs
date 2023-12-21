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
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    /// <summary>
    /// Create a ToFunctionPointer extension method for each delegate type. Unfortunately C# 7.0
    /// can't do something like that (you will need C# 7.3), thus we create one extension method for
    /// each delegate public static IntPtr ToFunctionPointer T (this T d) where T : Delegate
    /// </summary>
    internal static class DelegateExtensions
    {
        public static IntPtr ToFunctionPointer(this BodyArbiterIteratorFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this BodyConstraintIteratorFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this BodyShapeIteratorFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this BodyVelocityFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this BodyPositionFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this CollisionBeginFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }


        public static IntPtr ToFunctionPointer(this CollisionPreSolveFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }


        public static IntPtr ToFunctionPointer(this CollisionPostSolveFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }


        public static IntPtr ToFunctionPointer(this CollisionSeparateFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }


        public static IntPtr ToFunctionPointer(this PostStepFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }


        public static IntPtr ToFunctionPointer(this SpaceSegmentQueryFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this SpacePointQueryFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this SpaceBBQueryFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }


        public static IntPtr ToFunctionPointer(this SpaceObjectIteratorFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }


        public static IntPtr ToFunctionPointer(this SpaceDebugDrawCircleImpl d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }


        public static IntPtr ToFunctionPointer(this SpaceDebugDrawSegmentImpl d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this SpaceDebugDrawFatSegmentImpl d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this SpaceDebugDrawPolygonImpl d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this SpaceDebugDrawDotImpl d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this SpaceDebugDrawColorForShapeImpl d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }


        public static IntPtr ToFunctionPointer(this ConstraintSolveFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this DampedSpringForceFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this DampedRotarySpringTorqueFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this SpaceShapeQueryFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this MarchSegmentFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }

        public static IntPtr ToFunctionPointer(this MarchSampleFunction d)
        {
            if (d == null)
            {
                return IntPtr.Zero;
            }

            return Marshal.GetFunctionPointerForDelegate(d);
        }
    }
}