using JMXFileEditor.Silkroad.Data.JMXVDOF;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVDOF
{
    public class BlockFogParamVM : JMXStructure
    {
        #region Constructor
        public BlockFogParamVM(string Name, BlockFogParam FogParameters) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Color", FogParameters.Color));
            Childs.Add(new JMXAttribute("NearPlane", FogParameters.NearPlane));
            Childs.Add(new JMXAttribute("FarPlane", FogParameters.FarPlane));
            Childs.Add(new JMXAttribute("Intensity", FogParameters.Intensity));
            Childs.Add(new JMXAttribute("HasHeightFog", FogParameters.HasHeightFog));
            Childs.Add(new JMXAttribute("UnkFloat01", FogParameters.UnkFloat01));
            Childs.Add(new JMXAttribute("UnkFloat02", FogParameters.UnkFloat02));
            Childs.Add(new JMXAttribute("UnkFloat03", FogParameters.UnkFloat03));
            Childs.Add(new JMXAttribute("UnkFloat04", FogParameters.UnkFloat04));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new BlockFogParam
            {
                Color = (uint)((JMXAttribute)s.Childs[i++]).Value,
                NearPlane = (float)((JMXAttribute)s.Childs[i++]).Value,
                FarPlane = (float)((JMXAttribute)s.Childs[i++]).Value,
                Intensity = (float)((JMXAttribute)s.Childs[i++]).Value,
                HasHeightFog = (byte)((JMXAttribute)s.Childs[i++]).Value,
                UnkFloat01 = (float)((JMXAttribute)s.Childs[i++]).Value,
                UnkFloat02 = (float)((JMXAttribute)s.Childs[i++]).Value,
                UnkFloat03 = (float)((JMXAttribute)s.Childs[i++]).Value,
                UnkFloat04 = (float)((JMXAttribute)s.Childs[i++]).Value,
            };
        }
        #endregion
    }
}