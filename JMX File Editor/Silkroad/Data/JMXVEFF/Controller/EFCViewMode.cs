using JMXFileEditor.Silkroad.IO;
using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Controller
{
    public class EFCViewMode : EFController
    {

        public override string Name => "ViewMode";

        public ViewMode ViewMode;

        public override void Deserialize(BSReader reader)
        {
            var name = reader.ReadString();
            if (!name.StartsWith("View") || !Enum.TryParse(name.Substring(4), out ViewMode shape))
                throw new Exception($"Undefined {nameof(ViewMode)}: {name}");

            ViewMode = shape;
        }

        public override void Serialize(BSWriter writer) => writer.Write($"View{ViewMode}");
    }
}