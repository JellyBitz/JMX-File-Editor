using JMXFileEditor.Silkroad.Data.Common;

namespace JMXFileEditor.ViewModels.Silkroad.Common
{
    public class ObjectGeneralInfoVM : JMXStructure
    {
        #region Constructor
        public ObjectGeneralInfoVM(string Name, ObjectGeneralInfo ObjectInfo) : base(Name, true)
        {
            // create nodes
            Childs.Add(new JMXOption("Type", ObjectInfo.Type, JMXOption.GetValues<object>(typeof(ObjectGeneralType))));
            Childs.Add(new JMXAttribute("Name", ObjectInfo.Name));
            Childs.Add(new JMXAttribute("Int01", ObjectInfo.Int01));
            Childs.Add(new JMXAttribute("Int02", ObjectInfo.Int02));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure Structure)
        {
            return new ObjectGeneralInfo()
            {
                Type = (ObjectGeneralType)((JMXOption)Structure.Childs[0]).Value,
                Name = (string)((JMXAttribute)Structure.Childs[1]).Value,
                Int01 = (int)((JMXAttribute)Structure.Childs[2]).Value,
                Int02 = (int)((JMXAttribute)Structure.Childs[3]).Value
            };
        }
        #endregion
    }
}