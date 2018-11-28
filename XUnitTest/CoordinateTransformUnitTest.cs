using System;
using GeoInfoTool;
using Xunit;

namespace XUnitTest
{
    public class CoordinateTransformUnitTest
    {
        [Fact]
        public void TestTransformToTuple()
        {
            //Arrange
            var referenceOrigin = new ReferenceOriginWgs84Point(new Wgs84Point { Lat = 25.0783615, Lon = 121.5750212 });
            var targetWgs84Point = new Wgs84Point
            {
                Lat = 25.0806617,
                Lon = 121.5749139
            };
            var desiredTargetCartesian = (X: -10.8250645005394, Y: 254.80489950534);
            const int precision = 7;

            //Act
            var transformTool = new Wgs84Transform(referenceOrigin);
            var transformedOrigin = transformTool.TransformToCartesian(referenceOrigin);
            var transformedTarget = transformTool.TransformToCartesian(targetWgs84Point);

            //Assert
            Assert.Equal(0.0, transformedOrigin.X, precision);
            Assert.Equal(0.0, transformedOrigin.Y, 6);

            Assert.Equal(desiredTargetCartesian.X, transformedTarget.X, precision);
            Assert.Equal(desiredTargetCartesian.Y, transformedTarget.Y, precision);
        }

    }
}
