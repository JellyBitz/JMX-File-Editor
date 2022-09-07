using JMXFileEditor.Silkroad.IO;

using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Controller
{
    //BAN
    public class EFCBAN : EFController
    {
        public override string Name => "BAN";

        public List<string> Animations { get; } = new List<string>();

        public override void Deserialize(BSReader reader)
        {
            var count = reader.ReadInt32();
            for (var i = 0; i < count; i++)
                Animations.Add(reader.ReadString());
        }

        public override void Serialize(BSWriter writer)
        {
            writer.Write(Animations.Count);
            foreach (var animation in Animations)
                writer.Write(animation);
        }
    }
}