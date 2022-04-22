using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
    public class PrimAnimation : ISerializableBS
    {
        #region Public Properties
        public string Path { get; set; } = string.Empty;
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader)
        {
            this.Path = reader.ReadString();
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(this.Path);
        }
        #endregion
    }
}