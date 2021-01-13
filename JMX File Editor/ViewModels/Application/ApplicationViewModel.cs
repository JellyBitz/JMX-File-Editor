using JMXFileEditor.Silkroad.Data;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Input;

namespace JMXFileEditor.ViewModels
{
    /// <summary>
    /// ViewModel which controls the whole application
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        #region Private Members 
        private JMXFileNode m_JMXFileNodeOpened;
        private string m_FilePath;
        #endregion

        #region Public Properties
        /// <summary>
        /// Title from application with the current version
        /// </summary>
        public string Title { get; } = "JMX File Editor v"+ FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
        /// <summary>
        /// Gets or sets the current file opened
        /// </summary>
        public JMXFileNode FileOpened
        {
            get { return m_JMXFileNodeOpened; }
            set
            {
                m_JMXFileNodeOpened = value;
                OnPropertyChanged(nameof(FileOpened));
                OnPropertyChanged(nameof(IsFileOpen));
            }
        }
        /// <summary>
        /// Check if there is a file is opened
        /// </summary>
        public bool IsFileOpen
        {
            get { return FileOpened != null; }
        }
        /// <summary>
        /// Path of the current file opened
        /// </summary>
        public string FilePath
        {
            get { return m_FilePath; }
            set {
                m_FilePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command to open a JMX file
        /// </summary>
        public ICommand CommandOpenFile { get; }
        /// <summary>
        /// Command to save changes from JMX file
        /// </summary>
        public ICommand CommandSaveFile { get; }
        /// <summary>
        /// Command to save as a new JMX file
        /// </summary>
        public ICommand CommandSaveAsFile { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a viewmodel to control the application
        /// </summary>
        /// <param name="Window">User interface window this viewmodel controls</param>
        public ApplicationViewModel(IWindow Window)
        {
            #region Commands setup
            CommandOpenFile = new RelayCommand(() => {
                // Ask for file path and avoid empty result / canceled operation
                var path = Window.OpenFileDialog("Open...","All Files (*.*)|*.*");
                if (path == string.Empty)
                    return;
                // Try to load the file
                try
                {
                    // Load file format
                    var jmxFile = LoadJMXFile(path);
                    // Create and set nodes
                    FileOpened = JMXFileNode.Create(jmxFile);
                    // Set current path being used
                    FilePath = path;
                }
                catch (Exception ex)
                {
                    // Show details to user
                    Window.ShowMessage("File Error", ex.Message);
                }
            });
            CommandSaveFile = new RelayCommand(() => {

            });
            CommandSaveAsFile = new RelayCommand(() => {

            });
            #endregion
        }
        #endregion

        #region Private Helpers
        /// <summary>
        /// Creates and loads the JMX file
        /// </summary>
        private IJMXFile LoadJMXFile(string Path)
        {
            // Instance to be created and returned as result
            IJMXFile file = null;
            using (FileStream fs = new FileStream(Path,FileMode.Open,FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    // Read file type and create format
                    var header = new string(br.ReadChars(12));
                    switch (header)
                    {
                        case "JMXVRES 0109":
                            file = new JMXVRES_0109();
                            break;
                        default:
                            throw new FileFormatException("JMX File not supported.");
                    }
                    // Load it
                    fs.Seek(0, SeekOrigin.Begin);
                    file.Load(fs);
                }
            }
            return file;
        }
        #endregion
    }
}
