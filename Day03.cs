using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018
{
    class Day03
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
        }

        private class Fabric
        {
            public int   id;
            public Coord TopLeft;
            public Coord BottomRight;

            public Fabric(string fabric_config)
            {
                var parts = fabric_config.Split(" ");

                id = int.Parse(parts[0].Split("#")[1]);

                string[] s = parts[2].Split(",");
                TopLeft = new Coord(int.Parse(s[0]), int.Parse(s[1].Remove(s[1].Length-1, 1))); 

                s = parts[3].Split("x");
                BottomRight = new Coord(
                    int.Parse(s[0]) + this.TopLeft.x - 1, 
                    int.Parse(s[1]) + this.TopLeft.y - 1
                ); 
            }

            public bool NoOverlap(Fabric other)
            {                 
                return (
                    (this.BottomRight.x < other.TopLeft.x) || 
                    (this.TopLeft.x     > other.BottomRight.x) ||
                    (this.BottomRight.y < other.TopLeft.y) ||
                    (this.TopLeft.y     > other.BottomRight.y) 
                );
            }

            public bool Overlap(Fabric other)
            {
                return (! this.NoOverlap(other));
            }

            public override bool Equals(Object obj)
            {
                var other = obj as Fabric;

                if (obj == null)
                    return false;
                else
                    return (this.id == other.id);
            }

            public override int GetHashCode()
            {
                return this.id.GetHashCode();
            }
        }

        public void SolveA() 
        {
            var lines  = File.ReadLines("03_input.txt");   
            int result = 0;

            IDictionary<string, int> dict = new Dictionary<string, int>();

            foreach (string line in lines) 
            {
                Fabric f = new Fabric(line);

                for (int x=f.TopLeft.x; x<=f.BottomRight.x; x++)
                {
                    for (int y=f.TopLeft.y; y<=f.BottomRight.y; y++)
                    {
                        string key = x.ToString() + "," + y.ToString();

                        dict[key] = (dict.ContainsKey(key))
                          ? dict[key] + 1
                          : 1;
                    }
                }
            }

            foreach (string key in dict.Keys)
            {
                if (dict[key] > 1)
                  result++;
            }

            Console.WriteLine("Day03 A: " + result);  //= 106501
        }


        public void SolveB()
        {
            var lines = File.ReadLines("03_input.txt");
            List<Fabric> fabrics = new List<Fabric>();
            Fabric result = new Fabric("#0 @ -1,-1: -1x-1");

            foreach (string line in lines) 
            {
                Fabric f = new Fabric(line);
                fabrics.Add(f);
            }

            foreach (Fabric f1 in fabrics)
            {
                bool overlap = false;

                foreach (Fabric f2 in fabrics)
                {
                    if (f1.Equals(f2))
                        continue;
                    else if (f1.Overlap(f2))
                    {
                        overlap = true;
                        break;
                    }
                }

                if (! overlap) 
                {
                    result = f1;
                    break;
                }
            }

            Console.WriteLine("Day03 B: " + result.id);  //= 632
        }
    }
}