using JMXFileEditor.Silkroad.IO;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVRES.ModData
{
    public class ModDataParticle : IModData
    {
        public override ModDataType Type => ModDataType.ModDataParticle;

        public List<ModDataParticleConfig> Particles { get; set; } = new List<ModDataParticleConfig>();

        public override void Deserialize(BSReader reader)
        {
            base.Deserialize(reader);
            var particleCount = reader.ReadInt32();
            for (int i = 0; i < particleCount; i++)
                this.Particles.Add(reader.Deserialize<ModDataParticleConfig>());
        }

        public override void Serialize(BSWriter writer)
        {
            base.Serialize(writer);
            writer.Write(this.Particles.Count);
            foreach (var item in this.Particles)
                writer.Serialize(item);
        }
    }
}