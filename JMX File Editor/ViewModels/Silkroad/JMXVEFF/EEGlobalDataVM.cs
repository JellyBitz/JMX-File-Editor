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
                e.Childs.Add(new EEParameterVM("[" + e.Childs.Count + "]",
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
                    typeof(ParameterFrameTextureSlide),
                    typeof(ParameterBlendScaleGraph),
                    typeof(ParameterBlendDiffuseGraph),
                    typeof(ParameterBSAnimation)),
                e.Obj?.GetType(),
                e.Obj));
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
