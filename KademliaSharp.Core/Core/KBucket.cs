// Core/KBucket.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace KademliaSharp.Core
{
    public class KBucket
    {
        private readonly List<Node> _nodes;
        private readonly int _capacity;

        public KBucket(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be positive", nameof(capacity));

            _capacity = capacity;
            _nodes = new List<Node>(capacity);
        }

        public bool AddNode(Node node)
        {
            if (_nodes.Count < _capacity)
            {
                _nodes.Add(node);
                return true;
            }
            return false;
        }

        public bool RemoveNode(Node node)
        {
            return _nodes.Remove(node);
        }

        public bool Contains(Node node)
        {
            return _nodes.Contains(node);
        }

        public IReadOnlyList<Node> GetNodes()
        {
            return _nodes.AsReadOnly();
        }

        public int Count => _nodes.Count;

        public bool IsFull => _nodes.Count >= _capacity;
    }
}