using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVRES.ModData
{
    public class ModDataSoundTrack : ISerializableBS
    {
        #region Public Properties
        public string Path { get; set; }
        public int Time { get; set; }
        public string Event { get; set; }
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader)
        {
            this.Path = reader.ReadString();
            this.Time = reader.ReadInt32();
            this.Event = reader.ReadString();
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(this.Path);
            writer.Write(this.Time);
            writer.Write(this.Event);
        }
        #endregion
    }
}