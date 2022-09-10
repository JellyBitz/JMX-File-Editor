using JMXFileEditor.Silkroad.Data.JMXVEFF;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller
{
    public class EFStaticEmitVM : JMXStructure
    {
        #region Constructor
        public EFStaticEmitVM(string Name, EFStaticEmit data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Min", data.Min));
            Childs.Add(new JMXAttribute("Max", data.Max));
            Childs.Add(new JMXAttribute("BurstRate", data.BurstRate));
            Childs.Add(new JMXAttribute("MinParticles", data.MinParticles));
            Childs.Add(new JMXAttribute("SpawnRate", data.SpawnRate));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EFStaticEmit()
            {
                Min = (uint)((JMXAttribute)s.Childs[i++]).Value,
                Max = (uint)((JMXAttribute)s.Childs[i++]).Value,
                BurstRate = (uint)((JMXAttribute)s.Childs[i++]).Value,
                MinParticles = (uint)((JMXAttribute)s.Childs[i++]).Value,
                SpawnRate = (float)((JMXAttribute)s.Childs[i++]).Value,
            };
        }
        #endregion
    }
}
