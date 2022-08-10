using JMXFileEditor.Silkroad.Data.JMXVDOF;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVDOF
{
    public class BlockLightVM : JMXStructure
    {
        #region Constructor
        public BlockLightVM(string Name, BlockLight BlockLight) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Name", BlockLight.Name));

            Childs.Add(new Vector3VM("Position", BlockLight.Position));
            Childs.Add(new Color3VM("Color01", BlockLight.Color01));
            Childs.Add(new Color3VM("Color02", BlockLight.Color02));
            Childs.Add(new Color3VM("Color03", BlockLight.Color03));

            Childs.Add(new JMXAttribute("UnkFloat01", BlockLight.UnkFloat01));
            Childs.Add(new JMXAttribute("UnkFloat02", BlockLight.UnkFloat02));
            Childs.Add(new JMXAttribute("UnkFloat03", BlockLight.UnkFloat03));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new BlockLight
            {
                Name = (string)((JMXAttribute)s.Childs[i++]).Value,

                Position = (Vector3)((Vector3VM)s.Childs[i++]).GetClass(),
                Color01 = (Color3)((Color3VM)s.Childs[i++]).GetClass(),
                Color02 = (Color3)((Color3VM)s.Childs[i++]).GetClass(),
                Color03 = (Color3)((Color3VM)s.Childs[i++]).GetClass(),

                UnkFloat01 = (float)((JMXAttribute)s.Childs[i++]).Value,
                UnkFloat02 = (float)((JMXAttribute)s.Childs[i++]).Value,
                UnkFloat03 = (float)((JMXAttribute)s.Childs[i++]).Value,
            };
        }
        #endregion
    }
}