using JMXFileEditor.Silkroad.IO;

using System;
using System.ComponentModel;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public struct EFStaticEmit
    {
        public uint Min { get; set; }
        public uint Max { get; set; }
        public uint BurstRate { get; set; }
        public uint MinParticles { get; set; }
        public float SpawnRate { get; set; }

        public void Read(BSReader reader)
        {
            Min = reader.ReadUInt32();
            Max = reader.ReadUInt32();
            BurstRate = reader.ReadUInt32();
            MinParticles = reader.ReadUInt32();
            SpawnRate = reader.ReadSingle();
        }
    }
}