using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2018
{
    class Day14
    {
        public void SolveA()
        {
            StringBuilder recipies = new StringBuilder("37");
            int elf1   = 0;
            int elf2   = 1;
            int n      = 824501;
            int sequenceLength = 10;

            while (recipies.Length < (n + sequenceLength))
            {
                int a = int.Parse(recipies[elf1].ToString());
                int b = int.Parse(recipies[elf2].ToString());
                recipies.Append((a + b).ToString());
                elf1 = (elf1 + a + 1) % recipies.Length;
                elf2 = (elf2 + b + 1) % recipies.Length;
            }

            if ( (n + sequenceLength) < recipies.Length)
                recipies.Remove(recipies.Length - 1, 1);
            
            string result = recipies.ToString()
                                    .Substring(recipies.Length-10, 10);

            Console.WriteLine($"Day 14 A: {string.Join("", result)}");  //= 1031816654
        }

        public void SolveB()
        {
            StringBuilder recipies = new StringBuilder("37");
            int elf1      = 0;
            int elf2      = 1;
            string target = "824501";
            bool found    = false;
            StringBuilder cmp = new StringBuilder("37");
            int result = 0;

            while (! found)
            {
                int a = int.Parse(recipies[elf1].ToString());
                int b = int.Parse(recipies[elf2].ToString());

                List<string> newRecipies = (a + b).ToString()
                                                  .ToCharArray()
                                                  .Select(c => c.ToString())
                                                  .ToList();
                foreach (string s in newRecipies)
                {
                    recipies.Append(s);
                    cmp.Append(s);

                    if (cmp.Length > target.Length)
                        cmp.Remove(0, 1);

                    if (cmp.ToString() == target)
                    {
                        result = recipies.Length - target.Length;
                        found = true;
                    }
                }

                elf1 = (elf1 + a + 1) % recipies.Length;
                elf2 = (elf2 + b + 1) % recipies.Length;
            }

            Console.WriteLine($"Day 14 B: {result}");  //= 20179839
        }
    }
}