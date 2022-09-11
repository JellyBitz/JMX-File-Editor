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
            Childs.Add(new EEDiffuseBlendVM("DiffuseBlend", data.DiffuseBlend));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EFCDiffuseGraph()
            {
                ByteBlend = (EEBlend<byte, ByteBlend>)((EEByteBlendVM)s.Childs[i++]).GetClass(),
                DiffuseBlend = (EEBlend<Color32, DiffuseBlend>)((EEDiffuseBlendVM)s.Childs[i++]).GetClass(),
            };
        }
        #endregion
    }
}