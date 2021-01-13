namespace JMXFileEditor.ViewModels
{
    /// <summary>
    /// Native window
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        /// Opens a dialog to the user and returns the file path choosen
        /// </summary>
        string OpenFileDialog(string Title, string Filter);
        /// <summary>
        /// Shows an informative message to the user
        /// </summary>
        void ShowMessage(string Title, string Message);
    }
}
