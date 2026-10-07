using System;
using System.Windows.Forms;
public partial class ContactForm : Form
{
    private ContactManager contactManager;
    public ContactForm()
    {
        InitializeComponent();
        contactManager = new ContactManager();
        UpdateContactsList();
    }
    private void UpdateContactsList()
    {
        contactsListBox.Items.Clear();
        foreach (var contact in contactManager.Contacts)
        {
            contactsListBox.Items.Add($"{contact.Name}  -  {contact.PhoneNumber}");
        }
    }
    private void AddContactButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(nameTextBox.Text) || string.IsNullOrEmpty(phoneNumberTextBox.Text))
        {
            MessageBox.Show(" Заполните  все  поля!");
            return;
        }
        Contact newContact = new Contact(nameTextBox.Text, phoneNumberTextBox.Text);
        try
        {
            contactManager.AddContact(newContact);
            nameTextBox.Clear();
            phoneNumberTextBox.Clear();
            UpdateContactsList();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private void RemoveContactButton_Click(object sender, EventArgs e)
    {
        if (contactsListBox.SelectedIndex == -1)
        {
            MessageBox.Show(" Выберите  контакт  для  удаления!");
            return;
        }
        string selectedItem = contactsListBox.SelectedItem.ToString();
        string[] parts = selectedItem.Split(new[]
        {
            '-'
        }
        , StringSplitOptions.None);
        if (parts.Length >= 2)
        {
            string name = parts[0].Trim();
            string phoneNumber = parts[1].Trim();
            var contactToRemove = contactManager.Contacts.Find(c => c.Name == name && c.PhoneNumber == phoneNumber);
            if (contactToRemove != null)
            {
                try
                {
                    contactManager.RemoveContact(contactToRemove);
                    UpdateContactsList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
    private void SearchButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(searchTextBox.Text))
        {
            UpdateContactsList();
            return;
        }
        var searchResults = contactManager.SearchContacts(searchTextBox.Text);
        contactsListBox.Items.Clear();
        foreach (var contact in searchResults)
        {
            contactsListBox.Items.Add($"{contact.Name}  -  {contact.PhoneNumber}");
        }
    }
}
