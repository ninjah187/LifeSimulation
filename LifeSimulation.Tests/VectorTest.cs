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
    public class VectorTest
    {
        static IEnumerable<object[]> Length_IsProperlyCalculated_Data()
        {
            yield return new object[] { 4, 3, 5 };
            yield return new object[] { 3, 3, Math.Sqrt(18) };
        }

        [Theory, MemberData(nameof(Length_IsProperlyCalculated_Data))]
        void Length_IsProperlyCalculated(double x, double y, double expectedLength)
        {
            var vector = new Vector(x, y);

            var length = vector.Length;

            Assert.Equal(expectedLength, length);
        }

        void OperatorMinus_NegatesVectorCoordinates()
        {
            var vector = new Vector(1, 1);

            var negate = -vector;

            Assert.Equal(-1, negate.X);
            Assert.Equal(-1, negate.Y);
        }
    }
}
