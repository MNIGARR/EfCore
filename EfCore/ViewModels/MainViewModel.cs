using EfCore.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using EfCore.Models;
using EfCore.Context;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


using Microsoft.EntityFrameworkCore;


namespace EfCore.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Author author;
        private Category category;

        public Author Author { get => author; set => Set(ref author, value); }
        public Category Category { get => category; set => Set(ref category, value); }

        public ObservableCollection<Author> Authors { get; set; } = new();
        public ObservableCollection<Category> Categories { get; set; } = new();

        public ObservableCollection<Book> Books { get; set; } = new();

        public RelayCommand AuthorCommand
        {
            get => new RelayCommand(async () =>
            {
                try
                {
                    Books.Clear();
                    
                    List<Book> source = await App.context.Books.Where(book => (Author == null || book.IdAuthor == Author.Id) && (Category == null || book.IdCategory == Category.Id)).ToListAsync();
                    source.ForEach(book => Books.Add(book));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERRORRRRRRR!!!");
                }
            });
        }
    }
}
