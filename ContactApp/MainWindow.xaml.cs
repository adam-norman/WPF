using ContactApp.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SQLite;

namespace ContactApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;
        public MainWindow()
        {
            InitializeComponent();
            contacts = new List<Contact>();
            GetContacts();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            //newContactWindow.Show();
            newContactWindow.ShowDialog();
            GetContacts();
        }
        public void GetContacts()
        {

            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                contacts = connection.Table<Contact>().OrderBy(c => c.Name).ToList();
            }
            if (contacts != null)
            {
                contactsListView.ItemsSource = contacts;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            FilterContacts(searchBox.Text);
        }

        private void FilterContacts(string searchBox)
        {
            var filteredContacts = contacts.Where(c => c.ToString().ToLower().Contains(searchBox.ToLower().Trim()));
            contactsListView.ItemsSource = filteredContacts.ToList();
        }

        private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            var selectedContact = listView.SelectedValue as Contact;
            if (selectedContact != null)
            {
                ContactDetailsWindow contactDetailsWindow = new ContactDetailsWindow(selectedContact);
                contactDetailsWindow.ShowDialog();
                GetContacts();
                FilterContacts("");
            }
        }
    }
}
