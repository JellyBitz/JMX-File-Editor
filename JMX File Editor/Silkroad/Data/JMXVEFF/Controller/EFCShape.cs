using JMXFileEditor.Silkroad.IO;

using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Controller
{
    //Shape
    public class EFCShape : EFController
    {
        public override string Name => "Shape";

        public RenderShape Shape { get; set; }
        public EEResource Resource { get; } = new EEResource();

        public override void Deserialize(BSReader reader)
        {
            var name = reader.ReadString();

            if (!name.StartsWith("Render") || !Enum.TryParse(name.Substring(6), out RenderShape shape))
                throw new Exception($"Undefined {nameof(RenderShape)}: {name}");

            Shape = shape;
            Resource.Deserialize(reader);
        }

        public override void Serialize(BSWriter writer)
        {
            writer.Write($"Render{Shape}");
            writer.Serialize(Resource);
        }
    }
}