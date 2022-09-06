using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Controller
{
    //StaticEmit
    public class EFCStaticEmit : EFController
    {
        public override string Name => "StaticEmit";

        public EFStaticEmit StaticEmit { get; set; }

        public override void Read(BSReader reader)
        {
            //StaticEmit.Read(reader);
            this.StaticEmit = new EFStaticEmit
            {
                Min = reader.ReadUInt32(),
                Max = reader.ReadUInt32(),
                BurstRate = reader.ReadUInt32(),
                MinParticles = reader.ReadUInt32(),
                SpawnRate = reader.ReadSingle(),
            };
        }
    }
}