using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Blends;
using System.Security.Cryptography;

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
            return new EFCDiffuseGraph()
            {
                ByteBlend = (EEBlend<byte, ByteBlend>)((EEByteBlendVM)s.Childs[i++]).GetClass(),
                DiffuseBlend = ((GradientColorPickerVM)s.Childs[i++]).GetDiffuseBlend(),
            };
        }
        #endregion
    }
}