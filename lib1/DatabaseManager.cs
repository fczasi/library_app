using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lib1
{
    public class DatabaseManager
    {
        private SqlConnection connection;

        public DatabaseManager(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public void AddBook(string title, string author, string year)
        {
            connection.Open();

            SqlCommand command = new SqlCommand($"INSERT INTO books (Title, Author, Year) VALUES ('{title}', '{author}', {year})", connection);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void EditBook(int bookId, string title, string author, string year)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE books SET Title = @title, Author = @author, Year = @year WHERE Id = @bookId", connection);
            command.Parameters.AddWithValue("@bookId", bookId);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@author", author);
            command.Parameters.AddWithValue("@year", year);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void DeleteBook(int bookId)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM books WHERE Id = @bookId", connection);
            command.Parameters.AddWithValue("@bookId", bookId);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public List<Book> getbooks() 
        {

            List<Book> books = new List<Book>();

            String query = "SELECT * FROM books";

            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataReader dbr;
            try
            {
                connection.Open();
                dbr = cmd.ExecuteReader();
                while (dbr.Read())
                {
                    string sID = dbr["Id"].ToString();
                    string stitle = (string)dbr["Title"]; // name is string value
                    string sauthor = (string)dbr["Author"];
                    string syear = (string)dbr["Year"];
                    Book book = new Book()
                    {
                        Id = int.Parse(sID),
                        Title = stitle,
                        Author = sauthor,
                        Year = syear,
                    };
                    books.Add(book);
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }

            connection.Close();

            return books;
        }
    }
}
