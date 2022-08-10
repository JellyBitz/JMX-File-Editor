using JMXFileEditor.Silkroad.Data.JMXVDOF;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVDOF
{
    public class BlockObjectVM : JMXStructure
    {
        #region Constructor
        public BlockObjectVM(string Name, BlockObject BlockObject) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Path", BlockObject.Path));
            Childs.Add(new JMXAttribute("Name", BlockObject.Name));

            Childs.Add(new Vector3VM("Position", BlockObject.Position));
            Childs.Add(new Vector3VM("Rotation", BlockObject.Rotation));
            Childs.Add(new Vector3VM("Scale", BlockObject.Scale));

            Childs.Add(new JMXAttribute("Flag", BlockObject.Flag));
            Childs.Add(new JMXAttribute("UnkUInt01", BlockObject.UnkUInt01));
            Childs.Add(new JMXAttribute("RadiusSqrt", BlockObject.RadiusSqrt));
            Childs.Add(new JMXAttribute("WaterColor", BlockObject.WaterColor));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new BlockObject
            {
                Path = (string)((JMXAttribute)s.Childs[i++]).Value,
                Name = (string)((JMXAttribute)s.Childs[i++]).Value,

                Position = (Vector3)((Vector3VM)s.Childs[i++]).GetClass(),
                Rotation = (Vector3)((Vector3VM)s.Childs[i++]).GetClass(),
                Scale = (Vector3)((Vector3VM)s.Childs[i++]).GetClass(),

                Flag = (uint)((JMXAttribute)s.Childs[i++]).Value,
                UnkUInt01 = (uint)((JMXAttribute)s.Childs[i++]).Value,
                RadiusSqrt = (float)((JMXAttribute)s.Childs[i++]).Value,
                WaterColor = (uint)((JMXAttribute)s.Childs[i++]).Value,
            };
        }
        #endregion
    }
}