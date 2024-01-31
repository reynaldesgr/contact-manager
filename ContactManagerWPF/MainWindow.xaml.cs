using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ContactManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml. This is the main window for the Contact Manager application.
    /// It provides UI for displaying and manipulating contacts and folders.
    /// </summary>
    public partial class MainWindow : Window
    {
        private ContactManager contactManager;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            contactManager = new ContactManager();
        }

        /// <summary>
        /// Event handler for displaying the structure of contacts and folders in the TreeView.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void DisplayStructure_Click(object sender, RoutedEventArgs e)
        {
            structureTreeView.Items.Clear();
            var rootFolder = contactManager.GetStructureTree();
            var rootTreeViewItem = CreateTreeViewItemFromFolderNode(rootFolder);
            structureTreeView.Items.Add(rootTreeViewItem);

            ExpandAllNodes(rootTreeViewItem);
        }

        /// <summary>
        /// Recursively expands all nodes in the TreeView starting from the given item.
        /// </summary>
        /// <param name="item">The TreeViewItem to start expanding from.</param>
        private void ExpandAllNodes(TreeViewItem item) 
        {
            item.IsExpanded = true;
            foreach (var subItem in item.Items)
            {
                if (subItem is TreeViewItem subTreeViewItem)
                {
                    ExpandAllNodes(subTreeViewItem);
                }
            }
        }

        /// <summary>
        /// Creates a TreeViewItem from a FolderNode. Applies specific formatting based on the node's properties.
        /// </summary>
        /// <param name="folderNode">The FolderNode to convert.</param>
        /// <returns>A TreeViewItem representing the FolderNode.</returns>
        private TreeViewItem CreateTreeViewItemFromFolderNode(FolderNode folderNode, string indent="")
        {
            string childIndent = indent + "|-- ";

            var headerTextBlock = new TextBlock
            {
                Text = $"{childIndent}{folderNode.Name}",
                Foreground = Brushes.LightGreen
            };


            var treeItem = new TreeViewItem { Header = headerTextBlock };

            if (folderNode.Name.Equals(contactManager.GetCurrentName(), StringComparison.OrdinalIgnoreCase)) 
            {
                headerTextBlock.Background = Brushes.DarkBlue;
                headerTextBlock.Foreground = Brushes.White;
            }

            foreach (var subFolder in folderNode.Children.OfType<FolderNode>())
            {
                treeItem.Items.Add(CreateTreeViewItemFromFolderNode(subFolder));
            }

            foreach (var contact in folderNode.Children.OfType<ContactNode>())
            {
                var contactHeaderTextBlock = new TextBlock
                {
                    Text = $"{childIndent}{contact.FirstName} {contact.LastName} from {contact.Company} - {contact.Email} ({contact.Link})",
                    Foreground = Brushes.LightGreen
                };
                treeItem.Items.Add(new TreeViewItem { Header = contactHeaderTextBlock });
            }
            return treeItem;
        }

        /// <summary>
        /// Handles the "Create Folder" button click to prompt for a new folder name and add it.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void CreateFolder_Click(object sender, RoutedEventArgs e)
        {
            var inputDialog = new InputDialog();
            if (inputDialog.ShowDialog() == true)
            {
                contactManager.CreateNewFolder(inputDialog.Answer);
                DisplayStructure_Click(sender, e); 
            }
        }

        /// <summary>
        /// Handles the "Create Contact" button click to open a dialog for entering new contact details.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void CreateContact_Click(object sender, RoutedEventArgs e)
        {
            var contactDialog = new ContactDialog(); 
            if (contactDialog.ShowDialog() == true)
            {
                contactManager.CreateNewContact(contactDialog.LastName, contactDialog.FirstName, contactDialog.Email, contactDialog.Company, contactDialog.Link);
                DisplayStructure_Click(sender, e); 
            }

        }

        /// <summary>
        /// Handles the "Select Current Folder" button click to set the current folder based on user input.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void SelectCurrentFolder_Click(object sender, RoutedEventArgs e)
        {
            var inputDialog = new InputDialog();
            if (inputDialog.ShowDialog() == true)
            {
                contactManager.SelectCurrentFolder(inputDialog.Answer);
                DisplayStructure_Click(sender, e);
            }
           
        }

        /// <summary>
        /// Handles the "Save Data" button click to save all contact and folder data to an XML file.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void SaveData_Click(object sender, RoutedEventArgs e)
        {
            contactManager.SaveData(); 
            MessageBox.Show("Data saved successfully.");
        }

        /// <summary>
        /// Handles the "Exit" button click to close the application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }

}
