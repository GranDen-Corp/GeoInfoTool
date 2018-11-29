using GeoAPI.CoordinateSystems;

namespace GeoInfoTool
{
    public class Wgs84Point
    {
        /// <summary>
        /// WGS84 Longitude
        /// </summary>
        public double Lon { get; set; }

        /// <summary>
        /// WGS84 Latitude
        /// </summary>
        public double Lat { get; set; }
    }

    public class ReferenceOriginWgs84Point : Wgs84Point
    {
        public ReferenceOriginWgs84Point(Wgs84Point point)
        {
            Lon = point.Lon;
            Lat = point.Lat;
        }
        
        /// <summary>
        /// The deviation Y-axis value that needs to add after transform back
        /// </summary>
        public double ShiftedY { get; set; }

        /// <summary>
        /// The calculated Projected Coordinate System, should not be modified arbitrarily.
        /// </summary>
        public IProjectedCoordinateSystem ProjectedCoordinateSystem { get; set; }
    }

    public class CartesianPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}