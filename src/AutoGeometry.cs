using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#if __IOS__ || __TVOS__ || __WATCHOS__
using ObjCRuntime;
#endif

namespace ChipmunkBinding
{
    /// <summary>
    /// This class contains functions for automatic generation of geometry.
    /// </summary>
    public static class AutoGeometry
    {
#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(MarchSegmentFunction))]
#endif
        private static void MarchSegmentFunctionCallback(Vect v0, Vect v1, IntPtr data)
        {
            var marchData = (MarchData)GCHandle.FromIntPtr(data).Target;
            marchData.SegmentFunction(v0, v1, marchData.SegmentData);
        }

        private static MarchSegmentFunction segmentFunctionCallback = MarchSegmentFunctionCallback;

#if __IOS__ || __TVOS__ || __WATCHOS__
        [MonoPInvokeCallback(typeof(MarchSampleFunction))]
#endif
        private static double MarchSampleFunctionCallBack(Vect point, IntPtr data)
        {
            var marchData = (MarchData)GCHandle.FromIntPtr(data).Target;
            return marchData.SampleFunction(point, marchData.SampleData);
        }

        private static MarchSampleFunction sampleFunctionCallBack = MarchSampleFunctionCallBack;

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
