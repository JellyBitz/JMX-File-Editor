using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.Common
{
    public class ObjectGeneralInfo : ISerializableBS
    {
        public ObjectGeneralType Type { get; set; }
        public ObjectGeneralCategory Category { get; set; }
        public string Name { get; set; }
        public int Int0 { get; set; }
        public int Int1 { get; set; }

        public void Deserialize(BSReader reader)
        {
            this.Type = (ObjectGeneralType)reader.ReadInt16();
            this.Category = (ObjectGeneralCategory)reader.ReadInt16();
            this.Name = reader.ReadString();
            this.Int0 = reader.ReadInt32();
            this.Int1 = reader.ReadInt32();
        }

        public void Serialize(BSWriter writer)
        {
            writer.Write((short)this.Type);
            writer.Write((short)this.Category);
            writer.Write(this.Name);
            writer.Write(this.Int0);
            writer.Write(this.Int1);
        }
    }
}