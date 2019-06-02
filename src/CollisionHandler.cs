using System;

using cpCollisionHandlerPointer = System.IntPtr;

using voidptr_t = System.IntPtr;
using cpArbiter = System.IntPtr;
using cpSpace = System.IntPtr;
using cpBool = System.Byte;
using System.Diagnostics;

#if __IOS__ || __TVOS__ || __WATCHOS__
using ObjCRuntime;
#endif

namespace ChipmunkBinding
{
    public sealed class CollisionHandler<T> where T : class
    {
        private readonly cpCollisionHandlerPointer handle;

        private static CollisionBeginFunction beginCallback = CollisionBeginFunctionCallback;
        private static CollisionPreSolveFunction preSolveCallback = CollisionPreSolveFunctionCallback;
        private static CollisionPostSolveFunction postSolveCallback = CollisionPostSolveFunctionCallback;
        private static CollisionSeparateFunction separeteCallback = CollisionSeparateFunctionCallback;


        private CollisionHandler(cpCollisionHandlerPointer collisionHandle, ref cpCollisionHandler handler)
        {
            handle = collisionHandle;

            IntPtr data = NativeInterop.RegisterHandle(this);

            handler.userData = data;

            TypeA = (int)handler.typeA;
            TypeB = (int)handler.typeB;

            cpCollisionHandler.ToPointer(handler, handle);
        }

        internal static CollisionHandler<T> GetOrCreate(cpCollisionHandlerPointer collisionHandle)
        {
            Debug.Assert(collisionHandle != IntPtr.Zero, "CollisionHandle cannot be zero");

            var handler = cpCollisionHandler.FromHandle(collisionHandle);
            if (handler.userData != IntPtr.Zero)
                return NativeInterop.FromIntPtr<CollisionHandler<T>>(handler.userData);

            return new CollisionHandler<T>(collisionHandle, ref handler);
        }

        private Func<Arbiter, Space, T, bool> begin;
        /// <summary>
        /// This function is called when two shapes with types that match this collision handler begin colliding
        /// </summary>
        public Func<Arbiter, Space, T, bool> Begin
        {
            set
            {
                begin = value;

                var handler = cpCollisionHandler.FromHandle(handle);

                IntPtr callbackPointer;
                if (value == null)
                    callbackPointer = IntPtr.Zero;
                else
                    callbackPointer = beginCallback.ToFunctionPointer();

                handler.beginFunction = callbackPointer;

                cpCollisionHandler.ToPointer(handler, handle);
            }
            get => begin;
        }

        private Func<Arbiter, Space, T, bool> preSolve;
        /// <summary>
        /// This function is called each step when two shapes with types that match this collision handler are colliding.
        /// It's called before the collision solver runs so that you can affect a collision's outcome.
        /// </summary>
        public Func<Arbiter, Space, T, bool> PreSolve
        {
            set
            {
                preSolve = value;

                var handler = cpCollisionHandler.FromHandle(handle);

                IntPtr callbackPointer;
                if (value == null)
                    callbackPointer = IntPtr.Zero;
                else
                    callbackPointer = preSolveCallback.ToFunctionPointer();

                handler.preSolveFunction = callbackPointer;

                cpCollisionHandler.ToPointer(handler, handle);

            }
            get => preSolve;
        }

        private Action<Arbiter, Space, T> postSolve;
        /// <summary>
        /// This function is called each step when two shapes with types that match this collision handler are colliding.
        /// It's called after the collision solver runs so that you can read back information about the collision to trigger events in your game.
        /// </summary>
        public Action<Arbiter, Space, T> PostSolve
        {
            set
            {
                postSolve = value;

                var handler = cpCollisionHandler.FromHandle(handle);

                IntPtr callbackPointer;
                if (value == null)
                    callbackPointer = IntPtr.Zero;
                else
                    callbackPointer = postSolveCallback.ToFunctionPointer();

                handler.postSolveFunction = callbackPointer;

                cpCollisionHandler.ToPointer(handler, handle);

            }
            get => postSolve;
        }

        private Action<Arbiter, Space, T> separete;
        /// <summary>
        /// This function is called when two shapes with types that match this collision handler stop colliding.
        /// </summary>
        public Action<Arbiter, Space, T> Separete
        {
            set
            {
                separete = value;

                var handler = cpCollisionHandler.FromHandle(handle);

                IntPtr callbackPointer;
                if (value == null)
                    callbackPointer = IntPtr.Zero;
                else
                    callbackPointer = separeteCallback.ToFunctionPointer();

                handler.separateFunction = callbackPointer;

                cpCollisionHandler.ToPointer(handler, handle);

            }
            get => separete;
        }

        /// <summary>
        /// User definable context pointer that is passed to all of the collision handler functions.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        ///  In the collision handler callback, the shape with this type will be the first argument. Read only.
        /// </summary>
        public int TypeA { get; }

        /// <summary>
        /// In the collision handler callback, the shape with this type will be the second argument. Read only.
        /// </summary>
        public int TypeB { get; }

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(CollisionBeginFunction))]
#endif
        private static cpBool CollisionBeginFunctionCallback(cpArbiter arbiterHandle, cpSpace spaceHandle, voidptr_t userData)
        {
            var arbiter = new Arbiter(arbiterHandle);
            var space = Space.FromHandle(spaceHandle);

            var handler = NativeInterop.FromIntPtr<CollisionHandler<T>>(userData);
            var begin = handler.Begin;
            if (begin == null)
                return 1;

            if (begin(arbiter, space, handler.Data))
                return 1;

            return 0;
        }

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(CollisionPreSolveFunction))]
#endif
        private static cpBool CollisionPreSolveFunctionCallback(cpArbiter arbiterHandle, cpSpace spaceHandle, voidptr_t userData)
        {
            var arbiter = new Arbiter(arbiterHandle);
            var space = Space.FromHandle(spaceHandle);

            var handler = NativeInterop.FromIntPtr<CollisionHandler<T>>(userData);
            var preSolve = handler.PreSolve;
            if (preSolve == null)
                return 1;

            if (preSolve(arbiter, space, handler.Data))
                return 1;

            return 0;
        }

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(CollisionPostSolveFunction))]
#endif
        private static void CollisionPostSolveFunctionCallback(cpArbiter arbiterHandle, cpSpace spaceHandle, voidptr_t userData)
        {
            var arbiter = new Arbiter(arbiterHandle);
            var space = Space.FromHandle(spaceHandle);

            var handler = NativeInterop.FromIntPtr<CollisionHandler<T>>(userData);
            var postSolve = handler.PostSolve;
            if (postSolve == null)
                return;

            postSolve(arbiter, space, handler.Data);
        }

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(SpaceDebugDrawColorForShapeImpl))]
#endif
        private static void CollisionSeparateFunctionCallback(cpArbiter arbiterHandle, cpSpace spaceHandle, voidptr_t userData)
        {
            var arbiter = new Arbiter(arbiterHandle);
            var space = Space.FromHandle(spaceHandle);

            var handler = NativeInterop.FromIntPtr<CollisionHandler<T>>(userData);
            var separate = handler.Separete;
            if (separate == null)
                return;

            separate(arbiter, space, handler.Data);
        }
    }
}
