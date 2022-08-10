using JMXFileEditor.Silkroad.IO;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVDOF
{
    public class Link : ISerializableBS
    {
        #region Public Properties
        public List<uint> BlockIndices { get; set; } = new List<uint>();
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader)
        {
            BlockIndices = new List<uint>(reader.ReadUIntArray(reader.ReadInt32()));
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(BlockIndices.Count);
            writer.Write(BlockIndices.ToArray());
        }
        #endregion
    }
}