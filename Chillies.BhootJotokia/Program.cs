using System;

namespace Chillies.BhootJotokia
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }


    readonly struct Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y) => (this.X, this.Y) = (x, y);

    }
}
