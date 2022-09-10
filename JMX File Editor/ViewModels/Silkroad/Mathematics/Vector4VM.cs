using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.Mathematics
{
    /// <summary>
    /// ViewModel representing <see cref="Vector4"/>
    /// </summary>
    public class Vector4VM : JMXStructure
    {
        #region Constructor
        public Vector4VM(string Name, Vector4 Vector) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("X", Vector.X));
            Childs.Add(new JMXAttribute("Y", Vector.Y));
            Childs.Add(new JMXAttribute("Z", Vector.Z));
            Childs.Add(new JMXAttribute("W", Vector.W));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new Vector4
            {
                X = (float)((JMXAttribute)s.Childs[i++]).Value,
                Y = (float)((JMXAttribute)s.Childs[i++]).Value,
                Z = (float)((JMXAttribute)s.Childs[i++]).Value,
                W = (float)((JMXAttribute)s.Childs[i++]).Value
            };
        }
        #endregion
    }
}