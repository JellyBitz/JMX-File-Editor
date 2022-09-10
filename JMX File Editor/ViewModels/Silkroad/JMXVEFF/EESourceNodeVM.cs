using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF
{
    public class EESourceNodeVM : JMXStructure
    {
        #region Constructor
        public EESourceNodeVM(string Name, EESourceNode data) : base(Name, true)
        {
            var name = new JMXAttribute("Name", data.Name);
            Childs.Add(name);
            Childs.Add(new JMXOption("Type", data.Type, JMXOption.GetValues<object>(typeof(SourceNodeType))));
            Childs.Add(new JMXAttribute("UnkByte01", data.Byte1));
            Childs.Add(new JMXAttribute("Start", data.Start));
            Childs.Add(new JMXAttribute("End", data.End));
            Childs.Add(new JMXAttribute("UnkFloat01", data.Float2));
            var parameterNode = new EEParameterVM("Parameter",
                JMXAbstract.GetTypes(
                    typeof(ParameterEFStaticEmit),
                    typeof(ParameterFloat),
                    typeof(ParameterVector3),
                    typeof(ParameterMatrix),
                    typeof(ParameterRotVector),
                    typeof(ParameterAxisVector4),
                    typeof(ParameterAngleVector1),
                    typeof(ParameterFrameScale),
                    typeof(ParameterBlendScaleGraphPointer),
                    typeof(ParameterFrameDiffuse),
                    typeof(ParameterFrameBANRotation),
                    typeof(ParameterFrameBANPosition),
                    typeof(ParameterFrameTextureSlide)),
                data.Parameter?.GetType(),
                data.Parameter,
                false);
            Childs.Add(parameterNode);
            // Update node depending on name
            name.PropertyChanged += (_s, _e) =>
            {
                var newParameter = ParameterFactory.CreateParameterByCommandName((string)name.Value);
                parameterNode.SetCurrentType(newParameter?.GetType(), newParameter);
            };
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EESourceNode()
            {
                HasData = true,
                Name = (string)((JMXAttribute)s.Childs[i++]).Value,
                Type = (SourceNodeType)((JMXOption)s.Childs[i++]).Value,
                Byte1 = (byte)((JMXAttribute)s.Childs[i++]).Value,
                Start = (float)((JMXAttribute)s.Childs[i++]).Value,
                End = (float)((JMXAttribute)s.Childs[i++]).Value,
                Float2 = (float)((JMXAttribute)s.Childs[i++]).Value,
                Parameter = (IEEParameter)((EEParameterVM)s.Childs[i++]).GetClass(),
            };
        }
        #endregion
    }
}
