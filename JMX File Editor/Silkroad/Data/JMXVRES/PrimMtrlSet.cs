using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
    public class PrimMtrlSet : ISerializableBS
    {
        #region Public Properties
        public int Index { get; set; }
        public string Path { get; set; } = string.Empty;
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader)
        {
            this.Index = reader.ReadInt32();
            this.Path = reader.ReadString();
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(this.Index);
            writer.Write(this.Path);
        }
        #endregion
    }
}