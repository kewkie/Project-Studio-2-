using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SP2.Glif.Model
{
    public class GlifWorkflow : IEnumerable<Node>
    {
        private readonly List<Node> _nodes = new List<Node>();
        private Node _firstNode;

        public string Name { get; set; }
        public string Type { get; set; }
        
        public Vertex AddNodeConnection(string sourceId, string destinationId)
        {
            var sourceNode = GetOrCreate(sourceId);
            var destNode = GetOrCreate(destinationId);

            var vertex = sourceNode.Outgoing.FirstOrDefault(x => x.Destination == destNode);
            if (vertex != null)
                return vertex;
            
            vertex = new Vertex {Source = sourceNode, Destination = destNode};
            sourceNode.Outgoing.Add(vertex);
            destNode.Incoming.Add(vertex);
            return vertex;
        }

        public void AddNodeParam(string nodeId, string attr, string val)
        {
            var node = GetOrCreate(nodeId);
            var parameter = new Parameter {Key = attr, Value = val};
            node.Parameters.Add(parameter);
        }

        public void AddVertexParam(string sourceId, string destinationId, string attr, string val)
        {
            var vertex = AddNodeConnection(sourceId, destinationId);
            vertex.Parameters.Add(new Parameter {Key = attr, Value = val}); 
        }

        private Node GetOrCreate(string id)
        {
            var node = _nodes.FirstOrDefault(n => n.Id == id);
            if (node != null)
                return node;
            
            node = new Node(id);
            _nodes.Add(node);
            return node;
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
