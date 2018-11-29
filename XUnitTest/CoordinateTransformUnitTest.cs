using System;
using GeoInfoTool;
using Xunit;

namespace XUnitTest
{
    public class CoordinateTransformUnitTest
    {
        [Fact]
        public void TestTransformToXyTuple()
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

        [Fact]
        public void TestTransformToCartesianPoint()
        {
            //Arrange
            var referenceOrigin = new ReferenceOriginWgs84Point(new Wgs84Point { Lat = 25.0783615, Lon = 121.5750212 });
            var targetWgs84Point = new Wgs84Point
            {
                Lat = 25.0806617,
                Lon = 121.5749139
            };
            var desiredTargetCartesian = new CartesianPoint{X = -10.8250645005394 , Y = 254.80489950534}; 
            const int precision = 7;

            //Act
            var transformTool = new Wgs84Transform(referenceOrigin);
            var transformedOrigin = transformTool.TransformToCartesianPoint(referenceOrigin);
            var transformedTarget = transformTool.TransformToCartesianPoint(targetWgs84Point);

            //Assert
            Assert.Equal(0.0, transformedOrigin.X, precision);
            Assert.Equal(0.0, transformedOrigin.Y, 6);

            Assert.Equal(desiredTargetCartesian.X, transformedTarget.X, precision);
            Assert.Equal(desiredTargetCartesian.Y, transformedTarget.Y, precision);
        }

        [Fact]
        public void TestTransformBackToWgs84Tuple()
        {
            //Arrange
            var referenceOrigin = new ReferenceOriginWgs84Point(new Wgs84Point { Lat = 25.0783615, Lon = 121.5750212 });
            var transformedTarget = new CartesianPoint{ X = -10.8250645005394, Y = 254.80489950534};
            var desiredWgs84Tuple = (Lat: 25.0806617, Lon: 121.5749139);
            const int precision = 7;

            //Act
            var transformTool = new Wgs84Transform(referenceOrigin);
            var transBackedOrigin = transformTool.TransformBackToTuple(new CartesianPoint {X = 0.0, Y = 0.0});
            var transBackedWgs84Tuple = transformTool.TransformBackToTuple(transformedTarget);

            //Assert
            Assert.Equal(referenceOrigin.Lon, transBackedOrigin.Longitude, precision);
            Assert.Equal(referenceOrigin.Lat, transBackedOrigin.Latitude, precision);

            Assert.Equal(desiredWgs84Tuple.Lon, transBackedWgs84Tuple.Longitude, precision);
            Assert.Equal(desiredWgs84Tuple.Lat, transBackedWgs84Tuple.Latitude, precision);
        }

        [Fact]
        public void TestTransformBackToWgs84Point()
        {
            //Arrange
            var referenceOrigin = new ReferenceOriginWgs84Point(new Wgs84Point { Lat = 25.0783615, Lon = 121.5750212 });
            var transformedTarget = new CartesianPoint { X = -10.8250645005394, Y = 254.80489950534 };
            var desiredWgs84Point = new Wgs84Point{Lat = 25.0806617, Lon = 121.5749139 };
            const int precision = 7;

            //Act
            var transformTool = new Wgs84Transform(referenceOrigin);
            var transBackedOrigin = transformTool.TransformBackToWgs84Point(new CartesianPoint { X = 0.0, Y = 0.0 });
            var transBackedWgs84Point = transformTool.TransformBackToWgs84Point(transformedTarget);

            //Assert
            Assert.Equal(referenceOrigin.Lon, transBackedOrigin.Lon, precision);
            Assert.Equal(referenceOrigin.Lat, transBackedOrigin.Lat, precision);

            Assert.Equal(desiredWgs84Point.Lon, transBackedWgs84Point.Lon, precision);
            Assert.Equal(desiredWgs84Point.Lat, transBackedWgs84Point.Lat, precision);
        }
    }
}
