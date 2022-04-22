using JMXFileEditor.Silkroad.IO;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVRES.ModData
{
    public class ModDataBumpEnv : ModDataEnvMap
    {
        #region Public Properties
        public override ModDataType Type => ModDataType.ModDataBumpEnv;
        public float UnkFloat02 { get; set; }
        public float UnkFloat03 { get; set; }
        public float UnkFloat04 { get; set; }
        public float UnkFloat05 { get; set; }
        public float UnkFloat06 { get; set; }
        public float UnkFloat07 { get; set; }
        public List<string> Textures { get; set; } = new List<string>();
        #endregion

        #region Interface Implementation
        public override void Deserialize(BSReader reader)
        {
            base.Deserialize(reader);

            this.UnkFloat02 = reader.ReadSingle();
            this.UnkFloat03 = reader.ReadSingle();
            this.UnkFloat04 = reader.ReadSingle();
            this.UnkFloat05 = reader.ReadSingle();
            this.UnkFloat06 = reader.ReadSingle();
            this.UnkFloat07 = reader.ReadSingle();

            var textureCount = reader.ReadInt32();
            for (int i = 0; i < textureCount; i++)
            {
                // Check if entry is not enabled
                if (reader.ReadByte() == 0)
                    continue;

                this.Textures.Add(reader.ReadString());
            }
        }
        public override void Serialize(BSWriter writer)
        {
            base.Serialize(writer);

            writer.Write(this.UnkFloat02);
            writer.Write(this.UnkFloat03);
            writer.Write(this.UnkFloat04);
            writer.Write(this.UnkFloat05);
            writer.Write(this.UnkFloat06);
            writer.Write(this.UnkFloat07);

            writer.Write(this.Textures.Count);
            for (int i = 0; i < this.Textures.Count; i++)
            {
                var texture = this.Textures[i];
                if (string.IsNullOrWhiteSpace(texture))
                {
                    writer.Write((byte)0);
                    continue;
                }

                writer.Write((byte)1);
                writer.Write(texture);
            }
        }
        #endregion
    }
}