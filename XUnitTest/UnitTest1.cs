using System;
using GeoInfoTool;
using Xunit;

namespace XUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            var referenceOrigin = new ReferenceOriginWgs84Point(new Wgs84Point { Lat = 25.0783615, Lon = 121.5750212 });
            var targetWgs84Point = new Wgs84Point
            {
                Lat = 25.0806617,
                Lon = 121.5749139
            };
            var desiredTargetCartesian = (X: -10.8250645005394, Y: 254.80489950534);
            const string roundFormat = "0.##########";

            //Act
            var transformTool = new Wgs84Transform(referenceOrigin);
            var transformedOrigin = transformTool.TransformToCartesian(referenceOrigin);
            var transformedTarget = transformTool.TransformToCartesian(targetWgs84Point);

            //Assert
            Assert.Equal(0.0.ToString(roundFormat), transformedOrigin.X.ToString(roundFormat));
            Assert.Equal(0.0.ToString(roundFormat), transformedOrigin.Y.ToString(roundFormat));

            var xStr = desiredTargetCartesian.X.ToString(roundFormat);
            Assert.Equal(desiredTargetCartesian.X.ToString(roundFormat), transformedTarget.X.ToString(roundFormat));
            Assert.Equal(desiredTargetCartesian.Y.ToString(roundFormat), transformedTarget.Y.ToString(roundFormat));
        }
    }
}
