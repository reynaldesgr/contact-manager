using Microsoft.WindowsAPICodePack.Dialogs;
using Projet_CSHARP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ContactManagerWPF
{
    public partial class MainWindow : Window
    {
        private ContactManager contactManager;

        public MainWindow()
        {
            InitializeComponent();
            contactManager = new ContactManager();
        }

        private void DisplayStructure_Click(object sender, RoutedEventArgs e)
        {
            structureTreeView.Items.Clear();
            var rootFolder = contactManager.GetStructureTree();
            var rootTreeViewItem = CreateTreeViewItemFromFolderNode(rootFolder);
            structureTreeView.Items.Add(rootTreeViewItem);
        
            ExpandAllNodes(rootTreeViewItem);
        }

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

        private TreeViewItem CreateTreeViewItemFromFolderNode(FolderNode folderNode, string indent="")
        {
            string childIndent = indent + "|-- ";

            var headerTextBlock = new TextBlock
            {
                Text = $"{childIndent}{folderNode.Name}",
                Foreground = Brushes.LightGreen
            };


            var treeItem = new TreeViewItem { Header = headerTextBlock };

            foreach (var subFolder in folderNode.Children.OfType<FolderNode>())
            {
                treeItem.Items.Add(CreateTreeViewItemFromFolderNode(subFolder));
            }

            foreach (var contact in folderNode.Children.OfType<ContactNode>())
            {
                var contactHeaderTextBlock = new TextBlock
                {
                    Text = $"{childIndent}{contact.FirstName} {contact.LastName} - {contact.Link}",
                    Foreground = Brushes.LightGreen
                };
                treeItem.Items.Add(new TreeViewItem { Header = contactHeaderTextBlock });
            }
            return treeItem;
        }


        private void CreateFolder_Click(object sender, RoutedEventArgs e)
        {
            var inputDialog = new InputDialog();
            if (inputDialog.ShowDialog() == true)
            {
                contactManager.CreateNewFolder(inputDialog.Answer);
                DisplayStructure_Click(sender, e); 
            }
        }

        private void CreateContact_Click(object sender, RoutedEventArgs e)
        {
            var contactDialog = new ContactDialog(); 
            if (contactDialog.ShowDialog() == true)
            {
                contactManager.CreateNewContact(contactDialog.LastName, contactDialog.FirstName, contactDialog.Email, contactDialog.Company, contactDialog.Link);
                DisplayStructure_Click(sender, e); 
            }

        }

        private void SelectCurrentFolder_Click(object sender, RoutedEventArgs e)
        {
            var inputDialog = new InputDialog();
            if (inputDialog.ShowDialog() == true)
            {
                contactManager.SelectCurrentFolder(inputDialog.Answer);
            }
        }

        private void SaveData_Click(object sender, RoutedEventArgs e)
        {
            contactManager.SaveData(); 
            MessageBox.Show("Data saved successfully.");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

}
