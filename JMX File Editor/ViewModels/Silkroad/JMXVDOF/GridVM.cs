using JMXFileEditor.Silkroad.Data.JMXVDOF;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVDOF
{
    public class GridVM : JMXStructure
    {
        #region Constructor
        public GridVM(string Name, Grid Grid) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Width", Grid.Width));
            Childs.Add(new JMXAttribute("Height", Grid.Height));
            Childs.Add(new JMXAttribute("Length", Grid.Length));

            AddFormatHandler(typeof(GridBucket), (s, e) => {
                e.Childs.Add(new GridBucketVM("[" + e.Childs.Count + "]", e.Obj is GridBucket _obj ? _obj : new GridBucket()));
            });
            AddChildArray("BucketList", Grid.BucketList.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new Grid()
            {
                Width = (uint)((JMXAttribute)s.Childs[i++]).Value,
                Height = (uint)((JMXAttribute)s.Childs[i++]).Value,
                Length = (uint)((JMXAttribute)s.Childs[i++]).Value,

                BucketList = ((JMXStructure)s.Childs[i++]).GetChildList<GridBucket>(),
            };
        }
        #endregion
    }
}