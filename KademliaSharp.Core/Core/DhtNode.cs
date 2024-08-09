using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KademliaSharp.Core
{
    public class DhtNode
    {
        public Node LocalNode { get; }
        protected RoutingTable _routingTable;
        protected Dictionary<NodeId, byte[]> _dataStore;

        public DhtNode(NodeId nodeId, string address, int k)
        {
            LocalNode = new Node(nodeId, address);
            _routingTable = new RoutingTable(LocalNode, k);
            _dataStore = new Dictionary<NodeId, byte[]>();
        }

        public async Task<List<Node>> FindNode(NodeId targetId)
        {
            var closestNodes = _routingTable.FindClosestNodes(targetId, Constants.K);
            var result = new HashSet<Node>(closestNodes);
            var nodesToQuery = new Queue<Node>(closestNodes);
            var queriedNodes = new HashSet<Node>();

            while (nodesToQuery.Count > 0)
            {
                var currentNode = nodesToQuery.Dequeue();
                if (queriedNodes.Contains(currentNode))
                    continue;

                queriedNodes.Add(currentNode);

                var newNodes = await RequestFindNode(currentNode, targetId);

                foreach (var node in newNodes)
                {
                    if (!result.Contains(node))
                    {
                        result.Add(node);
                        nodesToQuery.Enqueue(node);
                    }
                }

                if (result.Count >= Constants.K)
                    break;
            }

            return result.OrderBy(n => n.Id.Xor(targetId)).Take(Math.Min(Constants.K, result.Count)).ToList();
        }

        public async Task<byte[]> FindValue(NodeId key)
        {
            if (_dataStore.TryGetValue(key, out byte[] value))
                return value;

            var closestNodes = await FindNode(key);
            foreach (var node in closestNodes)
            {
                var foundValue = await RequestFindValue(node, key);
                if (value != null)
                {
                    await Store(key, value);  // Cache the value locally
                    return value;
                }
            }

            return null;  // Value not found
        }

        public async Task Store(NodeId key, byte[] value)
        {
            var closestNodes = await FindNode(key);
            foreach (var node in closestNodes)
            {
                await RequestStore(node, key, value);
            }

            // Store locally as well
            _dataStore[key] = value;
        }

        public void AddNode(Node node)
        {
            _routingTable.AddNode(node);
        }

        // These methods would be implemented by a network layer in a real system
        protected virtual Task<List<Node>> RequestFindNode(Node node, NodeId targetId)
        {
            throw new NotImplementedException("Network layer not implemented");
        }

        protected virtual Task<byte[]> RequestFindValue(Node node, NodeId key)
        {
            throw new NotImplementedException("Network layer not implemented");
        }

        protected virtual Task RequestStore(Node node, NodeId key, byte[] value)
        {
            throw new NotImplementedException("Network layer not implemented");
        }
    }
}