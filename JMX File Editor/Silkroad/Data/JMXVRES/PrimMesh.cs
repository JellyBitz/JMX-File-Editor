using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
    public class PrimMesh : ISerializableParameterizedBS
    {
        #region Public Properties
        public string Path { get; set; } = string.Empty;
        public int Int01 { get; set; }
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader, params object[] param)
        {
            var flag = (int)param[0];

            this.Path = reader.ReadString();
            if ((flag & 1) != 0)
                this.Int01 = reader.ReadInt32();
        }
        public void Serialize(BSWriter writer, params object[] param)
        {
            var flag = (int)param[0];

            writer.Write(this.Path);
            if ((flag & 1) != 0)
                writer.Write(this.Int01);
        }
        #endregion
    }
}