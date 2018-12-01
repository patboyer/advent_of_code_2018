using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018
{
    class Day01
    {
        public void SolveA()
        {
            var result = File.ReadLines("01_input.txt")
                             .Select(s => int.Parse(s))
                             .Sum();

            Console.WriteLine("Day 01 A:" + result);  //= 516
        }

        public void SolveB()
        {
            var changes = File.ReadLines("01_input.txt")
                              .Select(s => int.Parse(s))
                              .ToList();

            int frequency = 0;
            int idx = -1;
            HashSet<int> frequencies = new HashSet<int>();

            while (! frequencies.Contains(frequency))
            {
                frequencies.Add(frequency);
                idx = (idx < changes.Count - 1) ? idx + 1 : 0;
                frequency += changes[idx];
            }

            Console.WriteLine("Day 01 B: " + frequency);  //= 71892
        }
    }
}