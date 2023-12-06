using JMXFileEditor.ViewModels;
using System.IO;
using System.Windows;

namespace JMXFileEditor
{
    /// <summary>
    /// Interaction logic for the MainWindow
    /// </summary>
    public partial class MainWindow : Window, IWindow
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            // Set view model
            DataContext = new ApplicationViewModel(this);
        }
        #endregion

        #region Interface implementation
        public string OpenFileDialog(string Title, string Filter, string InitialDirectory = "")
        {
            // Build dialog to search the file path
            var fileBrowserDialog = new Microsoft.Win32.OpenFileDialog();
            fileBrowserDialog.Title = Title;
            fileBrowserDialog.Filter = Filter;
            fileBrowserDialog.InitialDirectory = InitialDirectory;
            if (fileBrowserDialog.ShowDialog(this) == true)
            {
                return fileBrowserDialog.FileName;
            }
            return string.Empty;
        }

        public string OpenFolderDialog(string Title, ref string DefaultFilename, string InitialDirectory = "")
        {
            // Build dialog to search folder path
            var folderBrowserDialog = new Microsoft.Win32.OpenFileDialog();
            folderBrowserDialog.Title = Title;
            folderBrowserDialog.InitialDirectory = InitialDirectory;
            // setup to fake a folder browser
            folderBrowserDialog.ValidateNames = !string.IsNullOrEmpty(DefaultFilename);
            folderBrowserDialog.CheckFileExists = false;
            folderBrowserDialog.CheckPathExists = true;
            folderBrowserDialog.FileName = DefaultFilename;
            if (folderBrowserDialog.ShowDialog(this) == true)
            {
                var folderPath = Path.GetDirectoryName(folderBrowserDialog.FileName);
                DefaultFilename = folderBrowserDialog.FileName.Replace(folderPath+"\\", "");
                return folderPath;
            }
            DefaultFilename = string.Empty;
            return string.Empty;
        }

        public void ShowMessage(string Title, string Message)
        {
            MessageBox.Show(this, Message, Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
    }
}
