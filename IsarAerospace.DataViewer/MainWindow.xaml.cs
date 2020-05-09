using IsarAerospace.CsvLoader;
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
            InitSampleData();
        }

        private void InitSampleData()
        {
            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                Books.Add(new Book()
                {
                    Title = $"Title {i}",
                    Author = $"Author {i} ",
                    Year = random.Next(1990,2020),
                    Price = random.Next(0,3000),
                    InStock = random.Next()%2==0,
                    Binding = new List<string>(){
                    "Winner of the 1973 National Book Award",
                    "Gravity's Rainbow is a postmodern epic",
                    "a work as exhaustively significant to the second half of the 20th century as Joyce's Ulysses was to the first. Its sprawling",
                    "encyclopedic narrative, and penetrating analysis of the impact of technology on society make it an intellectual tour de force. Ignition!: An informal history of liquid rocket propellants encyclopedic narrative, and penetrating analysis of the impact of technology on society make it an intellectual tour de force. Ignition!: An informal history of liquid rocket propellants"
                    },
                    Description = $"This is Awsome {i}"
                });
            }
        }

        private void RemoveOutOfStockItemsBtn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Books.Count; i++)
                if (!Books[i].InStock)
                    Books.RemoveAt(i--);
        }

        private void LoadFileBtn_Click(object sender, RoutedEventArgs e)
        {
            InitSampleData();
        }
    }
}
