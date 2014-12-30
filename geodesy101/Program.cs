using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geodesy101
{
    class Program
    {
        static void Main(string[] args)
        {
            double d = 180.0;
            var o = d.toRadians();
            const double π = Math.PI;
            double r = 10d;
            double area = π * r.Square();


            LatLon PlaugGate = new LatLon(12.660450, 101.314343);
            LatLon Techno = new LatLon(12.666559, 101.316387);
            LatLon KonNong = new LatLon(12.686432, 101.341347);
            LatLon BanLaeng = new LatLon(12.699527, 101.333742);
            LatLon Housing = new LatLon(12.685096, 101.300576);

            LatLon point1 = PlaugGate;
            LatLon point2 = BanLaeng;

            var x = point1.distanceTo(point2);
            var y = point1.bearingTo(point2);

            // To Konnong
            Console.WriteLine("PlaugGate -> KonNong\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                PlaugGate.distanceTo(KonNong),
                PlaugGate.bearingTo(KonNong));
            Console.WriteLine("Techno -> KonNong\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                Techno.distanceTo(KonNong),
                Techno.bearingTo(KonNong));
            Console.WriteLine("BanLaeng -> KonNong\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                BanLaeng.distanceTo(KonNong),
                BanLaeng.bearingTo(KonNong));
            Console.WriteLine("Housing -> KonNong\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                Housing.distanceTo(KonNong),
                Housing.bearingTo(KonNong));
            Console.WriteLine();

            // To BanLaeng
            Console.WriteLine("PlaugGate -> BanLaeng\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                PlaugGate.distanceTo(BanLaeng),
                PlaugGate.bearingTo(BanLaeng));
            Console.WriteLine("Techno -> BanLaeng\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                Techno.distanceTo(BanLaeng),
                Techno.bearingTo(BanLaeng));
            Console.WriteLine("KonNong -> BanLaeng\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                KonNong.distanceTo(BanLaeng),
                KonNong.bearingTo(BanLaeng));
            Console.WriteLine("Housing -> BanLaeng\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                Housing.distanceTo(BanLaeng),
                Housing.bearingTo(BanLaeng));
            Console.WriteLine();

            // To PlaugGate
            Console.WriteLine("BanLaeng -> PlaugGate\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                BanLaeng.distanceTo(PlaugGate),
                BanLaeng.bearingTo(PlaugGate));
            Console.WriteLine("Techno -> PlaugGate\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                Techno.distanceTo(PlaugGate),
                Techno.bearingTo(PlaugGate));
            Console.WriteLine("KonNong -> PlaugGate\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                KonNong.distanceTo(PlaugGate),
                KonNong.bearingTo(PlaugGate));
            Console.WriteLine("Housing -> PlaugGate\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                Housing.distanceTo(PlaugGate),
                Housing.bearingTo(PlaugGate));
            Console.WriteLine();

            // To Techno
            Console.WriteLine("BanLaeng -> Techno\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                BanLaeng.distanceTo(Techno),
                BanLaeng.bearingTo(Techno));
            Console.WriteLine("KonNong -> Techno\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                KonNong.distanceTo(Techno),
                KonNong.bearingTo(Techno));
            Console.WriteLine("KonNong -> Techno\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                KonNong.distanceTo(Techno),
                KonNong.bearingTo(Techno));
            Console.WriteLine("Housing -> Techno\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                Housing.distanceTo(Techno),
                Housing.bearingTo(Techno));
            Console.WriteLine();

            // To Housing
            Console.WriteLine("BanLaeng -> Housing\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                BanLaeng.distanceTo(Housing),
                BanLaeng.bearingTo(Housing));
            Console.WriteLine("Techno -> Housing\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                Techno.distanceTo(Housing),
                Techno.bearingTo(Housing));
            Console.WriteLine("KonNong -> Housing\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                KonNong.distanceTo(Housing),
                KonNong.bearingTo(Housing));
            Console.WriteLine("PlaugGate -> Housing\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                PlaugGate.distanceTo(Housing),
                PlaugGate.bearingTo(Housing));
            Console.WriteLine();

            Console.WriteLine("BanLaeng -> Housing\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                BanLaeng.distanceTo(Housing),
                BanLaeng.bearingTo(Housing));
            Console.WriteLine("Housing -> BanLaeng\tDistance: {0:F3} km\tbearing: {1:F6} deg",
                Housing.distanceTo(BanLaeng),
                Housing.bearingTo(BanLaeng));


        }
    }

    public static class DoubleExtensions
    {
        public static double Square(this Double d)
        {
            return d * d;
        }
        public static double Sqrt(this Double d)
        {
            return Math.Sqrt(d);
        }
        public static double toRadians(this Double d)
        {
            return d * Math.PI / 180;
        }
        public static double toDegrees(this Double d)
        {
            return d * 180 / Math.PI;
        }
    }

}
