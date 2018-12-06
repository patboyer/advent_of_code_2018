using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace advent_of_code_2018
{
    class Day05
    {
        public string React(string input)
        {
            int numDeletions = 0;
            
            do
            {
                numDeletions = 0;
                StringBuilder sb = new StringBuilder(input);

                for (int i=1; i<sb.Length; i++)
                {
                    int a = (int)sb[i];
                    int b = (int)sb[i-1];

                    if ( (a==(b-32)) || (a==(b+32)) )
                    {
                        numDeletions += 1;
                        sb.Remove(i-1, 2);
                    }
                }

                input = sb.ToString();
            } while (numDeletions > 0);

            return input;
        }

        public void SolveA()
        {
            string text = File.ReadAllText("05_input.txt").Trim();
            text = React(text);
            Console.WriteLine("Day 05 A: " + text.Length);  //= 10972
        }

        public void SolveB()
        {
            string text = File.ReadAllText("05_input.txt").Trim();
            int n = text.Length;
            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (var c in alphabet)
            {
                char uc = (char)((int)c - 32);
                string s = text.Replace(c.ToString(), string.Empty).Replace(uc.ToString(), string.Empty);
                s = React(s);
                n = Math.Min(n, s.Length);
            }

            Console.WriteLine("Day 05 B: " + n);  //=  5278
        }
    }
}

