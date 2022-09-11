using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVENVI
{
    class EnvironmentColorBlend : EnvironmentDefaultBlend<Color3>
    {
        #region Abstract Implementation
        public override void Deserialize(BSReader reader)
        {
            Value = reader.ReadColor3();
            Time = reader.ReadFloat();
        }
        public override void Serialize(BSWriter writer)
        {
            writer.Write(Value);
            writer.Write(Time);
        }
        #endregion
    }
}
