using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.Mathematics
{
    /// <summary>
    /// ViewModel representing <see cref="Vector3"/>
    /// </summary>
    public class Vector3VM : JMXStructure
    {
        #region Constructor
        public Vector3VM(string Name, Vector3 Vector) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("X", Vector.X));
            Childs.Add(new JMXAttribute("Y", Vector.Y));
            Childs.Add(new JMXAttribute("Z", Vector.Z));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new Vector3
            {
                X = (float)((JMXAttribute)s.Childs[i++]).Value,
                Y = (float)((JMXAttribute)s.Childs[i++]).Value,
                Z = (float)((JMXAttribute)s.Childs[i++]).Value
            };
        }
        #endregion
    }
}