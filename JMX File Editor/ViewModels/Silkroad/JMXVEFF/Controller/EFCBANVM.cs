using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller
{
    public class EFCBANVM : JMXStructure
    {
        #region Constructor
        public EFCBANVM(string Name, EFCBAN data) : base(Name, true)
        {
            AddChildArray("Animations", data.Animations.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EFCBAN()
            {
                Animations = ((JMXStructure)s.Childs[i++]).GetChildList<string>(),
            };
        }
        #endregion
    }
}