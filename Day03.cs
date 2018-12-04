using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018
{
    class Day03
    {
        private IDictionary<string, int> ParseInput(IEnumerable<string> lines) 
        {
            IDictionary<string, int> dict = new Dictionary<string, int>();

            foreach (string line in lines) 
            {
                var parts = line.Split(" ");
                int id = int.Parse(parts[0].Split("#")[1]);

                string[] s = parts[2].Split(",");
                (int x, int y) coord = (int.Parse(s[0]), int.Parse(s[1].Remove(s[1].Length-1, 1))); 

                s = parts[3].Split("x");
                (int x, int y) rect = (int.Parse(s[0]), int.Parse(s[1])); 

                for (int x=coord.x; x<coord.x+rect.x; x++)
                {
                    for (int y=coord.y; y<coord.y+rect.y; y++)
                    {
                        string key = x.ToString() + "," + y.ToString();

                        dict[key] = (dict.ContainsKey(key))
                          ? dict[key] + 1
                          : 1;
                    }
                }
            }

            return dict;
        }

        public void SolveA() 
        {
            var lines  = File.ReadLines("03_input.txt");   
            int result = 0;
            var dict   = ParseInput(lines);

            foreach (string key in dict.Keys)
            {
                if (dict[key] > 1)
                  result++;
                else if (dict[key] == 1)
                  Console.WriteLine(key);
            }

            Console.WriteLine("Day03 A: " + result);  //= 106501
        }

        public void SolveB()
        {
            var lines = File.ReadLines("03_input.txt");
            var dict  = ParseInput(lines);

            foreach (string key in dict.Keys.OrderBy(s => s))
            {
                if (dict[key] == 1)
                  Console.WriteLine(key);
            }

            Console.WriteLine("Day03 B: ");
        }
    }
}