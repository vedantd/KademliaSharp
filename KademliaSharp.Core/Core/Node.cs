using System;

namespace KademliaSharp.Core
{
    public class Node : IEquatable<Node>
    {
        public NodeId Id { get; }
        public string Address { get; }

        public Node(NodeId id, string address)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public bool Equals(Node? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id) && Address == other.Address;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Node)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Address);
        }
    }
}