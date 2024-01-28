using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerWPF
{
    public class FileTreeNode
    {
        public string Name { get; set; }
        public ObservableCollection<FileTreeNode> Children { get; set; }

        public FileTreeNode()
        {
            Children = new ObservableCollection<FileTreeNode>();
        }
    }

    public class FolderNode : FileTreeNode
    {
        public string Name { get; set; }
        public ObservableCollection<FileTreeNode> Children { get; set; }

        public FolderNode(string name)
        {
            Name = name;
            Children = new ObservableCollection<FileTreeNode>();
        }
    }

    public class ContactNode : FileTreeNode
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public TLink Link { get; set; } 

        public ContactNode(string lastName, string firstName, string email, string company, TLink link)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Company = company;
            Link = link;
        }
        public string DisplayName => $"{FirstName} {LastName}";
    }



}
