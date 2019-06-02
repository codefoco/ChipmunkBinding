using System;

using cpShape = System.IntPtr;
using cpDataPointer = System.IntPtr;
using System.Diagnostics;

namespace ChipmunkBinding
{
    public class Shape : IDisposable
    {
#pragma warning disable IDE0032
        private readonly cpShape shape;
#pragma warning restore IDE0032

        public Shape(Body body, double width, double height, double radius)
        {
            shape = NativeMethods.cpBoxShapeNew(body.Handle, width, height, radius);
            RegisterUserData();
        }

        internal protected Shape(cpShape shapeHandle)
        {
            shape = shapeHandle;
            RegisterUserData();
        }

        /// <summary>
        /// Native handle cpShape
        /// </summary>
        public cpShape Handle => shape;

        protected void RegisterUserData()
        {
            cpDataPointer pointer = NativeInterop.RegisterHandle(this);
            NativeMethods.cpShapeSetUserData(shape, pointer);
        }

        void ReleaseUserData()
        {
            cpDataPointer pointer = NativeMethods.cpShapeGetUserData(shape);
            NativeInterop.ReleaseHandle(pointer);
        }

        public static Shape FromHandle(cpShape constraint)
        {
            cpDataPointer handle = NativeMethods.cpShapeGetUserData(constraint);
            return NativeInterop.FromIntPtr<Shape>(handle);
        }

        protected virtual void Dispose(bool dispose)
        {
            if (!dispose)
            {
                Debug.WriteLine("Disposing shape {0} on finalizer... (consider Dispose explicitly)", shape);
            }
            Free();
        }

        public void Free()
        {
            ReleaseUserData();
            NativeMethods.cpBodyFree(shape);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Shape()
        {
            Dispose(false);
        }

        /// <summary>
        /// Update, cache and return the bounding box of a shape based on the body it's attached to.
        /// </summary>
        /// <returns></returns>
        public cpBB CacheBB()
        {
            return NativeMethods.cpShapeCacheBB(shape);
        }

        /// <summary>
        /// Update, cache and return the bounding box of a shape with an explicit transformation.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public cpBB Update(cpTransform transform)
        {
            return NativeMethods.cpShapeUpdate(shape, transform);
        }

        /// <summary>
        /// Perform a nearest point query. It finds the closest point on the surface of shape to a specific point.
        /// The value returned is the distance between the points. A negative distance means the point is inside the shape.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public double PointQuery(cpVect point, out PointQueryInfo info)
        {
            var output = new cpPointQueryInfo();
            double distance = NativeMethods.cpShapePointQuery(shape, point, ref output);

            info = PointQueryInfo.FromQueryInfo(output);

            return distance;
        }

        /// <summary>
        /// Perform a segment query against a shape. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        //public bool SegmentQuery(cpVect a, cpVect b, double radius, SegmentQueryInfo info)
        //{
        //    cpSegmentQueryInfo queryInfo = info.ToQueryInfo();
        //    return NativeMethods.cpShapeSegmentQuery(shape, a, b, radius, ref queryInfo) != 0;
        //}


    }
}
