using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointBase
{
    class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point() { X = 0; Y = 0; }  // Default

        public Point(double x, double y) { X = x; Y = y; }

        public void ShowData() => Console.WriteLine($"X: {X} | Y: {Y}");
    }


}