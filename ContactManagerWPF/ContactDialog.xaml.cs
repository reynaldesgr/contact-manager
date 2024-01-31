using System.Windows;
using System.Windows.Controls;

namespace ContactManagerWPF
{
    /// <summary>
    /// Interaction logic for ContactDialog.xaml.
    /// This dialog is used to input or edit the details of a contact.
    /// </summary>
    public partial class ContactDialog : Window
    {
        /// <summary>
        /// Gets the last name entered in the dialog.
        /// </summary>
        public string LastName  => LastNameTextBox.Text;

        /// <summary>
        /// Gets the first name entered in the dialog.
        /// </summary>
        public string FirstName => FirstNameTextBox.Text;

        /// <summary>
        /// Gets the email address entered in the dialog.
        /// </summary>
        public string Email     => EmailTextBox.Text;

        /// <summary>
        /// Gets the company name entered in the dialog.
        /// </summary>
        public string Company   => CompanyTextBox.Text;

        /// <summary>
        /// Gets the selected link type from the dialog.
        /// Assumes TLink is an enum representing the link type of the contact.
        /// </summary>
        public TLink Link       => (TLink)Enum.Parse(typeof(TLink), ((ComboBoxItem)LinkComboBox.SelectedItem).Content.ToString());


        /// <summary>
        /// Initializes a new instance of the ContactDialog class.
        /// </summary>
        public ContactDialog()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Handles the Click event of the OK button.
        /// Sets the dialog result to true indicating that the user has finished input.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }


        /// <summary>
        /// Handles the Click event of the Cancel button.
        /// Sets the dialog result to false indicating that the user has cancelled the input.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }

}
