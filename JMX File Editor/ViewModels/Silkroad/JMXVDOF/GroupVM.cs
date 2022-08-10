using JMXFileEditor.Silkroad.Data.JMXVDOF;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVDOF
{
    public class GroupVM : JMXStructure
    {
        #region Constructor
        public GroupVM(string Name, Group Group) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Name", Group.Name));
            Childs.Add(new JMXAttribute("Flag", Group.Flag));
            AddChildArray("BlockIndices", Group.BlockIndices.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new Group()
            {
                Name = (string)((JMXAttribute)s.Childs[i++]).Value,
                Flag = (uint)((JMXAttribute)s.Childs[i++]).Value,
                BlockIndices = ((JMXStructure)s.Childs[i++]).GetChildList<uint>(),
            };
        }
        #endregion
    }
}