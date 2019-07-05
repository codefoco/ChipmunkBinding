using System;

namespace ChipmunkBinding
{
    public class MarchData
    {
        public BoundingBox BoundingBox { get; set; }
        public int XSamples { get; set; }
        public int YSamples { get; set; }
        public double Threshold { get; set; }
        public Func<Vect,object, double> SampleFunction { get; set; }
        public Action<Vect,Vect, object> SegmentFunction { get; set; }
        public object SampleData { get; set; }
        public object SegmentData { get; set; }
    }
}
