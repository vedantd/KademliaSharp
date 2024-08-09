// Core/RoutingTable.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace KademliaSharp.Core
{
    public class RoutingTable
    {
        private readonly Node _localNode;
        private readonly List<KBucket> _buckets;
        private readonly int _kBucketSize;

        public RoutingTable(Node localNode, int kBucketSize)
        {
            _localNode = localNode ?? throw new ArgumentNullException(nameof(localNode));
            _kBucketSize = kBucketSize;
            _buckets = Enumerable.Range(0, NodeId.IdLength * 8)
                                 .Select(_ => new KBucket(kBucketSize))
                                 .ToList();
        }

        public bool AddNode(Node node)
        {
            int bucketIndex = GetBucketIndex(node.Id);
            return _buckets[bucketIndex].AddNode(node);
        }


        public bool RemoveNode(Node node)
        {
            int bucketIndex = GetBucketIndex(node.Id);
            return _buckets[bucketIndex].RemoveNode(node);
        }

        public List<Node> FindClosestNodes(NodeId targetId, int count)
        {
            var allNodes = _buckets.SelectMany(b => b.GetNodes());
            return allNodes.OrderBy(n => n.Id ^ targetId)
                           .Take(count)
                           .ToList();
        }

        private int GetBucketIndex(NodeId nodeId)
        {
            var distance = _localNode.Id ^ nodeId;
            var leadingZeros = distance.ToByteArray()
                                       .TakeWhile(b => b == 0)
                                       .Count() * 8;
            if (leadingZeros == NodeId.IdLength * 8) return NodeId.IdLength * 8 - 1;
            return NodeId.IdLength * 8 - leadingZeros - 1;
        }
    }
}