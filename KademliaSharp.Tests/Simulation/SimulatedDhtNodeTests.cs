using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KademliaSharp.Core;
using KademliaSharp.Simulation;
using Xunit;

namespace KademliaSharp.Tests
{
    public class SimulatedDhtNodeTests
    {
        private Dictionary<NodeId, SimulatedDhtNode> _network;

        public SimulatedDhtNodeTests()
        {
            _network = new Dictionary<NodeId, SimulatedDhtNode>();
        }

        private SimulatedDhtNode CreateNode()
        {
            var nodeId = NodeId.GenerateRandom();
            var node = new SimulatedDhtNode(nodeId, $"node_{nodeId}", Constants.K, _network);
            _network[nodeId] = node;
            return node;
        }
        [Fact]
        public async Task TestFindNode()
        {
            var node1 = CreateNode();
            var node2 = CreateNode();
            var node3 = CreateNode();

            node1.AddNode(node2.LocalNode);
            node2.AddNode(node3.LocalNode);

            var result = await node1.FindNode(node3.LocalNode.Id);

            Assert.Contains(result, n => n.Id.Equals(node3.LocalNode.Id));
        }

        [Fact]
        public async Task TestStoreAndFindValue()
        {
            var node1 = CreateNode();
            var node2 = CreateNode();

            node1.AddNode(node2.LocalNode);
            node2.AddNode(node1.LocalNode);

            var key = NodeId.GenerateRandom();
            var value = Encoding.UTF8.GetBytes("Test Value");

            await node1.Store(key, value);

            var retrievedValue = await node2.FindValue(key);

            Assert.Equal(value, retrievedValue);
        }

        // [Fact]
        // public async Task NodeLookup_ReturnsKClosestNodes()
        // {
        //     var network = new SimulatedNetwork(100); // Create a network with 100 nodes
        //     var sourceNode = network.GetRandomNode();
        //     var targetId = NodeId.GenerateRandom();

        //     var result = await sourceNode.FindNode(targetId);

        //     Assert.Equal(Constants.K, result.Count);
        //     Assert.True(result.All(n => n.Id.XorDistance(targetId) <= result.Max(r => r.Id.XorDistance(targetId))));
        // }
    }
}