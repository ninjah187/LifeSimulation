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
    public class CircleHitBoxTest
    {
        Mock<ICircleHitBox> otherHitBoxMock;
        CircleHitBox _hitBox;

        public CircleHitBoxTest()
        {
            otherHitBoxMock = new Mock<ICircleHitBox>();
            _hitBox = new CircleHitBox();
        }

        [Fact]
        void Update_UpdatesCenterToCenterOfGameObjectAndRadiusToRadiusOfGameObject()
        {
            var gameObjectMock = new Mock<IGameObject>();
            gameObjectMock.SetupGet(m => m.Size).Returns(4);
            gameObjectMock.SetupGet(m => m.Position).Returns(new Point(0, 0));

            _hitBox.Update(gameObjectMock.Object);

            Mock.VerifyAll(gameObjectMock);
            Assert.Equal(new Point(2, 2), _hitBox.Center);
            Assert.Equal(2, _hitBox.Radius);
        }

        static IEnumerable<object[]> Collides_DoesntCollideWithOtherHitBox_ReturnsFalse_Data()
        {
            yield return new object[] { 2, 2, 2, 5, 5, 2 };
            yield return new object[] { 2, 2, 2, 6, 2, 2 };
        }

        [Theory, MemberData(nameof(Collides_DoesntCollideWithOtherHitBox_ReturnsFalse_Data))]
        void Collides_DoesntCollideWithOtherHitBox_ReturnsFalse(double x1, double y1, double r1,
                                                                double x2, double y2, double r2)
        {
            //otherHitBoxMock.Setup(m => m.Center)
            var otherHitBox = new CircleHitBox();
            otherHitBox.Center = new Point(x1, y1);
            otherHitBox.Radius = r1;
            _hitBox.Center = new Point(x2, y2);
            _hitBox.Radius = r2;

            var collides = _hitBox.Collides(otherHitBox);

            Assert.False(collides);
        }

        static IEnumerable<object[]> Collides_CollidesWithOtherHitBox_ReturnsTrue_Data()
        {
            yield return new object[] { 2, 2, 2, 3, 2, 2 };
        }

        [Theory, MemberData(nameof(Collides_CollidesWithOtherHitBox_ReturnsTrue_Data))]
        void Collides_CollidesWithOtherHitBox_ReturnsTrue(double x1, double y1, double r1,
                                                          double x2, double y2, double r2)
        {
            //otherHitBoxMock.SetupGet(m => m.Center).Returns(new Point(5, 5));
            //otherHitBoxMock.SetupGet(m => m.Radius).Returns(2);
            var otherHitBox = new CircleHitBox();
            otherHitBox.Center = new Point(x1, y1);
            otherHitBox.Radius = r1;
            _hitBox.Center = new Point(x2, y2);
            _hitBox.Radius = r2;

            var collides = _hitBox.Collides(otherHitBox);

            //Mock.VerifyAll(otherHitBoxMock);
            Assert.True(collides);
        }
    }
}
