using JMXFileEditor.Silkroad.IO;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JMXFileEditor.Silkroad.Data.JMXVENVI
{

    /// <summary>
    /// Represents the color grading / post processing effects over the course of the day.
    /// <para>https://github.com/DummkopfOfHachtenduden/SilkroadDoc/wiki/JMXVENVI</para>
    /// </summary>
    class JMXVENVI : IJMXFile
    {
        #region Public Properties
        public string Name { get; set; }
        public List<EnvironmentProfile> Profiles { get; } = new List<EnvironmentProfile>();
        public EnvironmentNode RootNode { get; set; } = new EnvironmentNode();
        #endregion

        #region Interface Implementation
        public string Format => "JMXVENVI";
        public string Extension => "ifo";
        public void Load(Stream stream)
        {
            using (var reader = new BSReader(stream, Encoding.GetEncoding(949)))
            {
                var format = reader.ReadString(8);
                if (format != "JMXVENVI")
                    throw new Exception($"Invalid file signature: {format}");

                var version = int.Parse(reader.ReadString(4));
                var profileCount = reader.ReadInt16();
                Name = reader.ReadString();
                for (var i = 0; i < profileCount; i++)
                    Profiles.Add(reader.DeserializeParameterized<EnvironmentProfile>(version));

                RootNode.Deserialize(reader);
            }
        }
        public void Save(string path)
        {
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var writer = new BSWriter(stream, Encoding.GetEncoding(949)))
            {
                writer.Write("JMXVENVI1003");

                writer.Write(Profiles.Count);
                writer.Write(Name);

                foreach (var profile in Profiles)
                    writer.SerializeParameterized(profile, 1003);

                writer.Serialize(RootNode);
            }
        }
        #endregion
    }
}
