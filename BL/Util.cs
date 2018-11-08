using System.Windows;

namespace Task3.BL
{
    /// <summary>
    /// Contains misc util methods.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Creates error message box.
        /// </summary>
        /// <param name="caption">Caption of the message box.</param>
        /// <param name="message">Error message.</param>
        public static void Error(string caption, string message)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Creates info message box.
        /// </summary>
        /// <param name="caption">Caption of the message box.</param>
        /// <param name="message">Info message.</param>
        public static void Info(string caption, string message)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}