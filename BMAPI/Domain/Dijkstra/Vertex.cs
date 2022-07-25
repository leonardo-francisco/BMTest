using System;

namespace BMAPI.Domain.Dijkstra
{
    public class Vertex
    {
        public String name;
        public int status;
        public int predecessor;
        public int pathLength;

        public Vertex(String name)
        {
            this.name = name;
        }
    }
}
