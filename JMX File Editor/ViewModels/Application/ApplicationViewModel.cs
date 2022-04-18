using JMXFileEditor.Silkroad.Data;
using JMXFileEditor.Silkroad.Data.JMXVBMT;
using JMXFileEditor.Silkroad.Data.JMXVCPD;
using JMXFileEditor.Silkroad.Data.JMXVRES;
using JMXFileEditor.ViewModels.Silkroad.JMXVBMT;
using JMXFileEditor.ViewModels.Silkroad.JMXVCPD;
using JMXFileEditor.ViewModels.Silkroad.JMXVRES;
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
        private JMXStructure m_JMXFileProperties;
        private JMXProperty m_JMXFileSelectedProperty;
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
        public JMXStructure FileProperties
        {
            get { return m_JMXFileProperties; }
            set
            {
                m_JMXFileProperties = value;
                OnPropertyChanged(nameof(FileProperties));
                OnPropertyChanged(nameof(IsFileOpen));
            }
        }
        /// <summary>
        /// Check if there is a file is opened
        /// </summary>
        public bool IsFileOpen
        {
            get { return FileProperties != null; }
        }
        /// <summary>
        /// Path of the current file opened
        /// </summary>
        public string FilePath
        {
            get { return m_FilePath; }
            set
            {
                m_FilePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }
        /// <summary>
        /// Selected property from file opened
        /// </summary>
        public JMXProperty FileSelectedProperty
        {
            get { return m_JMXFileSelectedProperty; }
            set
            {
                m_JMXFileSelectedProperty = value;
                OnPropertyChanged(nameof(FileSelectedProperty));
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
					// Create file UI
					FileProperties = CreateJMXViewModel(jmxFile);
                    // Set current file path used
                    FilePath = path;
                }
                catch (Exception ex)
                {
                    // Show details to user
                    Window.ShowMessage("File Error", ex.Message);
                }
            });
            CommandSaveFile = new RelayCommand(() => {
                // Just in case
                if (IsFileOpen)
                {
                    // Try to save the file
                    try
                    {
						// Converts to JMX File
						var jmxFile = LoadJMXFile(FileProperties);
                        // Save it
                        jmxFile.Save(FilePath);
						// Update values changed in the process
						FileProperties = CreateJMXViewModel(jmxFile);
                    }
                    catch (Exception ex)
                    {
                        // Show details to user
                        Window.ShowMessage("File Error", ex.Message);
                    }
                }
            });
            CommandSaveAsFile = new RelayCommand(() => {
                // Just in case
                if (IsFileOpen)
                {
                    // Try to save the file
                    try
                    {
                        // Converts to JMX File
                        var jmxFile = LoadJMXFile(FileProperties);

                        // Ask for file path and avoid empty result / canceled operation
                        var filename = (FileProperties.Name != string.Empty ? FileProperties.Name : jmxFile.Format) + "." + jmxFile.Extension;
                        var folderPath = Window.OpenFolderDialog("Save...", ref filename);
                        // check paths are correct
                        if (folderPath == string.Empty)
                            return;
                        // Search for a path if is not specified
                        var filePath = Path.Combine(folderPath, filename);
                        // Save it
                        jmxFile.Save(filePath);
                        // Update values changed in the process
                        FileProperties = CreateJMXViewModel(jmxFile);
						FilePath = filePath;
                    }
                    catch (Exception ex)
                    {
                        // Show details to user
                        Window.ShowMessage("File Error", ex.Message);
                    }
                }
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
                    // Check the header exist at least
                    if (br.BaseStream.Length < 12)
                        return null;

                    // Read file type and create format
                    var header = new string(br.ReadChars(12));
                    switch (header)
                    {
                        case JMXVRES_0109.FileHeader:
                            file = new JMXVRES_0109();
                            break;
                        case JMXVBMT_0102.FileHeader:
                            file = new JMXVBMT_0102();
                            break;
						case JMXVCPD_0101.FileHeader:
							file = new JMXVCPD_0101();
							break;
                        default:
                            throw new FileFormatException("JMX Header not found! File not supported.");
                    }
                    // Load it
                    fs.Seek(0, SeekOrigin.Begin);
                    file.Load(fs);
                }
            }
            return file;
		}
		/// <summary>
		/// Creates viewmodel from file
		/// </summary>
		private JMXStructure CreateJMXViewModel(IJMXFile JMXFile)
		{
			if (JMXFile is JMXVRES_0109 jmxvres_0109)
				return new JMXVRESVM(jmxvres_0109);
			if (JMXFile is JMXVBMT_0102 jmxvbmt_0102)
				return new JMXVBMTVM(jmxvbmt_0102);
			if (JMXFile is JMXVCPD_0101 jmxvcpd_0101)
				return new JMXVCPDVM(jmxvcpd_0101);
			// format not implemented
			throw new NotImplementedException();
		}
		/// <summary>
		/// Create JMX file from ViewModel
		/// </summary>
		private IJMXFile LoadJMXFile(JMXStructure JMXViewModel)
		{
			if(JMXViewModel is JMXVRESVM jmxvres)
				return (IJMXFile)jmxvres.GetClass();
			if (JMXViewModel is JMXVBMTVM jmxvbmt)
				return (IJMXFile)jmxvbmt.GetClass();
			if (JMXViewModel is JMXVCPDVM jmxvcpd)
				return (IJMXFile)jmxvcpd.GetClass();

			// format not implemented
			throw new NotImplementedException();
		}
		#endregion
	}
}
