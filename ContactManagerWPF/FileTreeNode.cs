using System.Collections.ObjectModel;

namespace ContactManagerWPF
{
    /// <summary>
    /// Represents a node in a file tree, which can be either a folder or a contact.
    /// </summary>
    public class FileTreeNode
    {
        /// <summary>
        /// Gets or sets the name of the node.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the collection of child nodes.
        /// </summary>
        public ObservableCollection<FileTreeNode> Children { get; set; }

        /// <summary>
        /// Initializes a new instance of the FileTreeNode class.
        /// </summary>
        public FileTreeNode()
        {
            Children = new ObservableCollection<FileTreeNode>();
        }
    }

    /// <summary>
    /// Represents a folder node within the file tree, containing child nodes.
    /// </summary>
    public class FolderNode : FileTreeNode
    {
        public string Name { get; set; }
        public ObservableCollection<FileTreeNode> Children { get; set; }

        /// <summary>
        /// Initializes a new instance of the FolderNode class with a specific name.
        /// </summary>
        /// <param name="name">The name of the folder.</param>
        public FolderNode(string name)
        {
            Name     = name;
            Children = new ObservableCollection<FileTreeNode>();
        }
    }

    /// <summary>
    /// Represents a contact node within the file tree, containing contact information.
    /// </summary>
    public class ContactNode : FileTreeNode
    {
        /// <summary>
        /// Gets or sets the last name of the contact.
        /// </summary>
        public string LastName  { get; set; }

        /// <summary>
        /// Gets or sets the first name of the contact.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the contact.
        /// </summary>
        public string Email     { get; set; }

        /// <summary>
        /// Gets or sets the company associated with the contact.
        /// </summary>
        public string Company   { get; set; }

        /// <summary>
        /// Gets or sets the link type of the contact.
        /// </summary>
        public TLink Link       { get; set; }

        /// <summary>
        /// Gets the display name, which is a combination of first and last names.
        /// </summary>
        public string DisplayName => $"{FirstName} {LastName}";

        /// <summary>
        /// Initializes a new instance of the ContactNode class with specified contact details.
        /// </summary>
        /// <param name="lastName">The last name of the contact.</param>
        /// <param name="firstName">The first name of the contact.</param>
        /// <param name="email">The email address of the contact.</param>
        /// <param name="company">The company associated with the contact.</param>
        /// <param name="link">The link type associated with the contact.</param>
        public ContactNode(string lastName, string firstName, string email, string company, TLink link)
        {
            LastName    = lastName;
            FirstName   = firstName;
            Email       = email;
            Company     = company;
            Link        = link;
        }
    }



}
