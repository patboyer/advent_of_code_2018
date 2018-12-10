using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace advent_of_code_2018
{
    class Day07
    {
        public class Node: IComparable<Node>
        {
            public string name;
            public List<Node> parents;
            public List<Node> children;

            public Node(string p_name)
            {
                name     = p_name;
                parents  = new List<Node>();
                children = new List<Node>();
            }

            public override string ToString()
            {
                return name;
            }

            public override bool Equals(object obj)
            {
                var other = obj as Node;
                return (name == other.name);
            }

            public override int GetHashCode()
            {
                return name.GetHashCode();
            }

            public int CompareTo(Node other)
            {
                return name.CompareTo(other.name);
            }
        }

        public void SolveA()
        {
            IDictionary<string, Node> nodes = new Dictionary<string, Node>();

            foreach (string line in File.ReadLines("07_input.txt"))
            {
                string[] parts = line.Split(" ");
                string parent = parts[1];
                string child  = parts[7];

                if (! nodes.ContainsKey(parent))
                    nodes.Add(parent, new Node(parent));

                if (! nodes.ContainsKey(child))
                    nodes.Add(child, new Node(child));
                
                nodes[parent].children.Add(nodes[child]);
                nodes[child].parents.Add(nodes[parent]);
            }

            List<Node> todo = nodes.Values
                                   .Where(n => n.parents.Count() == 0)
                                   .OrderBy(n => n.name)
                                   .ToList();

            string result = "";
            List<Node> done = new List<Node>() {};

            while (todo.Count() > 0)
            {
                int idx = 0;
                bool found = false;
                Node n = todo[idx];

                while (! found)
                {
                    found = true;
                    foreach (Node p in n.parents)
                    {
                        if (! done.Contains(p))
                        {
                            found = false;
                            idx++;
                            n = todo[idx];
                            break;
                        }
                    }
                }

                todo.RemoveAt(idx);
                result += n.ToString();
                done.Add(n);

                todo = todo.Union(n.children)
                             .OrderBy(s => s)
                             .ToList();
                Console.WriteLine(string.Join("", todo));
            }

            Console.WriteLine("Day 07 A: " + result);  //= FHMEQGIRSXNWZBCLOTUADJPKVY
        }

        public void SolveB()
        {

        }
    }
}
