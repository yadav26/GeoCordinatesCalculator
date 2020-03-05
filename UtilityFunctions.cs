using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class UtilityFunctions
    {
        public static double radiansInDegree = Math.PI / 180.0;

        public class Location
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double sd { get; set; }
        }
        static double toRadians(double degrees)
        {
            return degrees * radiansInDegree;
        }
        public static double distanceTo(double srcLat, double srcLong, double destLat, double destLong)
        {
            // see mathforum.org/library/drmath/view/51879.html for derivation
            double radius = 6371e3;
            var phi1 = toRadians(srcLat);
            var lambda1 = toRadians(srcLong);
            var phi2 = toRadians(destLat);
            var lambda2 = toRadians(destLong);
            var deltaPhi = phi2 - phi1;
            var deltaLambda = lambda2 - lambda1;

            var a = Math.Sin(deltaPhi / 2.0) * Math.Sin(deltaPhi / 2.0) +
                 Math.Cos(phi1) * Math.Cos(phi2) *
                 Math.Sin(deltaLambda / 2.0) * Math.Sin(deltaLambda / 2.0);
            var c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

            return radius * c;
        }

        public static Location getShortestDistanceToLine( double X1, double Y1, double X2, double Y2, double X3, double Y3)
        {
            double XX=X2-X1;
            double YY=Y2-Y1;
            double distance = ((XX * (X3 - X1)) + (YY * (Y3 - Y1))) / ((XX * XX) + (YY * YY));
            double X4 = X1 + XX * distance;
            double Y4 = Y1 + YY * distance;

            if( (X4 < X2) && (X4> X1) && (Y4<Y2) && (Y4 > Y1) )
                return new Location() { X=X4, Y= Y4, sd = distance };

            return null;


        }
/*  def get_perp(X1, Y1, X2, Y2, X3, Y3):
      """************************************************************************************************ 
      Purpose - X1,Y1,X2,Y2 = Two points representing the ends of the line segment
                X3, Y3 = The offset point 
      'Returns - X4,Y4 = Returns the Point on the line perpendicular to the offset or None if no such
                          point exists
      '************************************************************************************************ """
      XX = X2 - X1
      YY = Y2 - Y1
      ShortestLength = ((XX* (X3 - X1)) + (YY* (Y3 - Y1))) / ((XX* XX) + (YY* YY)) 
      X4 = X1 + XX* ShortestLength
      Y4 = Y1 + YY* ShortestLength
      if X4<X2 and X4> X1 and Y4<Y2 and Y4 > Y1:
          return X4, Y4
      return None

      */

        /*
* transform
*
* Parameters:
*     from:     The geodetic position to be translated.
*     from_a:   The semi-major axis of the "from" ellipsoid.
*     from_f:   Flattening of the "from" ellipsoid.
*     from_esq: Eccentricity-squared of the "from" ellipsoid.
*     da:       Change in semi-major axis length (meters); "to" minus "from"    *     df:       Change in flattening; "to" minus "from"
*     dx:       Change in x between "from" and "to" datum.
*     dy:       Change in y between "from" and "to" datum.
*     dz:       Change in z between "from" and "to" datum.
*/
        //public GeodeticPosition transform(GeodeticPosition from,
        //                                   double from_a, double from_f,

        //                                   double from_esq, double da, double df,
        //                                   double dx, double dy, double dz)
        //{
        //    double slat = Math.Sin(from.lat);
        //    double clat = Math.Cos(from.lat);
        //    double slon = Math.Sin(from.lon);
        //    double clon = Math.Cos(from.lon);
        //    double ssqlat = slat * slat;
        //    double adb = 1.0 / (1.0 - from_f);  // "a divided by b"
        //    double dlat, dlon, dh;

        //    double rn = from_a / Math.Sqrt(1.0 - from_esq * ssqlat);
        //    double rm = from_a * (1.0 - from_esq) / Math.Pow((1.0 - from_esq * ssqlat), 1.5);

        //    dlat = (((((-dx * slat * clon - dy * slat * slon) + dz * clat)
        //                + (da * ((rn * from_esq * slat * clat) / from_a)))
        //            + (df * (rm * adb + rn / adb) * slat * clat)))
        //        / (rm + from.h);

        //    dlon = (-dx * slon + dy * clon) / ((rn + from.h) * clat);

        //    dh = (dx * clat * clon) + (dy * clat * slon) + (dz * slat)
        //         - (da * (from_a / rn)) + ((df * rn * ssqlat) / adb);

        //    return new GeodeticPosition(from.lon + dlon, from.lat + dlat, from.h + dh);
        //}
    }
}
