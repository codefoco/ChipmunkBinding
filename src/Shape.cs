using System;

using cpShape = System.IntPtr;
using cpSpace = System.IntPtr;
using cpBody = System.IntPtr;

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

        // Shape properties

        /// <summary>
        /// The Space this shape (of body) is added to.
        /// </summary>
        public Space Space
        {
            get
            {
                cpSpace space = NativeMethods.cpShapeGetSpace(shape);
                return Space.FromHandleSafe(space);
            }
        }

        /// <summary>
        /// The Body this body is added to.
        /// </summary>
        public Body Body
        {
            get
            {
                cpBody space = NativeMethods.cpShapeGetBody(shape);
                return Body.FromHandleSafe(space);
            }
            set
            {
                Debug.Assert(value != null && Space == null, "Body can't be null and you can only change body if the shape wasn't added to space");
                NativeMethods.cpShapeSetBody(shape, value.Handle);
            }
        }

        /// <summary>
        /// Mass of this shape to have Chipmunk calculate mass properties for you.
        /// </summary>
        public double Mass
        {
            get => NativeMethods.cpShapeGetMass(shape);
            set => NativeMethods.cpShapeSetMass(shape, value);
        }

        /// <summary>
        /// Density of the shape if you are having Chipmunk calculate mass properties for you.
        /// </summary>
        public double Density
        {
            get => NativeMethods.cpShapeGetDensity(shape);
            set => NativeMethods.cpShapeSetDensity(shape, value);
        }

        /// <summary>
        /// Get the calculated moment of inertia for this shape.
        /// </summary>
        public double Moment => NativeMethods.cpShapeGetMoment(shape);

        /// <summary>
        /// Get the calculated area of this shape.
        /// </summary>
        public double Area => NativeMethods.cpShapeGetArea(shape);

        /// <summary>
        /// Get the centroid of this shape.
        /// </summary>
        public cpVect CenterOfGravity => NativeMethods.cpShapeGetCenterOfGravity(shape);

        /// <summary>
        /// Get the bounding box that contains the shape given it's current position and angle.
        /// </summary>
        public cpBB BoundingBox => NativeMethods.cpShapeGetBB(shape);

        /// <summary>
        /// Enable/Disable shape is set to be a sensor or not.
        /// </summary>
        public bool Sensor
        {
            get => NativeMethods.cpShapeGetSensor(shape) != 0;
            set => NativeMethods.cpShapeSetSensor(shape, value ? (byte)1 : (byte)0);
        }

        /// <summary>
        /// Elasticity of this shape.
        /// 
        /// A value of 0.0 gives no bounce, while a value of 1.0 will give a ‘perfect’ bounce. However due to inaccuracies in the simulation using 1.0 or greater is not recommended.
        /// </summary>
        public double Elasticity
        {
            get => NativeMethods.cpShapeGetElasticity(shape);
            set => NativeMethods.cpShapeSetElasticity(shape, value);
        }

        /// <summary>
        /// Friction coefficient.
        /// ChipmunkBinding uses the Coulomb friction model, a value of 0.0 is frictionless.
        /// https://en.wikipedia.org/wiki/Friction#Coefficient_of_friction
        /// </summary>
        public double Friction
        {
            get => NativeMethods.cpShapeGetFriction(shape);
            set => NativeMethods.cpShapeSetFriction(shape, value);
        }

        /// <summary>
        ///  The surface velocity of the object.
        ///  Useful for creating conveyor belts or players that move around.This value is only used when calculating friction, not resolving the collision.
        /// </summary>
        public cpVect SurfaceVelocity
        {
            get => NativeMethods.cpShapeGetSurfaceVelocity(shape);
            set => NativeMethods.cpShapeSetSurfaceVelocity(shape, value);
        }

        /// <summary>
        /// Collision type of this shape.
        /// </summary>
        public int CollisionType
        {
            get => (int)(uint)NativeMethods.cpShapeGetCollisionType(shape);
            set => NativeMethods.cpShapeSetCollisionType(shape,  (UIntPtr)(uint)value);
        }

        /// <summary>
        /// Set the collision ShapeFilter for this shape.
        /// </summary>
        public ShapeFilter Filter
        {
            get
            {
                cpShapeFilter shapeFilter = NativeMethods.cpShapeGetFilter(shape);
                return shapeFilter.ToShapeFilter();
            }
            set
            {
                var shapeFilter = cpShapeFilter.FromShapeFilter(value);
                NativeMethods.cpShapeSetFilter(shape, shapeFilter);
            }
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
        /// </summary>
        /// <param name="point"></param>
        /// <param name="info"></param>
        /// <returns>the point query info</returns>
        public PointQueryInfo PointQuery(cpVect point)
        {
            var output = new cpPointQueryInfo();

            NativeMethods.cpShapePointQuery(shape, point, ref output);

            return PointQueryInfo.FromQueryInfo(output);
        }

        /// <summary>
        /// Perform a segment query against a shape. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public SegmentQueryInfo SegmentQuery(cpVect a, cpVect b, double radius)
        {
            var queryInfo = new cpSegmentQueryInfo();
            NativeMethods.cpShapeSegmentQuery(shape, a, b, radius, ref queryInfo);

            return SegmentQueryInfo.FromQueryInfo(queryInfo);
        }

        /// <summary>
        /// Get contact information about this shape and other shape.
        /// </summary>
        /// <param name="shape2"></param>
        /// <returns></returns>
        public ContactPointSet Collide(Shape other)
        {
            Debug.Assert(System.Runtime.InteropServices.Marshal.SizeOf(typeof(cpContactPointSet)) == 104, "check Chipmunk sizeof(cpContactPointSet)");

            cpContactPointSet contactPointSet = NativeMethods.cpShapesCollide(shape, other.Handle);

            return ContactPointSet.FromContactPointSet(contactPointSet);
        }


    }
}
