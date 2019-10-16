using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickPlot.Tests.NUnitTests
{
    [TestFixture]
    public class GenerateTests
    {
        [Test]
        public void Random_ZeroCount_ReturnEmptyArray()
        {
            double[] expected = new double[0];

            var result = Generate.Random(0, 2, 5, 12);

            Assert.Zero(result.Length);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Random_NegativeCount_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Generate.Random(-1, 5, 12, 3));
        }

        [TestCase(5)]
        [TestCase(9)]
        [TestCase(42)]
        [TestCase(7)]
        public void Random_SmallCounts_ArraySizeEqualCount(int count)
        {
            var result = Generate.Random(count, 7, 3, 32);
            Assert.AreEqual(count, result.Length);
        }

        [Test]
        public void Random_SomeParam_AllElementsInsideRange()
        {
            var result = Generate.Random(50, 5, 3);

            Assert.That(!result.Any(x => x < 3 || x > 3 + 5));
        }

        [Test]
        public void Random_EqualSeeds_ProduceEqualArrays()
        {
            var result1 = Generate.Random(50, 12, 7, 42);
            var result2 = Generate.Random(50, 12, 7, 42);

            Assert.AreEqual(result1, result2);
        }

        [Test]
        public void Random_SeedNotSpecified_ProduceDifferentArrays()
        {
            var result1 = Generate.Random(50, 12, 7);
            var result2 = Generate.Random(50, 12, 7);

            Assert.AreNotEqual(result1, result2);
        }
        [Test]
        public void Consecutative_ZeroCount_ReturnsEmptyArray()
        {
            double[] result = Generate.Consecutative(0);
            Assert.IsEmpty(result);
        }
        [Test]
        public void Consecutative_ZeroCountSomeParams_ReturnsEmptyArray()
        {
            double[] result = Generate.Consecutative(0, 3, 15);
            Assert.IsEmpty(result);
        }
        [Test]
        public void Consecutative_NegativeCount_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Generate.Consecutative(-5, 7, 21));
        }

        [TestCase(5)]
        [TestCase(9)]
        [TestCase(42)]
        [TestCase(7)]
        public void Consecutative_SmallCounts_ArraySizeEqualCount(int count)
        {
            var result = Generate.Consecutative(count, 6, 12);
            Assert.AreEqual(count, result.Length);
        }
        [Test]
        public void Consecutative_TenPrecalcNumbers_GenerateCorrect()
        {
            double[] expected = new double[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var result = Generate.Consecutative(10, 1, 0);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Consecutative_TenPrecalcNumbersWithOffset_GenerateCorrect()
        {
            double[] expected = new double[10] { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };
            var result = Generate.Consecutative(10, 2, 2);
            Assert.AreEqual(expected, result);
        }
        [Test]
        public void Consecutative_TenPrecalcNumbersReverce_GenerateCorrect()
        {
            double[] expected = new double[10] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            var result = Generate.Consecutative(10, -1, 10);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Sin_ZeroCount_ReturnEmptyArray()
        {
            double[] result = Generate.Sin(0);
            Assert.IsEmpty(result);
        }

        [Test]
        public void Sin_ZeroCountSomeParams_ReturnEmptyArray()
        {
            double[] result = Generate.Sin(0, 12, 2, 4, 5);
            Assert.IsEmpty(result);
        }

        [Test]
        public void Sin_NegativeCount_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Generate.Sin(-7, 32, 15, 6, 2));
        }

        [TestCase(5)]
        [TestCase(9)]
        [TestCase(42)]
        [TestCase(7)]
        public void Sin_SmallCounts_ArraySizeEqualCount(int count)
        {
            var result = Generate.Sin(count, 6, 12, 5, 3);
            Assert.AreEqual(count, result.Length);
        }

        [TestCase(1, 5, 12)]
        [TestCase(15, 7, 32)]
        [TestCase(0, 3, 1)]
        [TestCase(3, 3, 3)]
        public void Sin_1PointZeroPhase_AlwaysEqualOffset(double oscillations, double offset, double mult)
        {
            double[] result = Generate.Sin(1, oscillations, offset, mult, 0);
            Assert.AreEqual(offset, result[0]);
        }

        [Test]
        public void Sin_5pointsManualCalc_GenerateCorrect()
        {
            double[] expected = new double[] { 0, 1, 0, -1, 0 };
            var result = Generate.Sin(5, 1, 0, 1, 0);
            Assert.That(result, Is.EqualTo(expected).Within(0.0001));
        }

        [Test]
        public void Sin_5pointsWithOffsetManualCalc_GenerateCorrect()
        {
            double[] expected = new double[] { 1, 2, 1, 0, 1 };
            var result = Generate.Sin(5, 1, 1, 1, 0);
            Assert.That(result, Is.EqualTo(expected).Within(0.0001));
        }

        [Test]
        public void Sin_9PointsManualCalc_GenerateCorrect()
        {
            double[] expected = new double[] { 0, 1, 0, -1, 0, 1, 0, -1, 0 };
            var result = Generate.Sin(9, 2, 0, 1, 0);
            Assert.That(result, Is.EqualTo(expected).Within(0.0001));
        }
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(15)]
        [TestCase(21)]
        public void Sin_ShiftByInteger_GenerateEqualResult(int shift)
        {
            double[] expected = new double[] { 0, 1, 0, -1, 0, 1, 0, -1, 0 };
            var result = Generate.Sin(20, 5, 12, 3, 0);
            var resultWithShift = Generate.Sin(20, 5, 12, 3, shift);
            Assert.That(result, Is.EqualTo(resultWithShift).Within(0.0001));
        }
    }
}
