using JMXFileEditor.Silkroad.Data.JMXVEFF;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF
{
    public class EEStaticProgramVM : JMXStructure
    {
        #region Constructor
        public EEStaticProgramVM(string Name, EEStaticProgram data) : base(Name, true)
        {
            AddFormatHandler(typeof(EESourceNode), (s, e) =>
            {
                e.Childs.Add(new EESourceNodeVM("[" + e.Childs.Count + "]", e.Obj is EESourceNode _obj ? _obj : new EESourceNode()));
            });
            AddChildArray("SourceData", data.SourceData.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EEStaticProgram()
            {
                SourceData = ((JMXStructure)s.Childs[i++]).GetChildList<EESourceNode>()
            };
        }
        #endregion
    }
}
