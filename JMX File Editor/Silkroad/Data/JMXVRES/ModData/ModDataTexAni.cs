using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVRES.ModData
{
    public class ModDataTexAni : IModData
    {
        #region Public Properties
        public override ModDataType Type => ModDataType.ModDataTexAni;
        public uint UnkUInt06 { get; set; }
        public uint UnkUInt07 { get; set; }
        public uint UnkUInt08 { get; set; }
        public uint UnkUInt09 { get; set; }
        public uint UnkUInt10 { get; set; }
        public Matrix4x4 Matrix { get; set; } = new Matrix4x4();
        #endregion

        #region Interface Implementation
        public override void Deserialize(BSReader reader)
        {
            base.Deserialize(reader);

            this.UnkUInt06 = reader.ReadUInt32();
            this.UnkUInt07 = reader.ReadUInt32();
            this.UnkUInt08 = reader.ReadUInt32();
            this.UnkUInt09 = reader.ReadUInt32();
            this.UnkUInt10 = reader.ReadUInt32();
            this.Matrix = reader.ReadMatrix4x4();
        }

        public override void Serialize(BSWriter writer)
        {
            base.Serialize(writer);

            writer.Write(this.UnkUInt06);
            writer.Write(this.UnkUInt07);
            writer.Write(this.UnkUInt08);
            writer.Write(this.UnkUInt09);
            writer.Write(this.UnkUInt10);
            writer.Write(this.Matrix);
        }
        #endregion
    }
}