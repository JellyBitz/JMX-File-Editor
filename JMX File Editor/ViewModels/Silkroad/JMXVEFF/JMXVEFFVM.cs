using JMXFileEditor.Silkroad.Data.JMXVEFF;

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
            // Add base class nodes
            var _data = new EFStoredObjectVM(string.Empty, JMXFile);
            foreach (var c in _data.Childs)
                Childs.Add(c);
        }
        #endregion

        #region Public Properties
        public override object GetClassFrom(JMXStructure s, int i)
        {
            var b = (EFStoredObject)new EFStoredObjectVM(string.Empty, new EFStoredObject()).GetClassFrom(s, i++);
            // Copy base class data
            return new EFStoredEffect()
            {
                Name = b.Name,
                Controllers = b.Controllers,

                EEGlobalData = b.EEGlobalData,
                EmptyCommands0 = b.EmptyCommands0,
                EmitterCommands = b.EmitterCommands,
                EmptyCommands1 = b.EmptyCommands1,
                LifeTimeCommand = b.LifeTimeCommand,
                ProgramCommands = b.ProgramCommands,

                Byte0 = b.Byte0,
                Byte1 = b.Byte1,
                Int0 = b.Int0,
                Int1 = b.Int1,
                Int2 = b.Int2,
                Byte2 = b.Byte2,
                Int3 = b.Int3,
                Byte3 = b.Byte3,

                ViewModeCommand = b.ViewModeCommand,
                Resource = b.Resource,
                RenderModeCommand = b.RenderModeCommand,

                EmptyProgram0 = b.EmptyProgram0,
                RenderCommands = b.RenderCommands,

                Children = b.Children,
            };
        }
        #endregion
    }
}