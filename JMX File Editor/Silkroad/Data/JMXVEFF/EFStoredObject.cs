using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;
using JMXFileEditor.Silkroad.IO;

using System.Collections.Generic;
using System.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public class EFStoredObject
    {
        private static bool _readHeader = true;

        public EFStoredObject(EFStoredObject parent = null)
        {
            this.Parent = parent;

            this.EEGlobalData = new EEGlobalData();
            this.EmptyCommands0 = new EEStaticProgram();
            this.EmitterCommands = new EEStaticProgram();
            this.EmptyCommands1 = new EEStaticProgram();
            this.ProgramCommands = new EEStaticProgram();

            this.LifeTimeCommand = new EEStaticCommand();
            this.ViewModeCommand = new EEStaticCommand();
            this.RenderCommand = new EEStaticCommand();

            this.EmptyProgram0 = new EEProgram();
            this.RenderCommands = new EEProgram();

            this.Resource = new EEResource();
        }

        public EFStoredObject Parent { get; set; }
        public List<EFStoredObject> Children { get; } = new List<EFStoredObject>();

        public string Name { get; private set; } = "<None>";

        public List<EFController> Controllers { get; } = new List<EFController>();

        public EEGlobalData EEGlobalData { get; }
        public EEStaticProgram EmptyCommands0 { get; } //Empty?
        public EEStaticProgram EmitterCommands { get; } //Emitters?
        public EEStaticProgram EmptyCommands1 { get; } //Empty?
        public EEStaticProgram ProgramCommands { get; } // ProgamUpdate
        public EEStaticCommand LifeTimeCommand { get; } //LifeTime
        public EEStaticCommand ViewModeCommand { get; } //ViewMode
        public EEStaticCommand RenderCommand { get; } //RenderMode
        public EEProgram EmptyProgram0 { get; } //Empty?
        public EEProgram RenderCommands { get; } //RenderCommands?
        public EEResource Resource { get; set; }

        public byte Byte0 { get; private set; }
        public byte Byte1 { get; private set; }
        public int Int0 { get; private set; }
        public int Int1 { get; private set; }
        public int Int2 { get; private set; }
        public byte Byte2 { get; private set; }
        public int Int3 { get; private set; }
        public byte Byte3 { get; private set; }

        public virtual void Read(BSReader reader)
        {
            var dataOffset = reader.ReadInt32();
            if (_readHeader)
            {
                this.Name = reader.ReadString();
                this.ReadControllers(reader);
            }
            else
            {
                reader.BaseStream.Seek(dataOffset, SeekOrigin.Current);
            }
            this.ReadStoredData(reader);
            this.ReadChildren(reader);
        }

        private void ReadControllers(BSReader reader)
        {
            var controllerCount = reader.ReadInt32();
            for (int i = 0; i < controllerCount; i++)
            {
                var controllerName = reader.ReadString();
                var controller = EFCFactory.CreateController(controllerName);
                controller.Read(reader);

                this.Controllers.Add(controller);
            }
        }

        private void ReadChildren(BSReader reader)
        {
            var childObjectCount = reader.ReadInt32();
            for (int i = 0; i < childObjectCount; i++)
            {
                var childObject = new EFStoredObject(this);
                childObject.Read(reader);

                this.Children.Add(childObject);
            }
        }

        private void ReadStoredData(BSReader reader)
        {
            this.EEGlobalData.Read(reader);
            this.EmptyCommands0.Read(reader);
            this.EmitterCommands.Read(reader);
            this.EmptyCommands1.Read(reader);
            this.LifeTimeCommand.Read(reader);
            this.ProgramCommands.Read(reader);

            this.Byte0 = reader.ReadByte(); // Type?
            this.Byte1 = reader.ReadByte();
            this.Int0 = reader.ReadInt32(); //Start?
            this.Int1 = reader.ReadInt32(); //End?
            this.Int2 = reader.ReadInt32();

            this.Byte2 = reader.ReadByte();

            this.Int3 = reader.ReadInt32();

            this.Byte3 = reader.ReadByte();

            this.ViewModeCommand.Read(reader);
            this.Resource.Read(reader);
            this.RenderCommand.Read(reader);

            this.EmptyProgram0.Read(reader);
            this.RenderCommands.Read(reader);
        }
    }
}