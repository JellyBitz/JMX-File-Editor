using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Controller
{
    //ScaleGraph
    public class EFCScaleGraph : EFController
    {
        public override string Name => "ScaleGraph";

        public EEBlend<float, FloatBlend> unkFloatBlend0; //X?
        public EEBlend<float, FloatBlend> unkFloatBlend1; //Y?
        public EEBlend<float, FloatBlend> unkFloatBlend2; //Z?
        public float unkFloat0;
        public float unkFloat1;

        public EFCScaleGraph()
        {
            unkFloatBlend0 = new EEBlend<float, FloatBlend>();
            unkFloatBlend1 = new EEBlend<float, FloatBlend>();
            unkFloatBlend2 = new EEBlend<float, FloatBlend>();
        }

        public override void Deserialize(BSReader reader)
        {
            unkFloatBlend0.Deserialize(reader);
            unkFloatBlend1.Deserialize(reader);
            unkFloatBlend2.Deserialize(reader);

            unkFloat0 = reader.ReadSingle();
            unkFloat1 = reader.ReadSingle();
        }

        public override void Serialize(BSWriter writer)
        {
            writer.Serialize(unkFloatBlend0);
            writer.Serialize(unkFloatBlend1);
            writer.Serialize(unkFloatBlend2);

            writer.Write(unkFloat0);
            writer.Write(unkFloat1);
        }
    }
}