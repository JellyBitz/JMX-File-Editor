using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVRES.ModData
{
    public class ModDataMtrl : IModData
    {
        public override ModDataType Type => ModDataType.ModDataMtrl;

        public uint UnkUInt06 { get; set; }
        public uint UnkUInt07 { get; set; }
        public uint UnkUInt08 { get; set; }
        public List<GradientKey> GradientKeys { get; set; } = new List<GradientKey>();
        public List<CurveKey> CurveKeys { get; set; } = new List<CurveKey>();
        public uint UnkUInt09 { get; set; }
        public uint UnkUInt10 { get; set; }
        public uint UnkUInt11 { get; set; }
        public uint UnkUInt12 { get; set; }
        public uint UnkUInt13 { get; set; }
        public uint UnkUInt14 { get; set; }
        public uint UnkUInt15 { get; set; }
        public uint UnkUInt16 { get; set; }
        public uint UnkUInt17 { get; set; }

        public class GradientKey : ISerializableBS
        {
            public int Time { get; set; }
            public Color4 Value { get; set; } = new Color4();

            public void Deserialize(BSReader reader)
            {
                this.Time = reader.ReadInt32();
                this.Value = reader.ReadColor4();
            }

            public void Serialize(BSWriter writer)
            {
                writer.Write(this.Time);
                writer.Write(this.Value);
            }
        }

        public class CurveKey : ISerializableBS
        {
            public int Time { get; set; }
            public float Value { get; set; }

            public void Deserialize(BSReader reader)
            {
                this.Time = reader.ReadInt32();
                this.Value = reader.ReadSingle();
            }

            public void Serialize(BSWriter writer)
            {
                writer.Write(this.Time);
                writer.Write(this.Value);
            }
        }

        public override void Deserialize(BSReader reader)
        {
            base.Deserialize(reader);
            this.UnkUInt06 = reader.ReadUInt32(); // 1000
            this.UnkUInt07 = reader.ReadUInt32(); // 2
            this.UnkUInt08 = reader.ReadUInt32();

            var gradientKeyCount = reader.ReadInt32();
            for (int i = 0; i < gradientKeyCount; i++)
                this.GradientKeys.Add(reader.Deserialize<GradientKey>());

            if ((this.UnkUInt07 & 4) != 0)
            {
                var curveKeyCount = reader.ReadInt32();
                for (int i = 0; i < curveKeyCount; i++)
                    this.CurveKeys.Add(reader.Deserialize<CurveKey>());
            }

            this.UnkUInt09 = reader.ReadUInt32();
            this.UnkUInt10 = reader.ReadUInt32(); // 1
            this.UnkUInt11 = reader.ReadUInt32(); // 1
            this.UnkUInt12 = reader.ReadUInt32();
            this.UnkUInt13 = reader.ReadUInt32(); // 262661 ?
            this.UnkUInt14 = reader.ReadUInt32(); // 33686530 ?
            this.UnkUInt15 = reader.ReadUInt32(); // 3361998720 ?
            this.UnkUInt16 = reader.ReadUInt32(); // 1065353216 ?
            this.UnkUInt17 = reader.ReadUInt32(); // 1
        }

        public override void Serialize(BSWriter writer)
        {
            base.Serialize(writer);

            writer.Write(this.UnkUInt06);
            writer.Write(this.UnkUInt07);
            writer.Write(this.UnkUInt08);

            writer.Write(this.GradientKeys.Count);
            foreach (var item in this.GradientKeys)
                writer.Serialize(item);

            if ((this.UnkUInt07 & 4) != 0)
            {
                writer.Write(this.CurveKeys.Count);
                foreach (var item in this.CurveKeys)
                    writer.Serialize(item);
            }

            writer.Write(this.UnkUInt09);
            writer.Write(this.UnkUInt10);
            writer.Write(this.UnkUInt11);
            writer.Write(this.UnkUInt12);
            writer.Write(this.UnkUInt13);
            writer.Write(this.UnkUInt14);
            writer.Write(this.UnkUInt15);
            writer.Write(this.UnkUInt16);
            writer.Write(this.UnkUInt17);
        }
    }
}