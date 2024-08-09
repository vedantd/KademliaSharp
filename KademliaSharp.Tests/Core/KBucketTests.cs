using System;
using Xunit;
using KademliaSharp.Core;

namespace KademliaSharp.Tests
{
    public class KBucketTests
    {
        [Fact]
        public void Constructor_NegativeCapacity_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new KBucket(-1));
        }

        [Fact]
        public void AddNode_BucketNotFull_ReturnsTrue()
        {
            var bucket = new KBucket(2);
            var node = new Node(NodeId.GenerateRandom(), "127.0.0.1:8080");

            Assert.True(bucket.AddNode(node));
            Assert.Equal(1, bucket.Count);
            Assert.Contains(node, bucket.GetNodes());
        }

        [Fact]
        public void AddNode_BucketFull_ReturnsFalse()
        {
            var bucket = new KBucket(1);
            var node1 = new Node(NodeId.GenerateRandom(), "127.0.0.1:8080");
            var node2 = new Node(NodeId.GenerateRandom(), "127.0.0.1:8081");

            bucket.AddNode(node1);
            Assert.False(bucket.AddNode(node2));
            Assert.Equal(1, bucket.Count);
            Assert.DoesNotContain(node2, bucket.GetNodes());
        }

        [Fact]
        public void RemoveNode_ExistingNode_ReturnsTrue()
        {
            var bucket = new KBucket(2);
            var node = new Node(NodeId.GenerateRandom(), "127.0.0.1:8080");
            bucket.AddNode(node);

            Assert.True(bucket.RemoveNode(node));
            Assert.Equal(0, bucket.Count);
            Assert.DoesNotContain(node, bucket.GetNodes());
        }

        [Fact]
        public void RemoveNode_NonExistingNode_ReturnsFalse()
        {
            var bucket = new KBucket(2);
            var node = new Node(NodeId.GenerateRandom(), "127.0.0.1:8080");

            Assert.False(bucket.RemoveNode(node));
        }
    }
}