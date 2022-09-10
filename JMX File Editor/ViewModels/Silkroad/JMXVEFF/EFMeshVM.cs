using JMXFileEditor.Silkroad.Data.JMXVEFF;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller
{
    public class EFMeshVM : JMXStructure
    {
        #region Constructor
        public EFMeshVM(string Name, EFMesh data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Path", data.Path));
            AddChildArray("Textures", data.Textures.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EFMesh()
            {
                Path = (string)((JMXAttribute)s.Childs[i++]).Value,
                Textures = ((JMXStructure)s.Childs[i++]).GetChildList<string>(),
            };
        }
        #endregion
    }
}
