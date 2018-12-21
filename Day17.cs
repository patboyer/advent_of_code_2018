using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018
{
    class Day17
    {
        const int TILE_CLAY     = -3;
        const int TILE_DRY_SAND = -2;
        const int TILE_WATER    = -1;
        const char DIR_LEFT     = '<';
        const char DIR_RIGHT    = '>';
        const char DIR_DOWN     = 'v';

        int[,] ground;

        bool addWater(int x, int y, int ymax, int waterDrop)
        {
            Console.WriteLine($"{x},{y}");
            if (y > ymax)
            {
                return false;
            }
            else if (ground[x,y] == TILE_DRY_SAND)
            {
                ground[x,y] = waterDrop;
                return true;
            }
            else if ( (ground[x,y] == TILE_CLAY) || (ground[x,y] == TILE_WATER) )
            {
                return false;
            }
            else // TILE_WET_SAND
            {
                if (ground[x,y] > waterDrop)
                    return false;
                else 
                    return (
                        addWater(x, y+1, ymax, waterDrop) || 
                        addWater(x+1, y, ymax, waterDrop) || 
                        addWater(x-1, y, ymax, waterDrop)
                    );
            }
        }

        public void SolveA() 
        {
            var clay = new List<(int x, int y)>();
            foreach (string line in File.ReadLines("17_input.txt"))
            {
                string[] delimiters = { " ", ",", ".", "="};
                string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                int op1 = int.Parse(parts[1]);
                int op3 = int.Parse(parts[3]);
                int op4 = int.Parse(parts[4]);
                
                if (parts[0] == "x")
                {
                    for (int y=op3; y<=op4; y++)
                        clay.Add((op1, y));
                }
                else 
                {
                    for (int x=op3; x<=op4; x++)
                        clay.Add((x, op1));
                }
            }

            int xmin = clay.Select(c => c.x).Min();
            int xmax = clay.Select(c => c.x).Max();
            int ymin = clay.Select(c => c.y).Min();
            int ymax = clay.Select(c => c.y).Max();
            int xshift = 1;
            ground = new int[xmax+5, ymax+1];

            for (int x=0; x<=xmax; x++)
                for (int y=0; y<=ymax; y++)
                    ground[x+xshift,y] = TILE_DRY_SAND;

            clay.ForEach(c => ground[c.x-xshift, c.y] = TILE_CLAY);
            
            int waterDrop = 0;
            bool waterAdded = true;

            while (waterAdded)
            {
                waterDrop++;
                waterAdded = addWater(500+xshift, 0, ymax, waterDrop);
            }

            Console.WriteLine("Day17 A: " + (waterDrop - 1).ToString());  //= 
        }


        public void SolveB()
        {
            int result = 0;
            Console.WriteLine("Day17 B: " + result);  //= 
        }
    }
}