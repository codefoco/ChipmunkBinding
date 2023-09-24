// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2023 Codefoco LTDA - The above copyright notice and this permission notice shall be
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
// ReSharper disable InconsistentNaming

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
using ObjCRuntime;
#endif

namespace ChipmunkBinding
{
    /// <summary>
    /// This class contains functions for automatic generation of geometry.
    /// </summary>
    public static class AutoGeometry
    {
#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(MarchSegmentFunction))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static void MarchSegmentFunctionCallback(Vect v0, Vect v1, IntPtr data)
        {
            var marchData = (MarchData)GCHandle.FromIntPtr(data).Target;
            marchData.SegmentFunction(v0, v1, marchData.SegmentData);
        }

        private static readonly MarchSegmentFunction segmentFunctionCallback = MarchSegmentFunctionCallback;

#if __IOS__ || __TVOS__ || __WATCHOS__ || __MACCATALYST__
#pragma warning disable CA1416 // Validate platform compatibility
        [MonoPInvokeCallback(typeof(MarchSampleFunction))]
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        private static double MarchSampleFunctionCallBack(Vect point, IntPtr data)
        {
            var marchData = (MarchData)GCHandle.FromIntPtr(data).Target;
            return marchData.SampleFunction(point, marchData.SampleData);
        }

        private static readonly MarchSampleFunction sampleFunctionCallBack = MarchSampleFunctionCallBack;

        /// <summary>
        /// Trace an aliased curve of an image along a particular threshold. The given number of
        /// samples will be taken and spread across the bounding box area using the sampling
        /// function and context. The segment function will be called for each segment detected that
        /// lies along the density contour for the threshold. Only the SegmentData and SampleData are
        /// optional.
        /// </summary>
        public static void MarchHard(MarchData data)
        {
            var gcHandle = GCHandle.Alloc(data);
            IntPtr handlePtr = GCHandle.ToIntPtr(gcHandle);

            NativeMethods.cpMarchHard(
                data.BoundingBox,
                (uint)data.XSamples,
                (uint)data.YSamples,
                data.Threshold,
                segmentFunctionCallback.ToFunctionPointer(),
                handlePtr,
                sampleFunctionCallBack.ToFunctionPointer(),
                handlePtr);

            gcHandle.Free();
        }

        /// <summary>
        /// Trace an anti-aliased contour of an image along a particular threshold. The given number
        /// of samples will be taken and spread across the bounding box area using the sampling
        /// function and context. The segment function will be called for each segment detected that
        /// lies along the density contour for the threshold.
        /// </summary>
        public static void MarchSoft(MarchData data)
        {
            var gcHandle = GCHandle.Alloc(data);
            IntPtr handlePtr = GCHandle.ToIntPtr(gcHandle);

            NativeMethods.cpMarchSoft(
                data.BoundingBox,
                (uint)data.XSamples,
                (uint)data.YSamples,
                data.Threshold,
                segmentFunctionCallback.ToFunctionPointer(),
                handlePtr,
                sampleFunctionCallBack.ToFunctionPointer(),
                handlePtr);

            gcHandle.Free();
        }
    }
}