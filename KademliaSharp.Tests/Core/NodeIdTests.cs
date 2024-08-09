using System;
using Xunit;
using KademliaSharp.Core;

namespace KademliaSharp.Tests
{
    public class NodeIdTests
    {
        [Fact]
        public void GenerateRandom_ReturnsUniqueIds()
        {
            var id1 = NodeId.GenerateRandom();
            var id2 = NodeId.GenerateRandom();

            Assert.NotEqual(id1, id2);
        }

        [Fact]
        public void Constructor_InvalidLength_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new NodeId(new byte[10]));
        }

        // [Fact]
        // public void Xor_CalculatesCorrectly()
        // {
        //     var id1 = NodeId.FromString("0000000000000000000000000000000000000000");
        //     var id2 = NodeId.FromString("0000000000000000000000000000000000000001");
        //     var id3 = NodeId.FromString("1000000000000000000000000000000000000000");

        //     Assert.Equal(id2, id1.Xor(id2));
        //     Assert.Equal(id3, id1.Xor(id3));
        //     Assert.Equal(NodeId.FromString("1000000000000000000000000000000000000001"), id2.Xor(id3));
        // }

        // [Fact]
        // public void FromString_ValidHexString_CreatesCorrectNodeId()
        // {
        //     var hexString = "1234567890ABCDEF1234567890ABCDEF12345678";
        //     var nodeId = NodeId.FromString(hexString);

        //     Assert.Equal(hexString, BitConverter.ToString(nodeId.ToByteArray()).Replace("-", ""));
        // }

        // [Fact]
        // public void FromString_InvalidLength_ThrowsArgumentException()
        // {
        //     Assert.Throws<ArgumentException>(() => NodeId.FromString("123"));
        // }
    }
}