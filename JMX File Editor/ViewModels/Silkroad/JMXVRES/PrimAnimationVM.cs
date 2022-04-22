using JMXFileEditor.Silkroad.Data.JMXVRES;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
    public class PrimAnimationVM : JMXStructure
    {
        #region Constructor
        public PrimAnimationVM(string Name, PrimAnimation Animation) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Path", Animation.Path));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure Structure)
        {
            PrimAnimation ani = new PrimAnimation()
            {
                Path = (string)((JMXAttribute)Structure.Childs[0]).Value,
            };
            return ani;
        }
        #endregion
    }
}