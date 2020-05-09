using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace IsarAerospace.CsvLoader
{
    public delegate void OnProgressChanged(object loadedObject, int rowNumber);

    public class CsvHandler
    {
        public OnProgressChanged NotifyProgressDel { get; set; }
        private BackgroundWorker _loadingWorker;
        private string _filePath;
        private bool _delay;

        public CsvHandler(string fileName, bool delay)
        {
            _filePath = fileName;
            _delay = delay;
            if (!string.IsNullOrEmpty(_filePath))
            {
                _loadingWorker = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
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

            using (var parser = new TextFieldParser(_filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                int i = -1;
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();

                    //Skip Headers
                    if (i == -1)
                    {
                        i++;
                        continue;
                    }
                    var book = new Book()
                    {
                        Title = fields[0],
                        Author = fields[1],
                        Year = int.Parse(fields[2]),
                        Price = decimal.Parse(fields[3]),
                        InStock = fields[4].ToLower() == "yes",
                        Binding = fields[5].Split(',').ToList(),
                        Description = fields[6]
                    };

                    if (worker.CancellationPending)
                        break;

                    worker.ReportProgress(++i, book);
                    if (_delay)
                        Thread.Sleep(3000);
                }
            }
        }

        private void LoadingWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        public void StopLoading()
        {
            _loadingWorker.CancelAsync();
        }
    }
}