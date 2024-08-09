using System.Collections.Generic;
using System.Threading.Tasks;
using KademliaSharp.Core;

namespace KademliaSharp.Simulation
{
    public class SimulatedDhtNode : DhtNode
    {
        private Dictionary<NodeId, SimulatedDhtNode> _network;

        public SimulatedDhtNode(NodeId nodeId, string address, int k, Dictionary<NodeId, SimulatedDhtNode> network) 
            : base(nodeId, address, k)
        {
            _network = network;
        }

        protected override Task<List<Node>> RequestFindNode(Node node, NodeId targetId)
        {
            return Task.FromResult(_network[node.Id]._routingTable.FindClosestNodes(targetId, Constants.K));
        }

        protected override Task<byte[]> RequestFindValue(Node node, NodeId key)
        {
            if (_network[node.Id]._dataStore.TryGetValue(key, out byte[] value))
                return Task.FromResult(value);
            return Task.FromResult<byte[]>(null);
        }

        protected override Task RequestStore(Node node, NodeId key, byte[] value)
        {
            _network[node.Id]._dataStore[key] = value;
            return Task.CompletedTask;
        }
    }
}