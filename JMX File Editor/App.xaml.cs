using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace JMXFileEditor
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Events
        protected override void OnStartup(StartupEventArgs e)
        {
            // Set default culture
            SetCultureInfo(new CultureInfo("en-US"),null);
            base.OnStartup(e);
        }
        #endregion

        #region Private Helpers
        /// <summary>
        /// Set application culture from console and/or UI
        /// </summary>
        private void SetCultureInfo(CultureInfo culture, CultureInfo cultureUI)
        {
            Thread.CurrentThread.CurrentCulture = culture ?? Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = cultureUI ?? Thread.CurrentThread.CurrentUICulture;
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
        #endregion
    }
}