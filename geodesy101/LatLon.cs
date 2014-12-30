using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace geodesy101
{
    class LatLon
    {
        double lat;
        double lon;
        double height;
        double radius;
        public LatLon() { }
        public LatLon(double lat, double lon, double height = 0d, double radius = 6371d)
        {
            radius = Math.Min(Math.Max(radius, 6353), 6384);
            this.lat = lat;
            this.lon = lon;
            this.height = height;
            this.radius = radius;
        }

        /// <summary>
        /// Returns the distance from 'this' point to destination point 
        /// (using haversine formula).
        /// </summary>
        /// <param name="LatLon point"></param>
        /// <returns> double </returns>
        public double distanceTo(LatLon point)
        {
            double R = this.radius;
            double φ1 = this.lat.toRadians(), λ1 = this.lon.toRadians();
            double φ2 = point.lat.toRadians(), λ2 = point.lon.toRadians();
            double Δφ = φ2 - φ1;
            double Δλ = λ2 - λ1;
            double a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                    Math.Cos(φ1) * Math.Cos(φ2) *
                    Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;
            return d;
        }
        /// <summary>
        /// Returns the (initial) bearing from 'this' point to destination point.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public double bearingTo(LatLon point)
        {
            double φ1 = this.lat.toRadians(), φ2 = point.lat.toRadians();
            double Δλ = (point.lon - this.lon).toRadians();
            // see http://mathforum.org/library/drmath/view/55417.html
            double y = Math.Sin(Δλ) * Math.Cos(φ2);
            double x = Math.Cos(φ1) * Math.Sin(φ2) -
                    Math.Sin(φ1) * Math.Cos(φ2) * Math.Cos(Δλ);
            double θ = Math.Atan2(y, x);
            return (θ.toDegrees() + 360) % 360;
        }

        /// <summary>
        /// Returns final bearing arriving at destination destination point from 'this' point;
        /// the final bearing will differ from the initial bearing 
        /// by varying degrees according to distance and latitude.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public double finalBearingTo(LatLon point)
        {
            // get initial bearing from destination point to this point & reverse it by adding 180°
            return (point.bearingTo(this) + 180) % 360;
        }


        /// <summary>
        /// Returns the midpoint between 'this' point and the supplied point.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public LatLon midpointTo(LatLon point)
        {
            // see http://mathforum.org/library/drmath/view/51822.html for derivation

            double φ1 = this.lat.toRadians(), λ1 = this.lon.toRadians();
            double φ2 = point.lat.toRadians();
            double Δλ = (point.lon - this.lon).toRadians();

            double Bx = Math.Cos(φ2) * Math.Cos(Δλ);
            double By = Math.Cos(φ2) * Math.Sin(Δλ);

            double φ3 = Math.Atan2(Math.Sin(φ1) + Math.Sin(φ2),
             Math.Sqrt((Math.Cos(φ1) + Bx) * (Math.Cos(φ1) + Bx) + By * By));
            double λ3 = λ1 + Math.Atan2(By, Math.Cos(φ1) + Bx);
            λ3 = (λ3 + 3 * Math.PI) % (2 * Math.PI) - Math.PI; // normalise to -180..+180º

            return new LatLon(φ3.toDegrees(), λ3.toDegrees());
        }

        /// <summary>
        /// Returns the destination point from 'this' point having travelled the given 
        /// distance on the given initial bearing (bearing normally varies around path followed).
        /// </summary>
        /// <param name="brng"></param>
        /// <param name="dist"></param>
        /// <returns></returns>
        public LatLon destinationPoint(double brng, double dist)
        {
            // see http://williams.best.vwh.net/avform.htm#LL

             
            double θ = brng.toRadians();
            double δ = dist / this.radius; // angular distance in radians

            double φ1 = this.lat.toRadians();
            double λ1 = this.lon.toRadians();

            double φ2 = Math.Asin(Math.Sin(φ1) * Math.Cos(δ) +
                                Math.Cos(φ1) * Math.Sin(δ) * Math.Cos(θ));
            double λ2 = λ1 + Math.Atan2(Math.Sin(θ) * Math.Sin(δ) * Math.Cos(φ1),
                                     Math.Cos(δ) - Math.Sin(φ1) * Math.Sin(φ2));
            λ2 = (λ2 + 3 * Math.PI) % (2 * Math.PI) - Math.PI; // normalise to -180..+180º

            return new LatLon(φ2.toDegrees(), λ2.toDegrees());
        }

        /// <summary>
        /// Returns the point of intersection of two paths defined by point and bearing
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="brng1"></param>
        /// <param name="p2"></param>
        /// <param name="brng2"></param>
        /// <returns></returns>
        public LatLon intersection(LatLon p1, double brng1, LatLon p2, double brng2)
        {
            // see http://williams.best.vwh.net/avform.htm#Intersection

            double φ1 = p1.lat.toRadians(), λ1 = p1.lon.toRadians();
            double φ2 = p2.lat.toRadians(), λ2 = p2.lon.toRadians();
            double θ13 = brng1.toRadians(), θ23 = brng2.toRadians();
            double Δφ = φ2 - φ1, Δλ = λ2 - λ1;

            double δ12 = 2 * Math.Asin(Math.Sqrt(Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                Math.Cos(φ1) * Math.Cos(φ2) * Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2)));
            if (δ12 == 0) return null;

            // initial/final bearings between points
            double θ1 = Math.Acos((Math.Sin(φ2) - Math.Sin(φ1) * Math.Cos(δ12)) /
                                (Math.Sin(δ12) * Math.Cos(φ1)));
            if (Double.IsNaN(θ1)) θ1 = 0; // protect against rounding
            double θ2 = Math.Acos((Math.Sin(φ1) - Math.Sin(φ2) * Math.Cos(δ12)) /
                                (Math.Sin(δ12) * Math.Cos(φ2)));

            double θ12, θ21;
            if (Math.Sin(λ2 - λ1) > 0)
            {
                θ12 = θ1;
                θ21 = 2 * Math.PI - θ2;
            }
            else
            {
                θ12 = 2 * Math.PI - θ1;
                θ21 = θ2;
            }

            double α1 = (θ13 - θ12 + Math.PI) % (2 * Math.PI) - Math.PI; // angle 2-1-3
            double α2 = (θ21 - θ23 + Math.PI) % (2 * Math.PI) - Math.PI; // angle 1-2-3

            if (Math.Sin(α1) == 0 && Math.Sin(α2) == 0) return null; // infinite intersections
            if (Math.Sin(α1) * Math.Sin(α2) < 0) return null;      // ambiguous intersection

            //α1 = Math.abs(α1);
            //α2 = Math.abs(α2);
            // ... Ed Williams takes abs of α1/α2, but seems to break calculation?

            double α3 = Math.Acos(-Math.Cos(α1) * Math.Cos(α2) +
                                 Math.Sin(α1) * Math.Sin(α2) * Math.Cos(δ12));
            double δ13 = Math.Atan2(Math.Sin(δ12) * Math.Sin(α1) * Math.Sin(α2),
                                  Math.Cos(α2) + Math.Cos(α1) * Math.Cos(α3));
            double φ3 = Math.Asin(Math.Sin(φ1) * Math.Cos(δ13) +
                                Math.Cos(φ1) * Math.Sin(δ13) * Math.Cos(θ13));
            double Δλ13 = Math.Atan2(Math.Sin(θ13) * Math.Sin(δ13) * Math.Cos(φ1),
                                   Math.Cos(δ13) - Math.Sin(φ1) * Math.Sin(φ3));
            var λ3 = λ1 + Δλ13;
            λ3 = (λ3 + 3 * Math.PI) % (2 * Math.PI) - Math.PI; // normalise to -180..+180º

            return new LatLon(φ3.toDegrees(), λ3.toDegrees());
        }




    }
}
