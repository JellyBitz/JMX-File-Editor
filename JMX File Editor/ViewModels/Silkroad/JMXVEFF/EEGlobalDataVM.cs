using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF
{
    public class EEGlobalDataVM : JMXStructure
    {
        #region Constructor
        public EEGlobalDataVM(string Name, EEGlobalData data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("UnkInt01", data.Int0));

            AddFormatHandler(typeof(IEEParameter), (s, e) =>
            {
                // Set default case on a new one
                IEEParameter obj = e.Obj == null ? new ParameterEFStaticEmit() : (IEEParameter)e.Obj;

                e.Childs.Add(new EEParameterVM("[" + e.Childs.Count + "]",
                JMXAbstract.GetTypes(
                    typeof(ParameterFloat),
                    typeof(ParameterVector3),
                    typeof(ParameterMatrix),
                    typeof(ParameterEFStaticEmit),
                    typeof(ParameterAxisVector4),
                    typeof(ParameterRotVector),
                    typeof(ParameterAngleVector1),
                    typeof(ParameterFrameScale),
                    typeof(ParameterBlendScaleGraph),
                    typeof(ParameterBlendScaleGraphPointer),
                    typeof(ParameterFrameDiffuse),
                    typeof(ParameterBlendDiffuseGraph),
                    typeof(ParameterFrameBANRotation),
                    typeof(ParameterFrameBANPosition),
                    typeof(ParameterBSAnimation),
                    typeof(ParameterFrameTextureSlide)),
                obj.GetType(),
                obj));
            });
            AddChildArray("Parameters", data.Parameters.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EEGlobalData()
            {
                Int0 = (int)((JMXAttribute)s.Childs[i++]).Value,
                Parameters = ((JMXStructure)s.Childs[i++]).GetChildList<IEEParameter>(),
            };
        }
        #endregion
    }
}
