using JMXFileEditor.Silkroad.IO;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVDOF
{
    public class GridBucket : ISerializableBS
    {
        #region Public Properties
        public uint ID { get; set; }
        public List<uint> BlockIndices { get; set; } = new List<uint>();
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader)
        {
            ID = reader.ReadUInt32();
            BlockIndices = new List<uint>(reader.ReadUIntArray(reader.ReadInt32()));
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(ID);
            writer.Write(BlockIndices.Count);
            writer.Write(BlockIndices.ToArray());
        }
        #endregion
    }
}