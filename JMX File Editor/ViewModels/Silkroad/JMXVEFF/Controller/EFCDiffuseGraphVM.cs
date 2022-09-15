using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Blends;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller
{
    public class EFCDiffuseGraphVM : JMXStructure
    {
        #region Constructor
        public EFCDiffuseGraphVM(string Name, EFCDiffuseGraph data) : base(Name, true)
        {
            Childs.Add(new EEByteBlendVM("ByteBlend", data.ByteBlend));
            Childs.Add(new GradientColorPickerVM("DiffuseBlend", data.DiffuseBlend.Begin, data.DiffuseBlend.End, data.DiffuseBlend));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            var gradientColor = (GradientColorPickerVM)s.Childs[i + 1];
            // Copy data
            var diffuseBlend = new EEBlend<Color32, DiffuseBlend>();
            diffuseBlend.Begin = gradientColor.Begin;
            diffuseBlend.End = gradientColor.End;
            foreach (var v in gradientColor.GradientValues)
                diffuseBlend.Points.Add(new DiffuseBlend() { Value = new Color32(v.Color.R, v.Color.G, v.Color.B, v.Color.A), Time = (float)v.Offset });
            // build result
            return new EFCDiffuseGraph()
            {
                ByteBlend = (EEBlend<byte, ByteBlend>)((EEByteBlendVM)s.Childs[i]).GetClass(),
                DiffuseBlend = diffuseBlend,
            };
        }
        #endregion
    }
}