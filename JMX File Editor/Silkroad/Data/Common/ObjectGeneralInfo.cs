using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.Common
{
    public class ObjectGeneralInfo : ISerializableBS
    {
        #region Public Properties
        public ObjectGeneralType Type { get; set; }
        public ObjectGeneralCategory Category { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Int01 { get; set; }
        public int Int02 { get; set; }
        #endregion

        #region Interface Implementations
        public void Deserialize(BSReader reader)
        {
            this.Type = (ObjectGeneralType)reader.ReadInt16();
            this.Category = (ObjectGeneralCategory)reader.ReadInt16();
            this.Name = reader.ReadString();
            this.Int01 = reader.ReadInt32();
            this.Int02 = reader.ReadInt32();
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write((short)this.Type);
            writer.Write((short)this.Category);
            writer.Write(this.Name);
            writer.Write(this.Int01);
            writer.Write(this.Int02);
        }
        #endregion
    }
}