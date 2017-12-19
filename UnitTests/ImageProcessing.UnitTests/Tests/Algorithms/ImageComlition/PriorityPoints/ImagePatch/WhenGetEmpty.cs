﻿#if UNIT_TESTS
using System.Drawing;
using NUnit.Framework;
using Rhino.Mocks;
using Zavolokas.GdiHelpers;
using Zavolokas.ImageProcessing.Algorithms.ImageComplition;
using Zavolokas.ImageProcessing.Algorithms.ImageComplition.PriorityPoints;
using Zavolokas.ImageProcessing.Primitives;
using Point = Zavolokas.ImageProcessing.Primitives.Point;

namespace Tests
{
    [TestFixture]
    class WhenGetEmpty
    {
        private PatchSourceImageBase image;
        private IPatchDistanceCalculator distanceCalculator;

        [SetUp]
        public void Setup()
        {
            var mocks = new MockRepository();
            distanceCalculator = mocks.DynamicMock<IPatchDistanceCalculator>();

            int width = 10;
            int height = 10;
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.FillRectangle(Brushes.Red, 0, 0, width, height);
                g.FillRectangle(Brushes.Green, 3, 3, width - 3 * 2, height - 3 * 2);
            }

            RgbColor[][] rgbImage = Utils.Bitmap2RgbArray(bitmap);

            image = mocks.DynamicMock<PatchSourceImageBase>(rgbImage, distanceCalculator);
        }

        [Test]
        public void IsEmptyShouldReturnTrue()
        {
            ImagePatch emptyPatch = ImagePatch.Empty;
            Assert.IsTrue(emptyPatch.IsEmpty);
        }

        [Test]
        public void IsEmptyShouldReturnFalse()
        {
            int patchSize = 9;
            var pos = new Point(5, 5);
            ImagePatch patch = new ImagePatch(pos, patchSize, image, distanceCalculator);

            Assert.IsFalse(patch.IsEmpty);
        }
    }
}
#endif