using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14_2
{
    class Graph
    {
        private readonly int MAX_VERTS = 20;
        private readonly int INFINITY = 1000000;
        private Vertex[] vertexList;
        private int nVerts;
        private int nTree;
        private int CurrentVert;
        private int StartToCurrent;
        private DistPar[] sPath;
        public Graph()
        {
            vertexList = new Vertex[MAX_VERTS];
            nVerts = 0;
            nTree = 0;
            CurrentVert = 0;
            StartToCurrent = 0;
            sPath = new DistPar[MAX_VERTS];
        }
        public void addVertex(char lab)
        {
            vertexList[nVerts++] = new Vertex(lab);
        }
        public void addEdge(int start, int end, int weight)
        {
            Edge theEdge = new Edge(weight, end);
            vertexList[start].addEdge(theEdge);
        }
        public void path()
        {
            for (int j = 0; j < nVerts; j++)
            {
                int startTree = j;
                vertexList[startTree].isInTree = true;
                nTree = 1;

                List<Edge> edgesList = vertexList[startTree].edges;
                for (int i = 0; i < nVerts; i++)
                    sPath[i] = new DistPar(startTree, INFINITY);

                for (int i = 0; i < edgesList.Count; i++)
                {
                    Edge tempEdge = edgesList[i];
                    sPath[tempEdge.end] = new DistPar(startTree, tempEdge.weight);
                }

                while (nTree < nVerts)
                {
                    int indexMin = getMin();
                    int minDist = sPath[indexMin].distance;

                    if (minDist == INFINITY)
                    {
                        Console.WriteLine("There are unreachable vertices");
                        break;
                    }
                    else
                    {
                        CurrentVert = indexMin;
                        StartToCurrent = sPath[indexMin].distance;
                    }
                    vertexList[CurrentVert].isInTree = true;
                    nTree++;
                    adjust_sPath();
                }
                displayPaths();

                nTree = 0;
                for (int i = 0; i < nVerts; i++)
                    vertexList[i].isInTree = false;
            }
        }
        public int getMin()
        {
            int minDist = INFINITY;
            int indexMin = 0;

            for (int i = 1; i < nVerts; i++)
            {
                if (!vertexList[i].isInTree && sPath[i].distance < minDist)
                {
                    minDist = sPath[i].distance;
                    indexMin = i;
                }
            }
            return indexMin;
        }
        public void adjust_sPath()
        {
            List<Edge> list = vertexList[CurrentVert].edges;
            for (int i = 0; i < list.Count; i++)         // go across columns
            {
                // if this column's vertex already in tree, skip it
                int v = list[i].end;
                if (vertexList[v].isInTree)
                {
                    continue;
                }
                // calculate distance for one sPath entry
                // get edge from currentVert to fringe
                Edge e = list[i];
                int fringe = e.end;
                int currentToFringe = e.weight;
                // add distance from start
                int startToFringe = StartToCurrent + currentToFringe;
                // get distance of current sPath entry
                int sPathDist = sPath[fringe].distance;

                // compare distance from start with sPath entry
                if (startToFringe < sPathDist)   // if shorter,
                {                            // update sPath
                    sPath[fringe].parentVert = CurrentVert;
                    sPath[fringe].distance = startToFringe;
                }
            }
        }
        public void displayPaths()
        {
            for (int i = 0; i < nVerts; i++)
            {
                Console.Write(vertexList[i].lable + "=");
                if (sPath[i].distance == INFINITY)
                    Console.Write("inf");
                else
                    Console.Write(sPath[i].distance);
                char parent = vertexList[sPath[i].parentVert].lable;
                Console.Write("(" + parent + ") ");
            }
            Console.WriteLine();
        }
    }
}
