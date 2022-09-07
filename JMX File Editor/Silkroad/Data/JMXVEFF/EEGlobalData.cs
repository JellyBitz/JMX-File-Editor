using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;
using JMXFileEditor.Silkroad.IO;

using System;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    [Serializable]
    public class EEGlobalData : ISerializableBS
    {
        public int Int0 { get; private set; }

        public List<IEEParameter> Parameters { get; } = new List<IEEParameter>();

        public void Deserialize(BSReader reader)
        {
            Int0 = reader.ReadInt32();

            var parameterCount = reader.ReadInt32();
            for (var i = 0; i < parameterCount; i++)
            {
                var parameter = ParameterFactory.CreateParameterByName(reader.ReadString());
                parameter.Deserialize(reader);

                Parameters.Add(parameter);
            }
        }

        public void Serialize(BSWriter writer)
        {
            writer.Write(Int0);

            writer.Write(Parameters.Count);
            foreach (var parameter in Parameters)
            {
                writer.Write(parameter.Name);
                writer.Serialize(parameter);
            }
        }
    }
}