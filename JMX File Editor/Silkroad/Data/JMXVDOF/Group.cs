using JMXFileEditor.Silkroad.IO;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVDOF
{
    public class Group : ISerializableBS
    {
        #region Public Properties
        public string Name { get; set; } = string.Empty;
        public uint Flag { get; set; }
        public List<uint> BlockIndices { get; set; } = new List<uint>();
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader)
        {
            Name = reader.ReadString();
            Flag = reader.ReadUInt32();
            BlockIndices = new List<uint>(reader.ReadUIntArray(reader.ReadInt32()));
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(Name);
            writer.Write(Flag);
            writer.Write(BlockIndices.Count);
            writer.Write(BlockIndices.ToArray());
        }
        #endregion
    }
}