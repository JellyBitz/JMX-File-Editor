using JMXFileEditor.Silkroad.Data.JMXVDOF;
using JMXFileEditor.Silkroad.Data.JMXVENVI;
using JMXFileEditor.Silkroad.Data.JMXVRES;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;
using System;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVENVI
{
    public class JMXVENVIVM : JMXStructure
    {
        public JMXVENVIVM(JMXFileEditor.Silkroad.Data.JMXVENVI.JMXVENVI JMXFile) : base(JMXFile.Format, true)
        {
            Childs.Add(new JMXAttribute("Name", JMXFile.Name));
            // Profiles
            AddFormatHandler(typeof(EnvironmentProfile), (s, e) => {
                e.Childs.Add(new EnvironmentProfileVM("[" + e.Childs.Count + "]", e.Obj is EnvironmentProfile _obj ? _obj : new EnvironmentProfile()));
            });
            AddChildArray("Profiles", JMXFile.Profiles.ToArray(), true, true);
            // Environment nodes
            Childs.Add(new EnvironmentNodeVM("RootNode", JMXFile.RootNode));
        }
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new JMXFileEditor.Silkroad.Data.JMXVENVI.JMXVENVI()
            {
                Name = (string)((JMXAttribute)s.Childs[i++]).Value,
                Profiles = ((JMXStructure)s.Childs[i++]).GetChildList<EnvironmentProfile>(),
                RootNode = (EnvironmentNode)((EnvironmentNodeVM)s.Childs[i++]).GetClass(),
            };
        }
    }
}
