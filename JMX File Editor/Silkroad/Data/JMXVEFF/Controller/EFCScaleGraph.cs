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

        public override void Read(BSReader reader)
        {
            unkFloatBlend0.Read(reader);
            unkFloatBlend1.Read(reader);
            unkFloatBlend2.Read(reader);

            unkFloat0 = reader.ReadSingle();
            unkFloat1 = reader.ReadSingle();
        }
    }
}