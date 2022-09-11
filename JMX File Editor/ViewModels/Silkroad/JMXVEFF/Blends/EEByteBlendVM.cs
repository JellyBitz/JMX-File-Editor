using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using System.Linq;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Blends
{
    public class EEByteBlendVM : JMXStructure
    {
        #region Constructor
        public EEByteBlendVM(string Name, EEBlend<byte, ByteBlend> data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Begin", data.Begin));
            Childs.Add(new JMXAttribute("End", data.End));

            AddFormatHandler(typeof(ByteBlend), (s, e) => {
                e.Childs.Add(new ByteBlendVM("[" + e.Childs.Count + "]", e.Obj is ByteBlend _obj ? _obj : new ByteBlend()));
            });
            AddChildArray("Points", data.Cast<ByteBlend>().ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EEBlend<byte, ByteBlend>(((JMXStructure)s.Childs[i+2]).GetChildList<ByteBlend>())
            {
                Begin = (float)((JMXAttribute)s.Childs[i++]).Value,
                End = (float)((JMXAttribute)s.Childs[i++]).Value
            };
        }
        #endregion
    }
}
