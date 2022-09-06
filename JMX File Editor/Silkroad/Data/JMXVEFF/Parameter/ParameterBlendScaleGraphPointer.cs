using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterBlendScaleGraphPointer : EEParameter<float>
    {
        public override string Name => "BlendScaleGraphPointer";

        public override void Read(BSReader reader)
        {
            this.Value = reader.ReadSingle();
        }

        public override void Write(BSWriter writer)
        {
            writer.Write(this.Value);
        }
    }
}