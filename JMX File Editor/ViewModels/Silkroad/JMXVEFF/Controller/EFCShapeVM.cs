using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller
{
    public class EFCShapeVM : JMXStructure
    {
        #region Constructor
        public EFCShapeVM(string Name, EFCShape data) : base(Name, true)
        {
            Childs.Add(new JMXOption("Shape", data.Shape, JMXOption.GetValues<object>(typeof(RenderShape))));
            Childs.Add(new EEResourceVM("Resource", data.Resource));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EFCShape()
            {
                Shape = (RenderShape)((JMXOption)s.Childs[i++]).Value,
                Resource = (EEResource)((EEResourceVM)s.Childs[i++]).GetClass(),
            };
        }
        #endregion
    }
}