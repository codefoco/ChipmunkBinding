using System;

namespace ChipmunkBinding
{
    /// <summary>
    /// March data used for <see cref="AutoGeometry"/>.
    /// </summary>
    public class MarchData
    {
        /// <summary>
        /// The bounding box.
        /// </summary>
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// The number of horizontal samples.
        /// </summary>
        public int XSamples { get; set; }

        /// <summary>
        /// The number of vertical samples.
        /// </summary>
        public int YSamples { get; set; }

        /// <summary>
        /// The threshold.
        /// </summary>
        public double Threshold { get; set; }

        /// <summary>
        /// Callback for sampling/
        /// </summary>
        public Func<Vect,object, double> SampleFunction { get; set; }

        /// <summary>
        /// Callback for segmentation.
        /// </summary>
        public Action<Vect,Vect, object> SegmentFunction { get; set; }

        /// <summary>
        /// User sample data.
        /// </summary>
        public object SampleData { get; set; }

        /// <summary>
        /// User segmentation data.
        /// </summary>
        public object SegmentData { get; set; }
    }
}
