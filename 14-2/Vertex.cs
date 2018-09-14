using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14_2
{
    class Vertex
    {
        public char lable;
        public bool isInTree;
        public List<Edge> edges;
        public Vertex(char l)
        {
            lable = l;
            isInTree = false;
            edges = new List<Edge>();
        }
        public void addEdge(Edge edg)
        {
            edges.Add(edg);
        }
    }
}
