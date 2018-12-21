using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace advent_of_code_2018
{
    class Day12
    {
        public int Solve(int n)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();

            foreach (string line in File.ReadLines("12_input.txt"))
            {
                string[] parts = line.Split(" => ");
                dict.Add(parts[0].Trim(), parts[1].Trim());
            }

            StringBuilder plants = new StringBuilder("##.##..#.#....#.##...###.##.#.#..###.#....##.###.#..###...#.##.#...#.#####.###.##..#######.####..#");
            StringBuilder sb = new StringBuilder("");
            int startLen = plants.Length;

            for (int i=1; i<=n; i++)
            {   
                plants.Insert(0, "....");
                plants.Append("....");
                sb = new StringBuilder("");

                for (int idx=2; idx<=plants.Length-3; idx++)
                {
                    string key = plants.ToString(idx-2, 5);
                    string s = (dict.ContainsKey(key)) ? dict[key] : ".";
                    sb.Append(s);
                }

                plants = new StringBuilder(sb.ToString());
            }

            int result = 0;
            for (int i=0; i<plants.Length; i++)
            {
                if (plants[i] == '#') {
                    result += i - (plants.Length - startLen)/2;
                }
            }

            return result;
        }

        public void SolveA()
        {
            Console.WriteLine("Day12 A: " + Solve(20));  //= 3061
        }

        public void SolveB()
        {
            // sequence starts repeating around the 70th iteration
            // adding 81 to the sum each time
            long result = Solve(200) + ((50000000000 - 200) * 81);
            Console.WriteLine("Day12 B: " + result);  //= 4049999998575
        }
    }
}