using IsarAerospace.CsvLoader;
using IsarAerospace.DataViewer.Converters;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IsarAerospace.DataViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CsvHandler _csvHandler;

        public ObservableCollection<Book> Books { get; set; }

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
            var openFileDlg = new OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            var result = openFileDlg.ShowDialog();

            // if file is selected
            if (result == true)
            {
                FileName.Content = $"Loading File: {openFileDlg.SafeFileName}";
                _csvHandler = new CsvHandler(openFileDlg.FileName, DelayProgress.IsChecked.Value)
                {
                    NotifyProgressDel = NewItemLoaded
                };
                CancelLoadingBtn.IsEnabled = true;
            }
        }

        private void NewItemLoaded(object book, int index)
        {
            Books.Add(book as Book);
            NumberToColorConverter.MaxGradientValue = Books.Max(x => x.Price);
            TotalLoadedBooks.Content = $"{index}";
        }

        private void DescriptionBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Books[mainDataGrid.SelectedIndex].Description, "Book Description");
        }

        private void CancelLoadingBtn_Click(object sender, RoutedEventArgs e)
        {
            _csvHandler.StopLoading();
            CancelLoadingBtn.IsEnabled = false;
        }

        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Books.Clear();
        }
    }
}