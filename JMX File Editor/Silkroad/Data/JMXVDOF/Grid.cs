using JMXFileEditor.Silkroad.IO;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVDOF
{
    public class Grid : ISerializableBS
    {
        #region Public Properties
        public uint Width { get; set; }
        public uint Height { get; set; }
        public uint Lenght { get; set; }
        public List<GridBucket> BucketList { get; set; } = new List<GridBucket>();
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader)
        {
            Width = reader.ReadUInt32();
            Height = reader.ReadUInt32();
            Lenght = reader.ReadUInt32();

            var count = reader.ReadInt32();
            BucketList = new List<GridBucket>(count);
            for (int i = 0; i < count; i++)
                BucketList.Add(reader.Deserialize<GridBucket>());
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(Width);
            writer.Write(Height);
            writer.Write(Lenght);

            writer.Write(BucketList.Count);
            for (int i = 0; i < BucketList.Count; i++)
                writer.Serialize(BucketList[i]);
        }
        #endregion
    }
}