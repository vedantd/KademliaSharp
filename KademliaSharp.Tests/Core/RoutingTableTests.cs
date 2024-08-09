using System;
using Xunit;
using KademliaSharp.Core;

namespace KademliaSharp.Tests
{
    public class RoutingTableTests
    {
        [Fact]
        public void Constructor_NullLocalNode_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RoutingTable(null, 20));
        }

        [Fact]
        public void AddNode_NewNode_ReturnsTrue()
        {
            var localNode = new Node(NodeId.GenerateRandom(), "local_address");
            var table = new RoutingTable(localNode, 20);
            var newNode = new Node(NodeId.GenerateRandom(), "new_address");

            Assert.True(table.AddNode(newNode));
        }

        [Fact]
        public void RemoveNode_ExistingNode_ReturnsTrue()
        {
            var localNode = new Node(NodeId.GenerateRandom(), "local_address");
            var table = new RoutingTable(localNode, 20);
            var node = new Node(NodeId.GenerateRandom(), "test_address");
            table.AddNode(node);

            Assert.True(table.RemoveNode(node));
        }

        [Fact]
        public void RemoveNode_NonExistingNode_ReturnsFalse()
        {
            var localNode = new Node(NodeId.GenerateRandom(), "local_address");
            var table = new RoutingTable(localNode, 20);
            var node = new Node(NodeId.GenerateRandom(), "test_address");

            Assert.False(table.RemoveNode(node));
        }

        [Fact]
        public void FindClosestNodes_ReturnsCorrectNumberOfNodes()
        {
            var localNode = new Node(NodeId.GenerateRandom(), "local_address");
            var table = new RoutingTable(localNode, 20);
            for (int i = 0; i < 100; i++)
            {
                table.AddNode(new Node(NodeId.GenerateRandom(), $"address_{i}"));
            }

            var targetId = NodeId.GenerateRandom();
            var result = table.FindClosestNodes(targetId, 10);

            Assert.Equal(10, result.Count);
        }

        // [Fact]
        // public void GetBucketCount_ReturnsCorrectNumber()
        // {
        //     var localNode = new Node(NodeId.GenerateRandom(), "local_address");
        //     var table = new RoutingTable(localNode, 20);

        //     Assert.Equal(160, table.GetBucketCount());
        // }
    }
}