using System;

namespace ChipmunkBinding
{
    /// <summary>
    /// March Data used for Autogeometry
    /// </summary>
    public class MarchData
    {
        /// <summary>
        /// Bouding box
        /// </summary>
        public BoundingBox BoundingBox { get; set; }
        /// <summary>
        /// Number of X samples
        /// </summary>
        public int XSamples { get; set; }

        /// <summary>
        /// Number of Y samples
        /// </summary>
        public int YSamples { get; set; }

        /// <summary>
        /// Threshold
        /// </summary>
        public double Threshold { get; set; }

        /// <summary>
        /// Callback for sampling
        /// </summary>
        public Func<Vect,object, double> SampleFunction { get; set; }

        /// <summary>
        /// Callback for Segmentation
        /// </summary>
        public Action<Vect,Vect, object> SegmentFunction { get; set; }

        /// <summary>
        /// User sample data
        /// </summary>
        public object SampleData { get; set; }

        /// <summary>
        /// User segmentation data
        /// </summary>
        public object SegmentData { get; set; }
    }
}
