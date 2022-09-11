using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;
using JMXFileEditor.Silkroad.IO;

using System.Collections.Generic;
using System.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public class EFStoredObject : ISerializableBS
    {
        #region Public Properties
        public string Name { get; set; } = "<None>";
        public List<EFController> Controllers { get; set; } = new List<EFController>();
        public EEGlobalData EEGlobalData { get; set; }
        public EEStaticProgram EmptyCommands0 { get; set; } //Empty?
        public EEStaticProgram EmitterCommands { get; set; } //Emitters?
        public EEStaticProgram EmptyCommands1 { get; set; } //Empty?
        public EEStaticProgram ProgramCommands { get; set; } // ProgamUpdate
        public EEStaticCommand LifeTimeCommand { get; set; } //LifeTime
        public byte Byte0 { get; set; }
        public byte Byte1 { get; set; }
        public int Int0 { get; set; }
        public int Int1 { get; set; }
        public int Int2 { get; set; }
        public byte Byte2 { get; set; }
        public int Int3 { get; set; }
        public byte Byte3 { get; set; }
        public EEStaticCommand ViewModeCommand { get; set; } //ViewMode
        public EEResource Resource { get; set; }
        public EEStaticCommand RenderModeCommand { get; set; } //RenderMode
        public EEProgram EmptyProgram0 { get; set; } //Empty?
        public EEProgram RenderCommands { get; set; } //RenderCommands?
        public EFStoredObject Parent { get; set; }
        public List<EFStoredObject> Children { get; set; } = new List<EFStoredObject>();
        #endregion

        #region Constructor
        public EFStoredObject(EFStoredObject parent = null)
        {
            Parent = parent;

            EEGlobalData = new EEGlobalData();
            EmptyCommands0 = new EEStaticProgram();
            EmitterCommands = new EEStaticProgram();
            EmptyCommands1 = new EEStaticProgram();
            ProgramCommands = new EEStaticProgram();

            LifeTimeCommand = new EEStaticCommand();
            ViewModeCommand = new EEStaticCommand();
            RenderModeCommand = new EEStaticCommand();

            EmptyProgram0 = new EEProgram();
            RenderCommands = new EEProgram();

            Resource = new EEResource();
        }
        #endregion

        public virtual void Deserialize(BSReader reader)
        {
            reader.SkipRead(4); // dataOffset
            //if (true)
            //{
            Name = reader.ReadString();
            ReadControllers(reader);
            //}
            //else
            //{
            //reader.BaseStream.Seek(dataOffset, SeekOrigin.Current);
            //}
            ReadStoredData(reader);
            ReadChildren(reader);
        }

        private void ReadControllers(BSReader reader)
        {
            var controllerCount = reader.ReadInt32();
            Controllers.Capacity = controllerCount;
            for (var i = 0; i < controllerCount; i++)
            {
                var controllerName = reader.ReadString();
                var controller = EFCFactory.CreateController(controllerName);
                controller.Deserialize(reader);

                Controllers.Add(controller);
            }
        }

        private void ReadChildren(BSReader reader)
        {
            var childObjectCount = reader.ReadInt32();
            Children.Capacity = childObjectCount;
            for (var i = 0; i < childObjectCount; i++)
            {
                var childObject = new EFStoredObject(this);
                childObject.Deserialize(reader);

                Children.Add(childObject);
            }
        }

        private void ReadStoredData(BSReader reader)
        {
            EEGlobalData.Deserialize(reader);
            EmptyCommands0.Deserialize(reader);
            EmitterCommands.Deserialize(reader);
            EmptyCommands1.Deserialize(reader);
            LifeTimeCommand.Deserialize(reader);
            ProgramCommands.Deserialize(reader);

            Byte0 = reader.ReadByte(); // Type?
            Byte1 = reader.ReadByte();
            Int0 = reader.ReadInt32(); //Start?
            Int1 = reader.ReadInt32(); //End?
            Int2 = reader.ReadInt32();

            Byte2 = reader.ReadByte();

            Int3 = reader.ReadInt32();

            Byte3 = reader.ReadByte();

            ViewModeCommand.Deserialize(reader);
            Resource.Deserialize(reader);
            RenderModeCommand.Deserialize(reader);

            EmptyProgram0.Deserialize(reader);
            RenderCommands.Deserialize(reader);
        }

        public void Serialize(BSWriter writer)
        {
            var storedObjectPosition = (int)writer.BaseStream.Position;
            writer.Write(0); // dataOffset

            writer.Write(Name);
            WriteControllers(writer);

            var dataOffset = ((int)writer.BaseStream.Position) - (storedObjectPosition + 4);

            // Write correct dataOffset
            writer.Seek(storedObjectPosition, SeekOrigin.Begin);
            writer.Write(dataOffset);
            writer.Seek(dataOffset, SeekOrigin.Current);

            WriteStoredData(writer);
            WriteChildren(writer);
        }

        private void WriteControllers(BSWriter writer)
        {
            writer.Write(Controllers.Count);
            foreach (var item in Controllers)
            {
                writer.Write(item.Name);
                writer.Serialize(item);
            }
        }
        private void WriteStoredData(BSWriter writer)
        {
            writer.Serialize(EEGlobalData);
            writer.Serialize(EmptyCommands0);
            writer.Serialize(EmitterCommands);
            writer.Serialize(EmptyCommands1);
            writer.Serialize(LifeTimeCommand);
            writer.Serialize(ProgramCommands);

            writer.Write(Byte0);
            writer.Write(Byte1);
            writer.Write(Int0);
            writer.Write(Int1);
            writer.Write(Int2);

            writer.Write(Byte2);

            writer.Write(Int3);

            writer.Write(Byte3);

            writer.Serialize(ViewModeCommand);
            writer.Serialize(Resource);
            writer.Serialize(RenderModeCommand);
            writer.Serialize(EmptyProgram0);

            writer.Serialize(RenderCommands);
        }
        private void WriteChildren(BSWriter writer)
        {
            writer.Write(Children.Count);
            foreach (var item in Children)
                writer.Serialize(item);
        }
    }
}