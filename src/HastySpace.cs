using System;
namespace ChipmunkBinding
{
    /// <summary>
    /// Similar to <see cref="Space"/>, but with ARM NEON optimizations in the solver.
    /// </summary>
    public class HastySpace : Space
    {
        /// <summary>
        /// On ARM platforms that support NEON, this will enable the vectorized solver.
        /// <see cref="HastySpace"/> also supports multiple threads, but runs single threaded by
        /// default for determinism.
        /// </summary>
        public HastySpace()
            : base(NativeMethods.cpHastySpaceNew())
        {
        }

        /// <summary>
        /// The number of threads to use for the solver. Currently Chipmunk is limited to 2 threads
        /// as using more generally provides very minimal performance gains. Passing 0 as the thread
        /// count on iOS or OS X will cause Chipmunk to automatically detect the number of threads
        /// it should use. On other platforms passing 0 for the thread count will set 1 thread.
        /// </summary>
        public int Threads
        {
            get => (int)NativeMethods.cpHastySpaceGetThreads(Handle);
            set => NativeMethods.cpHastySpaceSetThreads(Handle, (uint)value);
        }

        /// <summary>
        /// Step in the hasty space.
        /// </summary>
        public override void Step(double dt)
        {
            NativeMethods.cpHastySpaceStep(Handle, dt);
        }

        /// <summary>
        /// Destroy and free the hasty space.
        /// </summary>
        protected override void FreeSpace(IntPtr handle)
        {
            NativeMethods.cpHastySpaceFree(handle);
        }
    }
}
