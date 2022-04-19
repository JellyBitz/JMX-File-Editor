using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVBMT
{
    public class PrimMtrl
    {
        public string Name { get; set; } = string.Empty;
        public Color4 Diffuse { get; set; } = new Color4();
        public Color4 Ambient { get; set; } = new Color4();
        public Color4 Specular { get; set; } = new Color4();
        public Color4 Emissive { get; set; } = new Color4();
        public float UnkFloat01 { get; set; }
        public uint Flags { get; set; }
        public string DiffuseMapPath { get; set; } = string.Empty;
        public float UnkFloat02 { get; set; }
        public byte UnkByte01 { get; set; }
        public byte UnkByte02 { get; set; }
        public bool IsAbsolutePath { get; set; }
        public string NormalMapPath { get; set; } = string.Empty;
        public uint UnkUInt01 { get; set; }
    }
}