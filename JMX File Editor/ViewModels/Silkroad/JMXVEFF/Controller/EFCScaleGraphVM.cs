using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;
using JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Blends;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller
{
    public class EFCScaleGraphVM : JMXStructure
    {
        #region Constructor
        public EFCScaleGraphVM(string Name, EFCScaleGraph data) : base(Name, true)
        {
            Childs.Add(new EEFloatBlendVM("UnkFloatBlend01", data.unkFloatBlend0));
            Childs.Add(new EEFloatBlendVM("UnkFloatBlend02", data.unkFloatBlend1));
            Childs.Add(new EEFloatBlendVM("UnkFloatBlend03", data.unkFloatBlend2));

            Childs.Add(new JMXAttribute("UnkFloat01", data.unkFloat0));
            Childs.Add(new JMXAttribute("UnkFloat02", data.unkFloat1));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EFCScaleGraph()
            {
                unkFloatBlend0 = (EEBlend<float, FloatBlend>)((EEFloatBlendVM)s.Childs[i++]).GetClass(),
                unkFloatBlend1 = (EEBlend<float, FloatBlend>)((EEFloatBlendVM)s.Childs[i++]).GetClass(),
                unkFloatBlend2 = (EEBlend<float, FloatBlend>)((EEFloatBlendVM)s.Childs[i++]).GetClass(),

                unkFloat0 = (float)((JMXAttribute)s.Childs[i++]).Value,
                unkFloat1 = (float)((JMXAttribute)s.Childs[i++]).Value,
            };
        }
        #endregion
    }
}