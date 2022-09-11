using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;
using JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF
{
    public class EFStoredObjectVM : JMXStructure
    {
        #region Constructor
        public EFStoredObjectVM(string Name, EFStoredObject data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Name", data.Name));

            // Controllers
            AddFormatHandler(typeof(EFController), (s, e) =>
            {
                e.Childs.Add(new EFControllerVM("[" + e.Childs.Count + "]",
                    JMXAbstract.GetTypes(
                        typeof(EFCNormalTimeLife),
                        typeof(EFCNormalTimeLoopLife),
                        typeof(EFCStaticEmit),
                        typeof(EFCProgram),
                        typeof(EFCLinkMode),
                        typeof(EFCBAN),
                        typeof(EFCViewMode),
                        typeof(EFCShape),
                        typeof(EFCScaleGraph),
                        typeof(EFCDiffuseGraph)),
                    e.Obj?.GetType(), e.Obj));
            });
            AddChildArray("Controllers", data.Controllers.ToArray(), true, true);

            // Stored Data
            Childs.Add(new EEGlobalDataVM("EEGlobalData", data.EEGlobalData));
            Childs.Add(new EEStaticProgramVM("EmptyCommands01", data.EmptyCommands0));
            Childs.Add(new EEStaticProgramVM("EmitterCommands", data.EmitterCommands));
            Childs.Add(new EEStaticProgramVM("EmptyCommands02", data.EmptyCommands1));
            Childs.Add(new EESourceNodeVM("LifeTimeCommand", data.LifeTimeCommand.Data));
            Childs.Add(new EEStaticProgramVM("ProgramCommands", data.ProgramCommands));

            Childs.Add(new JMXAttribute("UnkByte01", data.Byte0));
            Childs.Add(new JMXAttribute("UnkByte02", data.Byte1));
            Childs.Add(new JMXAttribute("UnkInt01", data.Int0));
            Childs.Add(new JMXAttribute("UnkInt02", data.Int1));
            Childs.Add(new JMXAttribute("UnkInt03", data.Int2));
            Childs.Add(new JMXAttribute("UnkByte03", data.Byte2));
            Childs.Add(new JMXAttribute("UnkInt04", data.Int3));
            Childs.Add(new JMXAttribute("UnkByte04", data.Byte3));

            Childs.Add(new EESourceNodeVM("ViewModeCommand", data.ViewModeCommand.Data));
            Childs.Add(new EEResourceVM("Resource", data.Resource));
            Childs.Add(new EESourceNodeVM("RenderModeCommand", data.RenderModeCommand.Data));
            
            AddFormatHandler(typeof(EESourceNode), (s, e) =>
            {
                e.Childs.Add(new EESourceNodeVM("[" + e.Childs.Count + "]", e.Obj is EESourceNode _obj ? _obj : new EESourceNode()));
            });
            AddChildArray("EmptyProgram01", data.EmptyProgram0.SourceData.ToArray(), true, true);
            AddChildArray("RenderCommands", data.RenderCommands.SourceData.ToArray(), true, true);

            // Children
            AddFormatHandler(typeof(EFStoredObject), (s, e) =>
            {
                e.Childs.Add(new EFStoredObjectVM("[" + e.Childs.Count + "]", e.Obj is EFStoredObject _obj ? _obj : new EFStoredObject()));
            });
            AddChildArray("Children", data.Children.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EFStoredObject()
            {
                Name = (string)((JMXAttribute)s.Childs[i++]).Value,
                
                Controllers = ((JMXStructure)s.Childs[i++]).GetChildList<EFController>(),

                EEGlobalData = (EEGlobalData)((JMXStructure)s.Childs[i++]).GetClass(),
                EmptyCommands0 = (EEStaticProgram)((JMXStructure)s.Childs[i++]).GetClass(),
                EmitterCommands = (EEStaticProgram)((JMXStructure)s.Childs[i++]).GetClass(),
                EmptyCommands1 = (EEStaticProgram)((JMXStructure)s.Childs[i++]).GetClass(),
                LifeTimeCommand = new EEStaticCommand() { Data = (EESourceNode)((JMXStructure)s.Childs[i++]).GetClass() },
                ProgramCommands = (EEStaticProgram)((JMXStructure)s.Childs[i++]).GetClass(),

                Byte0 = (byte)((JMXAttribute)s.Childs[i++]).Value,
                Byte1 = (byte)((JMXAttribute)s.Childs[i++]).Value,
                Int0 = (int)((JMXAttribute)s.Childs[i++]).Value,
                Int1 = (int)((JMXAttribute)s.Childs[i++]).Value,
                Int2 = (int)((JMXAttribute)s.Childs[i++]).Value,
                Byte2 = (byte)((JMXAttribute)s.Childs[i++]).Value,
                Int3 = (int)((JMXAttribute)s.Childs[i++]).Value,
                Byte3 = (byte)((JMXAttribute)s.Childs[i++]).Value,

                ViewModeCommand = new EEStaticCommand() { Data = (EESourceNode)((JMXStructure)s.Childs[i++]).GetClass() },
                Resource = (EEResource)((EEResourceVM)s.Childs[i++]).GetClass(),
                RenderModeCommand = new EEStaticCommand() { Data = (EESourceNode)((JMXStructure)s.Childs[i++]).GetClass() },

                EmptyProgram0 = new EEProgram() { SourceData = ((JMXStructure)s.Childs[i++]).GetChildList<EESourceNode>() },
                RenderCommands = new EEProgram() { SourceData = ((JMXStructure)s.Childs[i++]).GetChildList<EESourceNode>() },

                Children = ((JMXStructure)s.Childs[i++]).GetChildList<EFStoredObject>(),
            };
        }
        #endregion
    }
}