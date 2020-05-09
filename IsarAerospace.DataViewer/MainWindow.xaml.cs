using IsarAerospace.CsvLoader;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace IsarAerospace.DataViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Book> Books{ get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Books = new ObservableCollection<Book>();
            mainDataGrid.ItemsSource = Books;
        }

        private void RemoveOutOfStockItemsBtn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Books.Count; i++)
                if (!Books[i].InStock)
                    Books.RemoveAt(i--);
        }

        private void LoadFileBtn_Click(object sender, RoutedEventArgs e)
        {

            // Create OpenFileDialog
            OpenFileDialog openFileDlg = new OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            var result = openFileDlg.ShowDialog();

            // Load content of file in a TextBlock
            if (result == true)
            {
                FileName.Content= $"Loading File: {openFileDlg.SafeFileName}";
                CsvHandler csvHandler = new CsvHandler(openFileDlg.FileName, DelayProgress.IsChecked.Value)
                {
                    NotifyProgressDel = NewItemLoaded
                };
            }
        }

        private void NewItemLoaded(object book,int index)
        {
            Books.Add(book as Book);
            TotalLoadedBooks.Content = $"Total Loaded Books Now : {index}";
            CurrentTotalBooks.Content = $"Current Books Count : {Books.Count}";
        }

    }
}
