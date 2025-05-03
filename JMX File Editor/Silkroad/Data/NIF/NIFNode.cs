using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;
using System.Drawing;

namespace JMXFileEditor.Silkroad.Data.NIF
{
    public class NIFNode : ISerializableBS
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string BackgroundImagePath { get; set; }
        public string StringID { get; set; }
        public string Description { get; set; }
        public NIFType Type { get; set; }
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int GrandParentId { get; set; } // or root id?
        public int Unknown02 { get; set; }
        public int Unknown03 { get; set; }
        public Color32 Color { get; set; }
        public Rectangle Rectangle { get; set; }
        public Vector2 TopLeft { get; set; }
        public Vector2 TopRight { get; set; }
        public Vector2 BottomRight { get; set; }
        public Vector2 BottomLeft { get; set; }
        public int Unknown04 { get; set; }
        public int ContentId { get; set; }
        public bool IsRoot { get; set; }
        public int Unknown07 { get; set; }
        public int Unknown08 { get; set; }
        public int Unknown09 { get; set; }
        public int Unknown10 { get; set; }
        public int Unknown11 { get; set; }
        public int Unknown12 { get; set; }
        public int Unknown13 { get; set; }
        public int Unknown14 { get; set; }
        public int Unknown15 { get; set; }
        public int Unknown16 { get; set; }
        public int Unknown17 { get; set; }
        public int Unknown18 { get; set; }
        public int Unknown19 { get; set; }
        public NIFStyle Style { get; set; }


        public void Deserialize(BSReader reader)
        {
            Name = reader.ReadString(64);
            ImagePath = reader.ReadString(256);
            BackgroundImagePath = reader.ReadString(256);
            StringID = reader.ReadString(128);
            Description = reader.ReadString(64);
            Type = (NIFType)reader.ReadInt32();
            Id = reader.ReadInt32();
            ParentId = reader.ReadInt32();
            GrandParentId = reader.ReadInt32();
            Unknown02 = reader.ReadInt32();
            Unknown03 = reader.ReadInt32();
            Color = reader.ReadColor32();
            Rectangle = reader.ReadRectangle();
            TopLeft = reader.ReadVector2();
            TopRight = reader.ReadVector2();
            BottomRight = reader.ReadVector2();
            BottomLeft = reader.ReadVector2();
            Unknown04 = reader.ReadInt32();
            ContentId = reader.ReadInt32();
            IsRoot = reader.ReadInt32() != 0;
            Unknown07 = reader.ReadInt32();
            Unknown08 = reader.ReadInt32();
            Unknown09 = reader.ReadInt32();
            Unknown10 = reader.ReadInt32();
            Unknown11 = reader.ReadInt32();
            Unknown12 = reader.ReadInt32();
            Unknown13 = reader.ReadInt32();
            Unknown14 = reader.ReadInt32();
            Unknown15 = reader.ReadInt32();
            Unknown16 = reader.ReadInt32();
            Unknown17 = reader.ReadInt32();
            Unknown18 = reader.ReadInt32();
            Unknown19 = reader.ReadInt32();
            Style = (NIFStyle)reader.ReadUInt32();

        }

        public void Serialize(BSWriter writer)
        {
            writer.Write(Name, 64);
            writer.Write(ImagePath, 256);
            writer.Write(BackgroundImagePath, 256);
            writer.Write(StringID, 128);
            writer.Write(Description, 64);
            writer.Write((int)Type);
            writer.Write(Id);
            writer.Write(ParentId);
            writer.Write(GrandParentId);
            writer.Write(Unknown02);
            writer.Write(Unknown03);
            writer.Write(Color);
            writer.Write(Rectangle);
            writer.Write(TopLeft);
            writer.Write(TopRight);
            writer.Write(BottomRight);
            writer.Write(BottomLeft);
            writer.Write(Unknown04);
            writer.Write(ContentId);
            writer.Write(IsRoot);
            writer.Write(Unknown07);
            writer.Write(Unknown08);
            writer.Write(Unknown09);
            writer.Write(Unknown10);
            writer.Write(Unknown11);
            writer.Write(Unknown12);
            writer.Write(Unknown13);
            writer.Write(Unknown14);
            writer.Write(Unknown15);
            writer.Write(Unknown16);
            writer.Write(Unknown17);
            writer.Write(Unknown18);
            writer.Write(Unknown19);
            writer.Write((uint)Style);
        }
    }
}
