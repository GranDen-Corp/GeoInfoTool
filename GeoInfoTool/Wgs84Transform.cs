using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeoAPI.CoordinateSystems;
using GeoAPI.CoordinateSystems.Transformations;
using ProjNet.CoordinateSystems;

namespace GeoInfoTool
{
    public class Wgs84Transform
    {
        private ReferenceOriginWgs84Point _referenceOrigin;

        public Wgs84Transform(ReferenceOriginWgs84Point referenceOrigin)
        {
            _referenceOrigin = referenceOrigin;
        }

        #region API Methods

        public (double X, double Y) TransformToCartesian(Wgs84Point wgs84Point)
        {
            var transform = GetCartesianTransform(ref _referenceOrigin);
            var rawResult = transform.MathTransform.Transform(new[]
            {
                wgs84Point.Lon,
                wgs84Point.Lat
            });

            return (X: rawResult[0], Y: rawResult[1] + _referenceOrigin.ShiftedY);
        }

        public CartesianPoint TransformToCartesianPoint(Wgs84Point wgs84Point)
        {
            var transform = GetCartesianTransform(ref _referenceOrigin);
            var rawResult = transform.MathTransform.Transform(new[]
            {
                wgs84Point.Lon,
                wgs84Point.Lat
            });

            return new CartesianPoint
            {
                X = rawResult[0],
                Y = rawResult[1] + _referenceOrigin.ShiftedY
            };
        }

        public static (double X, double Y) TransformToCartesian(Wgs84Point wgs84Point, ref ReferenceOriginWgs84Point referenceOrigin)
        {
            var transform = GetCartesianTransform(ref referenceOrigin);
            var rawResult = transform.MathTransform.Transform(new[]
            {
                wgs84Point.Lon,
                wgs84Point.Lat
            });

            return (X: rawResult[0], Y: rawResult[1] + referenceOrigin.ShiftedY);
        }

        public static CartesianPoint TransformToCartesianPoint(Wgs84Point wgs84Point, ref ReferenceOriginWgs84Point referenceOrigin)
        {
            var transform = GetCartesianTransform(ref referenceOrigin);
            var rawResult = transform.MathTransform.Transform(new[]
            {
                wgs84Point.Lon,
                wgs84Point.Lat
            });

            return new CartesianPoint
            {
                X = rawResult[0],
                Y = rawResult[1] + referenceOrigin.ShiftedY
            };
        }

        #endregion


        private static ICoordinateTransformation GetCartesianTransform(ref ReferenceOriginWgs84Point referenceOrigin)
        {
            if (referenceOrigin == null)
            {
                throw new ArgumentException("reference origin point must have value");
            }

            var csFactory = new CoordinateSystemFactory();

            var projectionParameters = new List<ProjectionParameter>
            {
                new ProjectionParameter("latitude_of_origin", referenceOrigin.Lat),
                new ProjectionParameter("central_meridian", referenceOrigin.Lon),
                new ProjectionParameter("false_easting", 0),
                new ProjectionParameter("false_northing", 0)
            };
            var projection = csFactory.CreateProjection("Mercator_1SP", "Mercator", projectionParameters);
            IProjectedCoordinateSystem projectedCoordinateSystem = csFactory.CreateProjectedCoordinateSystem("My WGS84", GeographicCoordinateSystem.WGS84,
                projection, LinearUnit.Metre, new AxisInfo("E", AxisOrientationEnum.East),
                new AxisInfo("N", AxisOrientationEnum.North));
            referenceOrigin.ProjectedCoordinateSystem = projectedCoordinateSystem;
            var ctFactory = new ProjNet.CoordinateSystems.Transformations.CoordinateTransformationFactory();
            var transform = ctFactory.CreateFromCoordinateSystems(GeographicCoordinateSystem.WGS84, projectedCoordinateSystem);
            var transformedPoint =
                transform.MathTransform.Transform(new[] { referenceOrigin.Lon, referenceOrigin.Lat });
            var shift = transformedPoint[1] * -1;
            referenceOrigin.ShiftedY = shift;

            return transform;
        }


        
    }
}
