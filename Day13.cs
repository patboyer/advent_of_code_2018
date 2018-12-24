using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018
{
    class Day13
    {
        const char CART_RIGHT = '>';
        const char CART_LEFT  = '<';
        const char CART_DOWN  = 'v';
        const char CART_UP    = '^';

        const char TRACK_VERT      = '|';
        const char TRACK_HORIZ     = '-';
        const char TRACK_CORNER1   = '/';
        const char TRACK_CORNER2   = '\\';
        const char TRACK_INTERSECT = '+';

        const int DIR_UP    = 0;
        const int DIR_RIGHT = 1;
        const int DIR_DOWN  = 2;
        const int DIR_LEFT  = 3;

        class Cart : IComparable<Cart>
        {
            private static int _id = 1;
            private int direction;
            private int turn;
            public int id;
            public int x;
            public int y;

            public Cart(int p_x, int p_y, char p_direction)
            {
                id = _id++;
                x  = p_x;
                y  = p_y;

                switch (p_direction)
                {
                    case CART_RIGHT: direction = DIR_RIGHT; break;
                    case CART_DOWN:  direction = DIR_DOWN;  break;
                    case CART_LEFT:  direction = DIR_LEFT;  break; 
                    case CART_UP:    direction = DIR_UP;    break;
                }

                turn = 1;
            }

            public void Move()
            {
                if (direction == DIR_UP)
                    y -= 1;
                else if (direction == DIR_DOWN)
                    y += 1;
                else if (direction == DIR_LEFT)
                    x -= 1;
                else 
                    x += 1;

                Console.WriteLine($"cart {id} moves to ({x},{y})");
            }

            public void Update(char c)
            {
                if ( (direction == DIR_UP) && (c == TRACK_CORNER1) )  // '/'
                    direction = DIR_RIGHT;
                else if( (direction == DIR_UP) && (c == TRACK_CORNER2) )  // '\'
                    direction = DIR_LEFT;
                else if ( (direction == DIR_DOWN) && (c == TRACK_CORNER1) )  // '/'
                    direction = DIR_LEFT;
                else if ( (direction == DIR_DOWN) && (c == TRACK_CORNER2) )  // '\'
                    direction = DIR_RIGHT;
                else if ( (direction == DIR_LEFT) && (c == TRACK_CORNER1) )  // '/'
                    direction = DIR_DOWN;
                else if ( (direction == DIR_LEFT) && (c == TRACK_CORNER2) )  // '\'
                    direction = DIR_UP;
                else if ( (direction == DIR_RIGHT) && (c == TRACK_CORNER1) )  // '/'
                    direction = DIR_UP;
                else if ( (direction == DIR_RIGHT) && (c == TRACK_CORNER2) )  // '\'
                    direction = DIR_DOWN;
                else if (c == TRACK_INTERSECT)
                {
                    switch (turn)
                    {
                        case 1: 
                            direction = (direction == DIR_UP) ? DIR_LEFT : direction - 1; 
                            break;
                        case 3:
                            direction = (direction == DIR_LEFT) ? DIR_UP : direction + 1; 
                            break;
                    }

                    turn = (turn == 3) ? 1 : turn + 1;
                }
            }

            public int CompareTo(Cart other)
            {
                return (y == other.y)
                    ? x.CompareTo(other.x)
                    : y.CompareTo(other.y);
            }

            public override bool Equals(object obj)
            {
                var other = obj as Cart;
                return ( (x == other.x) && (y == other.y) );
            }

            public override int GetHashCode()
            {
                return $"{x},{y}".GetHashCode();
            }

            public bool CrashWith(Cart other)
            {
                return ( (id != other.id) && (x == other.x) && (y == other.y) );
            }
        }

        public void Solve() 
        {
            string[] lines = File.ReadLines("13_input.txt").ToArray();   
            int yscale = lines.Length;
            int xscale = lines.Select(l => l.Length).Max();
            char[,] grid = new char[xscale, yscale];
            List<Cart> carts = new List<Cart>();

            for (int y=0; y<lines.Length; y++)
            {
                string line = lines[y];
                for (int x=0; x<line.Length; x++)
                {
                    char c = line[x];
                    grid[x,y] = c;

                    if 
                    (
                        (c == CART_UP)   ||
                        (c == CART_DOWN) ||
                        (c == CART_LEFT) ||
                        (c == CART_RIGHT) 
                    )
                    {
                        carts.Add(new Cart(x, y, c));
                    }
                }
            }

            foreach (Cart cart in carts) 
            {
                int x = cart.x;
                int y = cart.y;

                // being lazy and only accounting for the carts in our problem;
                // not all possible combinations
                if ( (grid[x-1,y] == TRACK_HORIZ) && (grid[x+1,y] == TRACK_HORIZ) )
                    grid[x,y] = TRACK_HORIZ;
                else if ( (grid[x-1,y] == TRACK_INTERSECT) && (grid[x+1,y] == TRACK_INTERSECT) )
                    grid[x,y] = TRACK_HORIZ;
                else if ( (grid[x,y-1] == TRACK_INTERSECT) && (grid[x,y+1] == TRACK_INTERSECT) )
                    grid[x,y] = TRACK_VERT;
                else if ( (grid[x,y-1] == TRACK_VERT) && (grid[x,y+1] == TRACK_INTERSECT) )
                    grid[x,y] = TRACK_VERT;
                else if ( (grid[x,y-1] == TRACK_INTERSECT) && (grid[x,y+1] == TRACK_VERT) )
                    grid[x,y] = TRACK_VERT;
                else if ( (grid[x,y-1] == TRACK_VERT) && (grid[x,y+1] == TRACK_VERT) )
                    grid[x,y] = TRACK_VERT;
                else if ( (grid[x,y-1] == TRACK_VERT) && (grid[x,y+1] == TRACK_VERT) )
                    grid[x,y] = TRACK_VERT;
                else if ( (grid[x,y-1] == TRACK_CORNER1) && (grid[x,y+1] == TRACK_INTERSECT) )
                    grid[x,y] = TRACK_VERT;
                else if ( (grid[x,y-1] == TRACK_CORNER2) && (grid[x,y+1] == TRACK_INTERSECT) )
                    grid[x,y] = TRACK_VERT;
            }

            while (carts.Count() > 1)
            {
                List<Cart> crashed = new List<Cart>();
                foreach (Cart c in carts.OrderBy(c => c))
                {
                    c.Move();
                    c.Update(grid[c.x, c.y]);

                    foreach (Cart c2 in carts.Where(c2 => (c.id != c2.id)))
                    {
                        if (c.CrashWith(c2))
                        {
                            Console.WriteLine($"Crash at ({c.x},{c.y})"); 
                            crashed.Add(c);
                            crashed.Add(c2);
                            break;
                        }
                    }
                }

                foreach (Cart c in crashed)
                {
                    carts.Remove(c);
                }
            }
        }
    }
}

// A: 63,103
// B: 16,134