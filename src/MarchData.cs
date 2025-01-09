// .      ______          __     ____               
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
        public Func<Vect, object, double> SampleFunction { get; set; }

        /// <summary>
        /// Callback for segmentation.
        /// </summary>
        public Action<Vect, Vect, object> SegmentFunction { get; set; }

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