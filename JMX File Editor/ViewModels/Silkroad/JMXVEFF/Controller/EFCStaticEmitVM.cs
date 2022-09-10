using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller
{
    public class EFCStaticEmitVM : JMXStructure
    {
        #region Constructor
        public EFCStaticEmitVM(string Name, EFCStaticEmit data) : base(Name, true)
        {
            // Mimic all nodes
            var _data = new EFStaticEmitVM(string.Empty, data.StaticEmit);
            foreach (var c in _data.Childs)
                Childs.Add(c);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EFCStaticEmit()
            {
                StaticEmit = (EFStaticEmit)new EFStaticEmitVM(string.Empty, new EFStaticEmit()).GetClassFrom(s, i)
            };
        }
        #endregion
    }
}