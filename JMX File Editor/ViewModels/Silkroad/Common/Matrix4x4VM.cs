using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.Common
{
    /// <summary>
    /// ViewModel representing a Matrix on 3D space
    /// </summary>

    public class Matrix4x4VM : JMXStructure
    {
        #region Constructor
        public Matrix4x4VM(string Name, Matrix4x4 Matrix) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("M11", Matrix.M11));
            Childs.Add(new JMXAttribute("M12", Matrix.M12));
            Childs.Add(new JMXAttribute("M13", Matrix.M13));
            Childs.Add(new JMXAttribute("M14", Matrix.M14));

            Childs.Add(new JMXAttribute("M21", Matrix.M21));
            Childs.Add(new JMXAttribute("M22", Matrix.M22));
            Childs.Add(new JMXAttribute("M23", Matrix.M23));
            Childs.Add(new JMXAttribute("M24", Matrix.M24));

            Childs.Add(new JMXAttribute("M31", Matrix.M31));
            Childs.Add(new JMXAttribute("M32", Matrix.M32));
            Childs.Add(new JMXAttribute("M33", Matrix.M33));
            Childs.Add(new JMXAttribute("M34", Matrix.M34));

            Childs.Add(new JMXAttribute("M41", Matrix.M41));
            Childs.Add(new JMXAttribute("M42", Matrix.M42));
            Childs.Add(new JMXAttribute("M43", Matrix.M43));
            Childs.Add(new JMXAttribute("M44", Matrix.M44));
        }
        #endregion Constructor

        #region Public Methods
        public override object GetClassFrom(JMXStructure Structure)
        {
            return new Matrix4x4
            (
                        (float)((JMXAttribute)Structure.Childs[0]).Value,
                        (float)((JMXAttribute)Structure.Childs[1]).Value,
                        (float)((JMXAttribute)Structure.Childs[2]).Value,
                        (float)((JMXAttribute)Structure.Childs[3]).Value,

                        (float)((JMXAttribute)Structure.Childs[4]).Value,
                        (float)((JMXAttribute)Structure.Childs[5]).Value,
                        (float)((JMXAttribute)Structure.Childs[6]).Value,
                        (float)((JMXAttribute)Structure.Childs[7]).Value,

                        (float)((JMXAttribute)Structure.Childs[8]).Value,
                        (float)((JMXAttribute)Structure.Childs[9]).Value,
                        (float)((JMXAttribute)Structure.Childs[10]).Value,
                        (float)((JMXAttribute)Structure.Childs[11]).Value,

                        (float)((JMXAttribute)Structure.Childs[12]).Value,
                        (float)((JMXAttribute)Structure.Childs[13]).Value,
                        (float)((JMXAttribute)Structure.Childs[14]).Value,
                        (float)((JMXAttribute)Structure.Childs[15]).Value
            );
        }
        #endregion Public Methods
    }
}