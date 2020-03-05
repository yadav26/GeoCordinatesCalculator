using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XLabs.Platform.Services.Geolocation;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {

        public static List<StationLocation> lsl = new List<StationLocation>();


        static void Main(string[] args)
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"..\..\..\Json\tripJson.json"))
                {
                    string json = sr.ReadToEnd();
                    DistanceRouteSchema deserializedProduct = JsonConvert.DeserializeObject<DistanceRouteSchema>(json);

                    Console.WriteLine(deserializedProduct);

                    double total=0;
                    GeoConverter converter = new GeoConverter("Australian National");
                    
                    Stopwatch stopWatch = Stopwatch.StartNew();
                    
                    for (long  i=0; i< 100000; ++i)//performace check
                    {
                        //randomly select for fresh cordinates.
                        UtilityFunctions.Location offsetLoc = new UtilityFunctions.Location() { X = -37.878866, Y = 144.745703, sd = 0 };

                        foreach (var r in deserializedProduct.routes[0].legs)
                        {
                            foreach (var s in r.steps)
                            {
                                GeoConverter.UTMResult sUTM = converter.convertLatLngToUtm(s.start_location.lat, s.start_location.lng);
                                GeoConverter.UTMResult eUTM = converter.convertLatLngToUtm(s.end_location.lat, s.end_location.lng);
                                GeoConverter.UTMResult offUTM = converter.convertLatLngToUtm(offsetLoc.X, offsetLoc.Y);

                                UtilityFunctions.Location intersectionX4 = UtilityFunctions.getShortestDistanceToLine(sUTM.Easting, sUTM.Northing,
                                    eUTM.Easting, eUTM.Northing, offUTM.Easting, offUTM.Northing);

                                if (intersectionX4 != null)
                                {
                                    GeoConverter.LatLng x4 = converter.convertUtmToLatLng(intersectionX4.X, intersectionX4.Y, sUTM.ZoneNumber, sUTM.ZoneLetter);
                                    //Console.WriteLine("UTM {0},{1} - LatLong {2} {3} d={4}", intersectionX4.X, intersectionX4.Y, x4.Lat, x4.Lng, intersectionX4.sd);
                                }
                                double d = UtilityFunctions.distanceTo(s.start_location.lat, s.start_location.lng, s.end_location.lat, s.end_location.lng);
                                total += d;

                            }
                            //Console.WriteLine("total distance - {0}", total);
                        }
                    }

                    stopWatch.Stop();
                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan ts = stopWatch.Elapsed;
                    Console.WriteLine("time lapsed - {0}", ts.Ticks);
                }

            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read: {0}");
                Console.WriteLine(e.Message);
            }



        }
    }
}
