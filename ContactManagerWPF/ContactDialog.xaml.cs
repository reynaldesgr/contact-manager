using Projet_CSHARP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ContactManagerWPF
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class ContactDialog : Window
    {
        public string LastName => LastNameTextBox.Text;
        public string FirstName => FirstNameTextBox.Text;
        public string Email => EmailTextBox.Text;
        public string Company => CompanyTextBox.Text;
        public TLink Link => (TLink)Enum.Parse(typeof(TLink), ((ComboBoxItem)LinkComboBox.SelectedItem).Content.ToString());

        public ContactDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }

}
