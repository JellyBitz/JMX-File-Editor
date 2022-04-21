using JMXFileEditor.Silkroad.IO;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVRES.ModData
{
    public class ModDataSet : ISerializableBS
    {
        public int Type { get; set; }
        public PrimAnimationType AnimationType { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<IModData> ModData { get; set; } = new List<IModData>();

        public void Deserialize(BSReader reader)
        {
            this.Type = reader.ReadInt32();
            this.AnimationType = (PrimAnimationType)reader.ReadInt32();
            this.Name = reader.ReadString();

            var dataCount = reader.ReadInt32();
            this.ModData.Capacity = dataCount;
            for (int i = 0; i < dataCount; i++)
            {
                var data = ModDataFactory.Create((ModDataType)reader.ReadInt32());
                data.Deserialize(reader);
                this.ModData.Add(data);
            }
        }

        public void Serialize(BSWriter writer)
        {
            writer.Write(this.Type);
            writer.Write((int)this.AnimationType);
            writer.Write(this.Name);

            writer.Write(this.ModData.Count);
            foreach (var data in this.ModData)
            {
                writer.Write((int)data.Type);
                writer.Serialize(data);
            }
        }
    }
}