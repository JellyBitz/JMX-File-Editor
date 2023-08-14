using JMXFileEditor.Silkroad.Data.JMXVENVI;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVENVI
{
    public class EnvironmentNodeVM : JMXStructure
    {
        public EnvironmentNodeVM(string Name, EnvironmentNode data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Name", data.Name));
            Childs.Add(new JMXAttribute("ProfileId", data.ProfileId));
            Childs.Add(new JMXAttribute("Short01", data.Short0));
            Childs.Add(new JMXAttribute("Int01", data.Int0));
            Childs.Add(new JMXAttribute("Int02", data.Int1));
            // Children
            AddFormatHandler(typeof(EnvironmentNode), (s, e) => {
                e.Childs.Add(new EnvironmentNodeVM("[" + e.Childs.Count + "]", e.Obj is EnvironmentNode _obj ? _obj : new EnvironmentNode()));
            });
            AddChildArray("Children", data.Children.ToArray(), true, true);
        }
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EnvironmentNode()
            {
                Name = (string)((JMXAttribute)s.Childs[i++]).Value,
                ProfileId = (short)((JMXAttribute)s.Childs[i++]).Value,
                Short0 = (short)((JMXAttribute)s.Childs[i++]).Value,
                Int0 = (int)((JMXAttribute)s.Childs[i++]).Value,
                Int1 = (int)((JMXAttribute)s.Childs[i++]).Value,
                Children = ((JMXStructure)s.Childs[i++]).GetChildList<EnvironmentNode>()
            };
        }
    }
}
