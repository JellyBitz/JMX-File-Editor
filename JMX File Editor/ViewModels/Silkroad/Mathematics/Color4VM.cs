using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.Mathematics
{
    public class Color4VM : JMXStructure
    {
        #region Constructor
        public Color4VM(string Name, Color4 Color) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Red", Color.Red));
            Childs.Add(new JMXAttribute("Green", Color.Green));
            Childs.Add(new JMXAttribute("Blue", Color.Blue));
            Childs.Add(new JMXAttribute("Alpha", Color.Alpha));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure Structure)
        {
            return new Color4()
            {
                Red = (float)((JMXAttribute)Structure.Childs[0]).Value,
                Green = (float)((JMXAttribute)Structure.Childs[1]).Value,
                Blue = (float)((JMXAttribute)Structure.Childs[2]).Value,
                Alpha = (float)((JMXAttribute)Structure.Childs[3]).Value
            };
        }
        #endregion
    }
}