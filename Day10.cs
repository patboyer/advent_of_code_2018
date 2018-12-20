using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018
{
    class Day10
    {
        int _seconds;

        class Particle
        {
            public int x;
            public int y;
            public int vx;
            public int vy;

            public Particle(int p_x, int p_y, int p_vx, int p_vy)
            {
                x  = p_x;
                y  = p_y;
                vx = p_vx;
                vy = p_vy;
            }
        }

        void displayParticles(List<Particle> plist) 
        {
            int minx = plist.Select(p => p.x).Min();
            int maxx = plist.Select(p => p.x).Max();
            int miny = plist.Select(p => p.y).Min();
            int maxy = plist.Select(p => p.y).Max();
            int shiftx = 1 - minx;
            int shifty = 1 - miny;

            // through trial and error figured out how small the area
            // is when it converges
            if ( (maxx-minx)>65 || (maxy-miny)>65 )
              return;

            char[,] grid = new char[maxx+shiftx+1, maxy+shifty+1];
            for (int x=0; x<=maxx+shiftx; x++)
            {
                for (int y=0; y<=maxy+shifty; y++)
                {
                    grid[x,y] = '.';
                }
            }

            foreach (Particle p in plist)
            {
                int x = p.x + shiftx;
                int y = p.y + shifty;
                grid[x,y] = '#';
            }

            for (int y=0; y<=maxy+shifty; y++)
            {
                for (int x=0; x<=maxx+shiftx; x++)
                {
                    Console.Write(grid[x,y]);
                }

                Console.WriteLine("");
            }

            // warning: ugly ugly ugly :)
            throw new Exception("seconds: " + _seconds);
        }

        public void Solve()
        {
            string[] delimiters = { "position", "velocity", "=", " ", "<", ">", "," };
            List<Particle> particles = new List<Particle>();
            _seconds = 0;

            foreach (string line in File.ReadLines("10_input.txt"))
            {
                int[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(s => int.Parse(s))
                                     .ToArray();
                
                particles.Add(new Particle(parts[0], parts[1], parts[2], parts[3]));
            }

            do {
                displayParticles(particles);
                particles = particles.Select(p => new Particle(p.x+p.vx, p.y+p.vy, p.vx, p.vy)).ToList();
                _seconds++;
            } while (true);
            
            //= PANLPAPR in 10304 seconds
        }
    }
}