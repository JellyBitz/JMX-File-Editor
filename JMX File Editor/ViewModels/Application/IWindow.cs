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
        string OpenFileDialog(string Title, string Filter, string InitialDirectory = "");
        /// <summary>
        /// Opens a dialog to the user and returns the path choosen
        /// </summary>
        /// <param name="Title">Dialog Title</param>
        /// <param name="FileNameOption">Default filename for dialogs used for saving</param>
        /// <returns></returns>
        string OpenFolderDialog(string Title, ref string FileNameOption, string InitialDirectory = "");
        /// <summary>
        /// Shows an informative message to the user
        /// </summary>
        void ShowMessage(string Title, string Message);
    }
}
