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

            Assert.That(!result.Any(x => x < 3 || x > 3 + 5 ));
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
    }
}
