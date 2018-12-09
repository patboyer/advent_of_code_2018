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

            public override bool Equals(object obj)
            {
                var other = obj as Coord;
                return ( (x == other.x) && (y == other.y) );
            }

            public override int GetHashCode()
            {
                return this.ToString().GetHashCode();
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

        private class Map
        {
            private List<Coord> coords;
            private int TopBorder;
            private int BottomBorder;
            private int LeftBorder;
            private int RightBorder;
            private List<Coord> visited;


            public Map(List<Coord> p_coords)
            {
                coords = p_coords;

                TopBorder    = coords.Select(c => c.y).Min() - 1;
                BottomBorder = coords.Select(c => c.y).Max() + 1;
                LeftBorder   = coords.Select(c => c.x).Min() - 1;
                RightBorder  = coords.Select(c => c.x).Max() + 1;
            }

            public int GetArea(Coord c)
            {
                visited = new List<Coord>();
                return NumOwned(c, c);
            }

            public int NumOwned(Coord c, Coord target) 
            {
                if (visited.Contains(target))
                  return 0;

                if 
                (
                    (target.x == LeftBorder) ||
                    (target.x == RightBorder) ||
                    (target.y == TopBorder) ||
                    (target.y == BottomBorder)
                )
                {
                    return -1000000;
                }

                visited.Add(target);
                int a = c.ManhattanDistance(target);

                foreach (Coord c2 in coords)
                {
                    if (c == c2)
                      continue;

                    if (a >= c2.ManhattanDistance(target))
                      return 0;
                }

                return 1 + (
                    NumOwned(c, new Coord(target.x+1, target.y)) +
                    NumOwned(c, new Coord(target.x-1, target.y)) +
                    NumOwned(c, new Coord(target.x,   target.y+1)) +
                    NumOwned(c, new Coord(target.x,   target.y-1))
                );
            }

            public int GetCommonArea() 
            {
                double startx = coords.Select(c => c.x).Average();
                double starty = coords.Select(c => c.y).Average();
                Coord start   = new Coord((int)Math.Floor(startx), (int)Math.Floor(starty));
                visited       = new List<Coord>();
                return NumCommon(10000, start);
            }

            int NumCommon(int n, Coord target)
            {
                if (visited.Contains(target))
                  return 0;
                
                visited.Add(target);
                int sum = coords.Select(c => c.ManhattanDistance(target)).Sum();

                if (sum < n)
                {
                    return 1 + (
                        NumCommon(n, new Coord(target.x + 1, target.y)) +
                        NumCommon(n, new Coord(target.x - 1, target.y)) +
                        NumCommon(n, new Coord(target.x,     target.y+1)) +
                        NumCommon(n, new Coord(target.x,     target.y-1)) 
                    );
                }
                else 
                {
                    return 0;
                }
            }
        }

        public void SolveA()
        {
            List<Coord> coords = File.ReadLines("06_input.txt")
                             .Select(s => s.Split(", "))
                             .Select(arr => new Coord(int.Parse(arr[0]), int.Parse(arr[1])))
                             .ToList();

            Map map    = new Map(coords);
            int result = coords.Select(c => map.GetArea(c)).Max();

            Console.WriteLine("Day 06 A: " + result);  //= 4215
        }

        public void SolveB()
        {
            List<Coord> coords = File.ReadLines("06_input.txt")
                             .Select(s => s.Split(", "))
                             .Select(arr => new Coord(int.Parse(arr[0]), int.Parse(arr[1])))
                             .ToList();

            Map map    = new Map(coords);
            int result = map.GetCommonArea();
            Console.WriteLine("Day 06 B: " + result);  //= 40376
        }
    }
}
