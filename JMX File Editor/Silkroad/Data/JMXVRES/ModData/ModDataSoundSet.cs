using JMXFileEditor.Silkroad.IO;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVRES.ModData
{
    public class ModDataSoundSet : ISerializableBS
    {
        public string Name { get; set; } = string.Empty;
        public List<ModDataSoundTrack> Tracks { get; set; } = new List<ModDataSoundTrack>();

        public void Deserialize(BSReader reader)
        {
            this.Name = reader.ReadString();
            var trackCount = reader.ReadInt32();
            for (int i = 0; i < trackCount; i++)
            {
                if (reader.ReadUInt32() == 0)
                    continue;

                this.Tracks.Add(reader.Deserialize<ModDataSoundTrack>());
            }
        }

        public void Serialize(BSWriter writer)
        {
            writer.Write(this.Name);
            writer.Write(this.Tracks.Count);
            foreach (var track in this.Tracks)
            {
                writer.Write(1);
                writer.Serialize(track);
            }
        }
    }
}