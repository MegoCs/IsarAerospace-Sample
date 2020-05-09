using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace IsarAerospace.CsvLoader
{
    public delegate void OnProgressChanged(object loadedObject, int rowNumber);
    public class CsvHandler
    {
        public OnProgressChanged NotifyProgressDel { get; set; }
        private BackgroundWorker _loadingWorker;
        private string _filePath;
        public CsvHandler(string fileName, bool delay)
        {
            _filePath = fileName;
            if (!string.IsNullOrEmpty(_filePath))
            {
                _loadingWorker = new BackgroundWorker
                {
                    WorkerReportsProgress = true
                };

                _loadingWorker.RunWorkerCompleted += LoadingWorker_RunWorkerCompleted;

                _loadingWorker.DoWork += LoadingWorker_DoWork;

                _loadingWorker.ProgressChanged += LoadingWorker_ProgressChanged;

                _loadingWorker.RunWorkerAsync();
            }
        }

        private void LoadingWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            NotifyProgressDel(e.UserState, e.ProgressPercentage);
        }

        private void LoadingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            Random random = new Random();

            for (int i = 0; i < 20; i++)
            {
                var book = new Book()
                {
                    Title = $"Title {random.Next(0,100)}",
                    Author = $"Author {random.Next(0, 100)} ",
                    Year = random.Next(1990, 2020),
                    Price = random.Next(0, 3000),
                    InStock = random.Next() % 2 == 0,
                    Description = $"This is Awsome {i}",
                    Binding = new List<string>(){
                        "Winner of the 1973 National Book Award",
                        "Gravity's Rainbow is a postmodern epic",
                        "a work as exhaustively significant to the second half of the 20th century as Joyce's Ulysses was to the first. Its sprawling"
                    }
                };
                worker.ReportProgress(i, book);
                Thread.Sleep(2000);
            }
        }

        private void LoadingWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }
}
