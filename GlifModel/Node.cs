using System.Collections.Generic;

namespace SP2.Glif.Model
{
    public class Node
    {
        public Node(string id)
        {
            Id = id;
            Outgoing = new List<Vertex>();
            Incoming = new List<Vertex>();
            Parameters = new List<Parameter>();
        }

        public string Id { get; private set;}
        public IList<Vertex> Outgoing { get; private set;}
        public IList<Vertex> Incoming { get; private set;}
        public IList<Parameter> Parameters { get; private set; }
    }
}
