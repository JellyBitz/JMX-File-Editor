using JMXFileEditor.Silkroad.Data.JMXVEFF;
using System.Windows.Markup;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF
{
    /// <summary>
    /// ViewModel representing the <see cref="EFStoredEffect"/> file format
    /// </summary>
    public class JMXVEFFVM : JMXStructure
    {
        #region Constructor
        public JMXVEFFVM(EFStoredEffect JMXFile) : base(JMXFile.Format, true)
        {
            Childs.Add(new JMXAttribute("Scale", JMXFile.Scale));
            // Global Position Probably?
            Childs.Add(new JMXAttribute("UnkInt01", JMXFile.Version13Value0));
            Childs.Add(new JMXAttribute("UnkInt02", JMXFile.Version13Value1));
            Childs.Add(new JMXAttribute("UnkInt03", JMXFile.Version13Value2));
            Childs.Add(new EFStoredObjectVM("Root", JMXFile));
        }
        #endregion

        #region Public Properties
        public override object GetClassFrom(JMXStructure s, int i)
        {
            var scale = (float)((JMXAttribute)s.Childs[i++]).Value;
            var unk01 = (int)((JMXAttribute)s.Childs[i++]).Value;
            var unk02 = (int)((JMXAttribute)s.Childs[i++]).Value;
            var unk03 = (int)((JMXAttribute)s.Childs[i++]).Value;

            var root = (EFStoredObject)((EFStoredObjectVM)s.Childs[i++]).GetClass();
            // Copy base class data
            return new EFStoredEffect()
            {
                Scale = scale,

                Version13Value0 = unk01,
                Version13Value1 = unk02,
                Version13Value2 = unk03,

                Name = root.Name,
                Controllers = root.Controllers,

                EEGlobalData = root.EEGlobalData,
                EmptyCommands0 = root.EmptyCommands0,
                EmitterCommands = root.EmitterCommands,
                EmptyCommands1 = root.EmptyCommands1,
                LifeTimeCommand = root.LifeTimeCommand,
                ProgramCommands = root.ProgramCommands,

                Byte0 = root.Byte0,
                Byte1 = root.Byte1,
                Int0 = root.Int0,
                Int1 = root.Int1,
                Int2 = root.Int2,
                Byte2 = root.Byte2,
                Int3 = root.Int3,
                Byte3 = root.Byte3,

                ViewModeCommand = root.ViewModeCommand,
                Resource = root.Resource,
                RenderModeCommand = root.RenderModeCommand,

                EmptyProgram0 = root.EmptyProgram0,
                RenderCommands = root.RenderCommands,

                Children = root.Children,
            };
        }
        #endregion
    }
}