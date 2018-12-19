using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2018
{
    class Day11
    {
        public void SolveA()
        {
            int size = 300;
            int[,] grid = new int[size+1,size+1];
            int serialNum = 9221;
            int z = 2;
            int max = 0;
            string result = "1,1";
            int total = 0;

            for (int x=1; x<=size; x++)
            {
                for (int y=1; y<=size; y++)
                {
                    total = 0;
                    int rackID = x + 10;
                    int powerLevel = ((rackID * y) + serialNum) * rackID;
                    string s = powerLevel.ToString();
                    powerLevel = (s.Length >= 3) 
                        ? int.Parse(s[s.Length-3].ToString())
                        : 0;
                    grid[x,y] = powerLevel - 5;

                    for (int i=x-z; (i>0 && i<=x); i++)
                    {
                        for (int j=y-z; (j>0 && j<=y); j++)
                        {
                            total += grid[i,j];
                        }                        
                    }

                    if (total > max) 
                    {
                        max = total;
                        result = (x-z).ToString() + "," + (y-z).ToString();
                    }
                }
            }

            Console.WriteLine("Day 11 A: " + result);  //= "20,77"
        }

        public void SolveB()
        {
            int size = 300;
            int[,] grid = new int[size+1,size+1];
            int serialNum = 9221;
            int max = 0;
            string result = "1,1,2";

            for (int x=1; x<=size; x++)
            {
                for (int y=1; y<=size; y++)
                {
                    int rackID = x + 10;
                    int powerLevel = ((rackID * y) + serialNum) * rackID;
                    string s = powerLevel.ToString();
                    powerLevel = (s.Length >= 3) 
                        ? int.Parse(s[s.Length-3].ToString())
                        : 0;
                    grid[x,y] = powerLevel - 5;

                    // try different size squares
                    int z = (x<y) ? x : y;

                    for (int c=1; c<=z; c++)
                    {
                        int total = 0;

                        for (int i=x-c; (i>0 && i<=x); i++)
                        {
                            for (int j=y-c; (j>0 && j<=y); j++)
                            {
                                total += grid[i,j];
                            }                        
                        }

                        if (total > max) 
                        {
                            max = total;
                            result = (x-c).ToString() + "," + (y-c).ToString() + "," + (c+1).ToString();
                        }
                    }
                }
            }

            Console.WriteLine("Day 11 B: " + result);  //= "143,57,10"
        }
    }
}