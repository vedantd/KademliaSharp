using System;
using Xunit;
using KademliaSharp.Core;

namespace KademliaSharp.Tests
{
    public class NodeTests
    {
        [Fact]
        public void Constructor_ValidArguments_CreatesNode()
        {
            var id = NodeId.GenerateRandom();
            var address = "127.0.0.1:8080";
            var node = new Node(id, address);

            Assert.Equal(id, node.Id);
            Assert.Equal(address, node.Address);
        }

        [Fact]
        public void Constructor_NullId_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Node(null, "127.0.0.1:8080"));
        }

        [Fact]
        public void Constructor_NullAddress_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Node(NodeId.GenerateRandom(), null));
        }

        [Fact]
        public void Equals_SameNode_ReturnsTrue()
        {
            var id = NodeId.GenerateRandom();
            var address = "127.0.0.1:8080";
            var node1 = new Node(id, address);
            var node2 = new Node(id, address);

            Assert.True(node1.Equals(node2));
        }

        [Fact]
        public void Equals_DifferentNodes_ReturnsFalse()
        {
            var node1 = new Node(NodeId.GenerateRandom(), "127.0.0.1:8080");
            var node2 = new Node(NodeId.GenerateRandom(), "127.0.0.1:8081");

            Assert.False(node1.Equals(node2));
        }
    }
}