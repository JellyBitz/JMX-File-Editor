using JMXFileEditor.Silkroad.IO;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVRES.ModData
{
    public class ModDataSound : IModData
    {
        public override ModDataType Type => ModDataType.ModDataSound;

        public uint UnkUInt06 { get; set; }
        public uint UnkUInt07 { get; set; }
        public uint UnkUInt08 { get; set; }
        public float UnkFloat02 { get; set; }
        public float UnkFloat03 { get; set; }

        public uint UnkUInt09 { get; set; }
        public uint UnkUInt10 { get; set; }
        public uint UnkUInt11 { get; set; }
        public uint UnkUInt12 { get; set; }

        public uint UnkUInt13 { get; set; }
        public uint UnkUInt14 { get; set; }
        public List<ModDataSoundSet> SoundSet { get; set; } = new List<ModDataSoundSet>();

        public override void Deserialize(BSReader reader)
        {
            base.Deserialize(reader);

            // Check working set
            var soundSetCount = reader.ReadInt32();
            if (soundSetCount <= 0)
                return;

            // Continue reading
            this.UnkUInt06 = reader.ReadUInt32();
            this.UnkUInt07 = reader.ReadUInt32();
            this.UnkUInt08 = reader.ReadUInt32();
            this.UnkFloat02 = reader.ReadSingle(); // 10
            this.UnkFloat03 = reader.ReadSingle(); // 100
            this.UnkUInt09 = reader.ReadUInt32();
            this.UnkUInt10 = reader.ReadUInt32();
            this.UnkUInt11 = reader.ReadUInt32();
            this.UnkUInt12 = reader.ReadUInt32();
            this.UnkUInt13 = reader.ReadUInt32();
            this.UnkUInt14 = reader.ReadUInt32();

            for (int i = 0; i < soundSetCount; i++)
                this.SoundSet.Add(reader.Deserialize<ModDataSoundSet>());
        }

        public override void Serialize(BSWriter writer)
        {
            base.Serialize(writer);

            writer.Write(this.SoundSet.Count);
            if (this.SoundSet.Count == 0)
                return;

            writer.Write(this.UnkUInt06);
            writer.Write(this.UnkUInt07);
            writer.Write(this.UnkUInt08);
            writer.Write(this.UnkFloat02);
            writer.Write(this.UnkFloat03);
            writer.Write(this.UnkUInt09);
            writer.Write(this.UnkUInt10);
            writer.Write(this.UnkUInt11);
            writer.Write(this.UnkUInt12);
            writer.Write(this.UnkUInt13);
            writer.Write(this.UnkUInt14);

            foreach (var item in this.SoundSet)
                writer.Serialize(item);
        }
    }
}