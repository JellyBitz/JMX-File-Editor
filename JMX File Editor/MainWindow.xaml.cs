using JMXFileEditor.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        public string OpenFileDialog(string Title, string Filter = "")
        {
            // Build dialog to search the file path
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = Title;
            openFileDialog.Filter = Filter;
            if (openFileDialog.ShowDialog(this) == true)
            {
                return openFileDialog.FileName;
            }
            return "";
        }
        public void ShowMessage(string Title,string Message)
        {
            MessageBox.Show(this, Message, Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region UI stuffs
        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            if (item != null)
            {
                item.Focus();
                e.Handled = true;
            }
        }
        #endregion
    }
}
