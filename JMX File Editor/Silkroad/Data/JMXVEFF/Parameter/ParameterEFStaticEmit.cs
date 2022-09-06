using JMXFileEditor.Silkroad.IO;


namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterEFStaticEmit : EEParameter<EFStaticEmit>
    {
        public override string Name => "EFStaticEmit";

        public override void Read(BSReader reader)
        {
            this.Value = new EFStaticEmit
            {
                Min = reader.ReadUInt32(),
                Max = reader.ReadUInt32(),
                BurstRate = reader.ReadUInt32(),
                MinParticles = reader.ReadUInt32(),
                SpawnRate = reader.ReadSingle(),
            };
        }

        public override void Write(BSWriter writer)
        {
            writer.Write(Value.Min);
            writer.Write(Value.Max);
            writer.Write(Value.BurstRate);
            writer.Write(Value.MinParticles);
            writer.Write(Value.SpawnRate);
        }
    }
}