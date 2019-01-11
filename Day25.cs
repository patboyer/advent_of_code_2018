using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018
{
    class Day25
    {
        class Star
        {
            public int c0, c1, c2, c3;

            public Star(int p0, int p1, int p2, int p3)
            {
                c0 = p0;
                c1 = p1;
                c2 = p2;
                c3 = p3;          
            }

            public int ManhattanDistance(Star other) 
            {
                return Math.Abs(c0 - other.c0)
                     + Math.Abs(c1 - other.c1)
                     + Math.Abs(c2 - other.c2)
                     + Math.Abs(c3 - other.c3);
            }

            public override string ToString()
            {
                return $"({c0}, {c1}, {c2}, {c3})";
            }
        }

        public void SolveA()
        {
            List<Star> stars = new List<Star>();
            foreach (string line in File.ReadLines("25_input.txt"))
            {
                string[] parts = line.Split(",");

                stars.Add(new Star(
                    int.Parse(parts[0]), 
                    int.Parse(parts[1]), 
                    int.Parse(parts[2]), 
                    int.Parse(parts[3])
                ));
            }

            int numGroups = 0;

            while (stars.Count() > 0)
            {
                List<Star> constellation = new List<Star>() { stars.ElementAt(0) };
                stars.RemoveAt(0);
                numGroups++;

                int idx = 0;
                while (idx < stars.Count())
                {
                    Star s1 = stars[idx];
                    bool isConstellationMember = (constellation.Where(s2 => s1.ManhattanDistance(s2) <= 3).Count() > 0);

                    if (isConstellationMember)
                    {
                        stars.RemoveAt(idx);
                        constellation.Add(s1);
                        idx = 0;
                    }
                    else
                    {
                        idx++;
                    }
                }
            }

            Console.WriteLine($"Day 25 A: {numGroups}");  //= 310
        }
    }
}