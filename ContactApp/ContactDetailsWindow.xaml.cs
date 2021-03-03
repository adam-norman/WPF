using ContactApp.Models;
using SQLite;
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

namespace ContactApp
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class ContactDetailsWindow : Window
    {
        private Contact contact;

        public ContactDetailsWindow(Contact contact)
        {
            InitializeComponent();
            this.contact = contact;
            FillContactForm();
        }

        private void FillContactForm()
        {
            phoneTextBox.Text = this.contact.Phone;
            nameTextBox.Text = this.contact.Name;
            emailTextBox.Text = this.contact.Email;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            this.contact.Phone = phoneTextBox.Text;
            this.contact.Name = nameTextBox.Text;
            this.contact.Email = emailTextBox.Text;
            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                connection.CreateTable<Contact>();
                connection.Update(contact);
            }
            Close(); // closing window
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.dbPath))
            {
                connection.CreateTable<Contact>();
                connection.Delete(contact);
            }
            Close(); // closing window
        }
    }
}
