using System.Collections.Generic;

namespace SP2.Glif.Model
{
    public class Vertex
    {
        public Vertex()
        {
            Parameters = new List<Parameter>();
        }

        public Node Source { get; set; }
        public Node Destination { get; set; }
        public IList<Parameter> Parameters { get; private set; }
    }
}
