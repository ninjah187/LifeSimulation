using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using LifeSimulation.Core;

namespace LifeSimulation.Tests
{
    public class PointTest
    {
        static IEnumerable<object[]> OperatorMinus_WithTwoPoints_ReturnsAppropriateVector_Data()
        {
            yield return new object[] { 0, 0, 2, 2, new Vector(2, 2) };
            yield return new object[] { 2, 2, 5, 5, new Vector(3, 3) };
        }

        [Theory, MemberData(nameof(OperatorMinus_WithTwoPoints_ReturnsAppropriateVector_Data))]
        void OperatorMinus_WithTwoPoints_ReturnsAppropriateVector(double x1, double y1, double x2, double y2,
                                                                  Vector expectedVector)
        {
            var p1 = new Point(x1, y1);
            var p2 = new Point(x2, y2);

            var vector = p2 - p1;

            Assert.Equal(expectedVector, vector);
        }
    }
}
