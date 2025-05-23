﻿// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2025 Codefoco LTDA - The above copyright notice and this permission notice shall be
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
    /// Similar to <see cref="Space"/>, but with ARM NEON optimizations in the solver.
    /// </summary>
    public sealed class HastySpace : Space
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
        public sealed override void Step(double dt)
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