using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;
using JMXFileEditor.Silkroad.IO;

using System;
using System.Collections.Generic;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    [Serializable]
    public class EEGlobalData
    {
        public int Int0 { get; private set; }

        public List<IEEParameter> Parameters { get; } = new List<IEEParameter>();

        public void Read(BSReader reader)
        {
            this.Int0 = reader.ReadInt32();

            var parameterCount = reader.ReadInt32();
            for (int i = 0; i < parameterCount; i++)
            {
                var parameter = ParameterFactory.CreateParameterByName(reader.ReadString());
                parameter.Read(reader);

                this.Parameters.Add(parameter);
            }
        }
    }
}