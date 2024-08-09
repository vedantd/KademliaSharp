using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using KademliaSharp.Core;

namespace KademliaSharp.Tests
{
    public class DhtNodeTests
    {
        private class TestDhtNode : DhtNode
        {
            public TestDhtNode(NodeId nodeId, string address, int k) : base(nodeId, address, k) { }

            protected override Task<List<Node>> RequestFindNode(Node node, NodeId targetId)
            {
                return Task.FromResult(new List<Node> { new Node(targetId, "test_address") });
            }

            protected override Task<byte[]> RequestFindValue(Node node, NodeId key)
            {
                return Task.FromResult(_dataStore.TryGetValue(key, out byte[] value) ? value : null);
            }

            protected override Task RequestStore(Node node, NodeId key, byte[] value)
            {
                _dataStore[key] = value;
                return Task.CompletedTask;
            }
        }

        [Fact]
        public void Constructor_SetsLocalNode()
        {
            var nodeId = NodeId.GenerateRandom();
            var address = "test_address";
            var node = new TestDhtNode(nodeId, address, Constants.K);

            Assert.Equal(nodeId, node.LocalNode.Id);
            Assert.Equal(address, node.LocalNode.Address);
        }

        [Fact]
        public async Task FindNode_ReturnsKClosestNodes()
        {
            var node = new TestDhtNode(NodeId.GenerateRandom(), "test_address", Constants.K);
            var targetId = NodeId.GenerateRandom();

            // Add some nodes to the routing table
            for (int i = 0; i < Constants.K + 5; i++)
            {
                node.AddNode(new Node(NodeId.GenerateRandom(), $"address_{i}"));
            }

            var result = await node.FindNode(targetId);

            Assert.Equal(Constants.K, result.Count);
            Assert.Contains(result, n => n.Id.Equals(targetId));
        }

        [Fact]
        public async Task FindValue_ReturnsValue()
        {
            var node = new TestDhtNode(NodeId.GenerateRandom(), "test_address", Constants.K);
            var key = NodeId.GenerateRandom();
            var expectedValue = Encoding.UTF8.GetBytes("test_value");

            // Store the value first
            await node.Store(key, expectedValue);

            var result = await node.FindValue(key);

            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public async Task Store_DoesNotThrowException()
        {
            var node = new TestDhtNode(NodeId.GenerateRandom(), "test_address", Constants.K);
            var key = NodeId.GenerateRandom();
            var value = Encoding.UTF8.GetBytes("test_value");

            await node.Store(key, value);

            // If we reach here without an exception, the test passes
        }
    }
}