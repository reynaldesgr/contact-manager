using System.IO;

// Cyptography
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace ContactManagerWPF
{
    /// <summary>
    /// Manages contact information, allowing for creation, storage, and retrieval of contact details.
    /// </summary>
    class ContactManager
    {
        private Folder            root;
        private Folder            current;
        private DataEntityFactory entityFactory;
        private DataSerializer    serializer;

        private string            encryptionKey;
        private string            decryptionKey;

        /// <summary>
        /// Initializes a new instance of the ContactManager class, setting up the data storage and encryption mechanisms.
        /// </summary>
        public ContactManager()
        {
            entityFactory = new DataEntityFactory();
            serializer    = new DataSerializer();
            encryptionKey = GetEncryptionKey();
            LoadData(); 
        }

        /// <summary>
        /// Creates a new folder for organizing contacts.
        /// </summary>
        /// <param name="name">The name of the new folder.</param>
        public void CreateNewFolder(string name)
        {
            // Ensure current.SubFolders is not null
            if (current.SubFolders == null)
            {
                current.SubFolders = new List<Folder>();
            }

            Folder newFolder = entityFactory.CreateFolder(name);
            current.SubFolders.Add(newFolder);
            Console.WriteLine($"New folder '{name}' created in '{current.Name}'");
        }

        /// <summary>
        /// Creates a new contact and adds it to the current folder.
        /// </summary>
        /// <param name="lastName">The last name of the contact.</param>
        /// <param name="firstName">The first name of the contact.</param>
        /// <param name="email">The email address of the contact.</param>
        /// <param name="company">The company associated with the contact.</param>
        /// <param name="link">The type of link associated with the contact.</param>
        public void CreateNewContact(string lastName, string firstName, string email, string company, TLink link)
        {
            if (current.Contacts == null)
            {
                current.Contacts = new List<Contact>();
            }

            Contact newContact = entityFactory.CreateContact(lastName, firstName, email, company, link);
            current.Contacts.Add(newContact);
            Console.WriteLine($"New contact '{firstName} {lastName}' created in '{current.Name}'.");
        }

        /// <summary>
        /// Selects the current working folder based on the provided name.
        /// </summary>
        /// <param name="folderName">The name of the folder to make current.</param>
        public void SelectCurrentFolder(string folderName)
        {
            Folder selected = FindFolderByName(root, folderName);
            if (selected != null)
            {
                current = selected;
                Console.WriteLine($"Current folder set to '{folderName}'.");
            }
            else
            {
                Console.WriteLine($"Folder '{folderName}' not found.");
            }
        }

        /// <summary>
        /// Saves the current state of contact information to a file with encryption.
        /// </summary>
        public void SaveData()
        {
            string fileName = "contactsData.xml";
            string fullPath = Path.GetFullPath(fileName);

            if (!File.Exists(fileName))
            {
                using (FileStream fs = File.Create(fileName))
                {
                    fs.Close();
                }
            }
            if (string.IsNullOrEmpty(encryptionKey))
            {
                encryptionKey = GetEncryptionKey(); 
            }

            serializer.SerializeToFile(root, fileName, encryptionKey);
            Console.WriteLine("Data saved successfully in " + fullPath + ".");
        }

        /// <summary>
        /// Generates an encryption key based on the current Windows user's SID, hashed for security.
        /// </summary>
        /// <returns>A base64 encoded string to be used as an encryption key.</returns>
        private string GetEncryptionKey()
        {
            string usid = WindowsIdentity.GetCurrent().User.Value;

            using (SHA256 sha256 = SHA256.Create()) 
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(usid));
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Loads contact information from a file, decrypting it with the current decryption key.
        /// </summary>
        public void LoadData()
        {
            string fileName = "contactsData.xml";

            if (File.Exists(fileName))
            {
                if (string.IsNullOrEmpty(decryptionKey))
                {
                    decryptionKey = GetEncryptionKey();
                }

                root = serializer.DeserializeFromFile<Folder>(fileName, decryptionKey);
                current = root;
                Console.WriteLine("Data loaded successfully.");
            }
            else
            {
                root = entityFactory.CreateFolder("root");
                current = root;
                Console.WriteLine("No existing data. Created a new root folder.");
            }
        }

        /// <summary>
        /// Unloads the current data, resetting the state to a new root folder.
        /// </summary>
        public void UnloadData()
        {
            root = entityFactory.CreateFolder("root");
            current = root;
            Console.WriteLine("Data unloaded successfully.");
        }

        /// <summary>
        /// Displays the hierarchical structure of folders and contacts.
        /// </summary>
        /// <param name="folder">The starting folder to display.</param>
        /// <param name="depth">The current depth in the hierarchy, used for indentation.</param>
        public void DisplayStructure(Folder folder, int depth)
        {
            string indent = new string(' ', depth * 4);

            Console.WriteLine($"{indent}[D] - {folder.Name}");

            if (folder.Contacts != null)
            {
                foreach (var contact in folder.Contacts)
                {
                    Console.WriteLine($"{indent} | [C] - {contact.FirstName} {contact.LastName} ({contact.Email})");
                }

            }
            if (folder.SubFolders != null)
            {
                foreach (var subFolder in folder.SubFolders)
                {
                    DisplayStructure(subFolder, depth + 1);
                }
            }
        }

        /// <summary>
        /// Finds a folder by name, searching recursively through the hierarchy.
        /// </summary>
        /// <param name="folder">The folder to start the search in.</param>
        /// <param name="folderName">The name of the folder to find.</param>
        /// <returns>The found Folder or null if not found.</returns>
        private Folder FindFolderByName(Folder folder, string folderName)
        {
            if (folder.Name.Equals(folderName, StringComparison.OrdinalIgnoreCase))
            {
                return folder;
            }

            if (null != folder.SubFolders)
            {
                foreach (var subfolder in folder.SubFolders)
                {
                    var foundFolder = FindFolderByName(subfolder, folderName);
                    if (foundFolder != null)
                    {
                        return foundFolder;
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Retrieves the hierarchical structure of folders and contacts starting from the root.
        /// </summary>
        /// <returns>A FolderNode representing the root of the hierarchy.</returns>
        public FolderNode GetStructureTree()
        {
            return CreateNode(root);
        }

        /// <summary>
        /// Recursively creates a tree of FolderNodes and ContactNodes from a given Folder.
        /// Each FolderNode contains ContactNodes for contacts in the folder and child FolderNodes for its subfolders.
        /// </summary>
        /// <param name="folder">The Folder object to transform into a FolderNode.</param>
        /// <returns>A FolderNode representing the given Folder and its hierarchy.</returns>
        private FolderNode CreateNode(Folder folder)
        {
            var folderNode = new FolderNode(folder.Name);
            if (folder.Contacts != null)
            {
                foreach (var contact in folder.Contacts)
                {
                    var contactNode = new ContactNode(contact.LastName, contact.FirstName, contact.Email, contact.Company, contact.Link);
                    folderNode.Children.Add(contactNode);
                }
            }
            if (folder.SubFolders != null)
            {
                foreach (var subFolder in folder.SubFolders)
                {
                    folderNode.Children.Add(CreateNode(subFolder));
                }
            }
            return folderNode;
        }

        /// <summary>
        /// Gets the name of the current working folder.
        /// </summary>
        /// <returns>The name of the current Folder, or null if no current folder is set.</returns>
        internal string? GetCurrentName()
        {
            return current.Name;
        }
    }
}
