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
        /// Loads the file from stream
        /// </summary>
        void Load(Stream stream);
        /// <summary>
        /// Save file data
        /// </summary>
        void Save(string path);
    }
}