using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller
{
    public class EFCViewModeVM : JMXStructure
    {
        #region Constructor
        public EFCViewModeVM(string Name, EFCViewMode data) : base(Name, true)
        {
            Childs.Add(new JMXOption("ViewMode", data.ViewMode, JMXOption.GetValues<object>(typeof(ViewMode))));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EFCViewMode()
            {
                ViewMode = (ViewMode)((JMXOption)s.Childs[i++]).Value
            };
        }
        #endregion
    }
}