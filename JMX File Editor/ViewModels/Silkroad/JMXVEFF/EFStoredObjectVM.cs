using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;
using JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF
{
    public class EFStoredObjectVM : JMXStructure
    {
        #region Constructor
        public EFStoredObjectVM(string Name, EFStoredObject obj, bool IsEditable = true) : base(Name, IsEditable)
        {
            Childs.Add(new JMXAttribute("Name", obj.Name));

            // Controllers
            AddFormatHandler(typeof(EFController), (s, e) =>
            {
                e.Childs.Add(new EFControllerVM("[" + e.Childs.Count + "]",
                    JMXAbstract.GetTypes(
                        typeof(EFCNormalTimeLife),
                        typeof(EFCNormalTimeLoopLife),
                        typeof(EFCStaticEmit),
                        typeof(EFCProgram),
                        typeof(EFCLinkMode),
                        typeof(EFCBAN),
                        typeof(EFCViewMode),
                        typeof(EFCShape),
                        typeof(EFCScaleGraph),
                        typeof(EFCDiffuseGraph)),
                    e.Obj?.GetType(), e.Obj));
            });
            AddChildArray("Controllers", obj.Controllers.ToArray(), true, true);

            // TO BE CONTINUED ...
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EFStoredObject()
            {
                Name = (string)((JMXAttribute)s.Childs[i++]).Value,
                Controllers = ((JMXStructure)s.Childs[i++]).GetChildList<EFController>(),

                // TO BE CONTINUED...
            };
        }
        #endregion
    }
}