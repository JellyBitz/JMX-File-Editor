using System.IO;

namespace JMXFileEditor.Silkroad.Data
{
    /// <summary>
    /// Represents any possible JMX file
    /// </summary>
    public interface IJMXFile
    {
        /// <summary>
        /// Format name
        /// </summary>
        string Format { get; }
        /// <summary>
        /// File extension
        /// </summary>
        string Extension { get; }
        /// <summary>
        /// Loads the file from reading the stream
        /// </summary>
        void Load(Stream stream);
        /// <summary>
        /// Save the file data
        /// </summary>
        void Save(string path);
    }
}