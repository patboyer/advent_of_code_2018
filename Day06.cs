using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace advent_of_code_2018
{
    class Day06
    {
        private class Coord 
        {
            public int x;
            public int y;

            public Coord(int p_x, int p_y)
            {
                x = p_x;
                y = p_y;
            }

            public override string ToString()
            {
                return "(" + x.ToString() + ", " + y.ToString() + ")";
            }

            public int ManhattanDistance(Coord other)
            {
                return Math.Abs(x - other.x) + Math.Abs(y - other.y);
            }
        }

        public void SolveA()
        {
            List<Coord> coords = File.ReadLines("06_input.txt")
                             .Select(s => s.Split(", "))
                             .Select(arr => new Coord(int.Parse(arr[0]), int.Parse(arr[1])))
                             .ToList();

            string result = "";
            Console.WriteLine("Day 06 A: " + result);  //= 
        }

        public void SolveB()
        {
            string result = "";
            Console.WriteLine("Day 06 B: " + result);  //=
        }
    }
}

