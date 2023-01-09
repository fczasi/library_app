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

namespace lib1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public DatabaseManager dbManager = new DatabaseManager("Server=localhost;Database=library;Trusted_Connection=True;");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void deleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (bookDataGrid.SelectedItem != null)
            {
                Book? passedBook;
                if (bookDataGrid.SelectedItem != null)
                {
                    passedBook = bookDataGrid.SelectedItem as Book;
                }
                else
                {
                    passedBook = new Book();
                }
                dbManager.DeleteBook(passedBook.Id);
                czystka();
            }
        }

        private void editBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (bookDataGrid.SelectedItem != null)
            {
                Book? passedBook;
                if (bookDataGrid.SelectedItem != null)
                {
                    passedBook = bookDataGrid.SelectedItem as Book;
                }
                else
                {
                    passedBook = new Book();
                }
                dbManager.EditBook(passedBook.Id, titlebox.Text, authorbox.Text, yearbox.Text);
                czystka();
            }
            
        }

        private void addBookButton_Click(object sender, RoutedEventArgs e)
        {
            dbManager.AddBook(titlebox.Text, authorbox.Text, yearbox.Text);
        }

        private void showbutton_Click(object sender, RoutedEventArgs e)
        {
            bookDataGrid.ItemsSource= dbManager.getbooks();
        }

        private void czystka() 
        {
            titlebox.Clear();
            authorbox.Clear();
            yearbox.Clear();
        }
    }
}
