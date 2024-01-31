using System.Windows;

namespace ContactManagerWPF
{
    /// <summary>
    /// Interaction logic for InputDialog.xaml.
    /// Provides a modal dialog for inputting text, with OK and Cancel buttons.
    /// </summary>
    public partial class InputDialog : Window
    {
        /// <summary>
        /// Gets the text input from the user.
        /// </summary>
        public string Answer { get; private set; }

        /// <summary>
        /// Initializes a new instance of the InputDialog class.
        /// </summary>
        public InputDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the OK button.
        /// Stores the input text and sets the dialog result to true.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Answer = InputTextBox.Text;
            this.DialogResult = true;
        }

        /// <summary>
        /// Handles the Click event of the Cancel button.
        /// Sets the dialog result to false, indicating cancellation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
