// Core/NodeId.cs
using System;
using System.Linq;

namespace KademliaSharp.Core
{
    public class NodeId : IEquatable<NodeId>, IComparable<NodeId>
    {
        
        public const int IdLength = 20; // 160 bits
        private readonly byte[] _id;

        public NodeId(byte[] id)
        {
            if (id == null || id.Length != IdLength)
                throw new ArgumentException($"ID must be {IdLength} bytes long", nameof(id));

            _id = (byte[])id.Clone();
        }

        public static NodeId GenerateRandom()
        {
            var rng = new Random();
            var id = new byte[IdLength];
            rng.NextBytes(id);
            return new NodeId(id);
        }

        public byte[] ToByteArray() => (byte[])_id.Clone();

        public bool Equals(NodeId? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _id.SequenceEqual(other._id);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NodeId)obj);
        }

        public override int GetHashCode()
        {
            return _id.Aggregate(0, (current, b) => current * 31 + b);
        }

        public int CompareTo(NodeId? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return _id.Zip(other._id, (a, b) => a.CompareTo(b)).FirstOrDefault(c => c != 0);
        }

        public static NodeId operator ^(NodeId left, NodeId right)
        {
            var result = new byte[IdLength];
            for (int i = 0; i < IdLength; i++)
            {
                result[i] = (byte)(left._id[i] ^ right._id[i]);
            }
            return new NodeId(result);
        }
        public NodeId Xor(NodeId other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            var result = new byte[IdLength];
            for (int i = 0; i < IdLength; i++)
            {
                result[i] = (byte)(_id[i] ^ other._id[i]);
            }
            return new NodeId(result);
        }
    }
}