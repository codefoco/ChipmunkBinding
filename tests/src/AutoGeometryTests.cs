// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2024 Codefoco LTDA - The above copyright notice and this permission notice shall be
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
using System.Collections.Generic;
using System.Linq;

using ChipmunkBinding;

using NUnit.Framework;


namespace ChipmunkBindingTest.Tests
{
    [TestFixture]
    public class AutoGeometryTests
    {
        [Test]
        public void MarchSoft()
        {
            string[] img =
            {
                 "  xx   ",
                 "  xx   ",
                 "  xx   ",
                 "  xx   ",
                 "  xx   ",
                 "  xxxxx",
                 "  xxxxx",
            };

            var segments = new List<Tuple<Vect, Vect>>(13);

            var expectedSegments = new List<Tuple<Vect, Vect>>
            {
                new Tuple<Vect, Vect>(new Vect(1.5, 1), new Vect(1.5, 0)),
                new Tuple<Vect, Vect>(new Vect(3.5, 0), new Vect(3.5, 1)),
                new Tuple<Vect, Vect>(new Vect(1.5, 2), new Vect(1.5, 1)),
                new Tuple<Vect, Vect>(new Vect(3.5, 1), new Vect(3.5, 2)),
                new Tuple<Vect, Vect>(new Vect(1.5, 3), new Vect(1.5, 2)),
                new Tuple<Vect, Vect>(new Vect(3.5, 2), new Vect(3.5, 3)),
                new Tuple<Vect, Vect>(new Vect(1.5, 4), new Vect(1.5, 3)),
                new Tuple<Vect, Vect>(new Vect(3.5, 3), new Vect(3.5, 4)),
                new Tuple<Vect, Vect>(new Vect(1.5, 5), new Vect(1.5, 4)),
                new Tuple<Vect, Vect>(new Vect(3.5, 4), new Vect(4, 4.5)),
                new Tuple<Vect, Vect>(new Vect(4, 4.5), new Vect(5, 4.5)),
                new Tuple<Vect, Vect>(new Vect(5, 4.5), new Vect(6, 4.5)),
                new Tuple<Vect, Vect>(new Vect(1.5, 6), new Vect(1.5, 5))
            };

            var marchData = new MarchData
            {
                BoundingBox = new BoundingBox(0, 0, 6, 6),
                XSamples = 7,
                YSamples = 7,
                Threshold = 0.5,
                SegmentFunction = (v0, v1, _) => segments.Add(new Tuple<Vect, Vect>(v0, v1)),
                SampleFunction = (point, _) =>
                {
                    if (img[(int)point.Y][(int)point.X] == 'x')
                        return 1;
                    return 0;
                }
            };

            AutoGeometry.MarchSoft(marchData);

            Assert.IsTrue(expectedSegments.SequenceEqual(segments), "#1");
        }
    }
}