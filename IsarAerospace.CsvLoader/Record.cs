using System.Collections.Generic;
using System.Linq;

namespace IsarAerospace.CsvLoader
{
    public class Record
    {
        public string Title { get; set; }
        public string Author {get;set;}
        public int Year {get;set;}
        public double Price {get;set;}
        public bool InStock {get;set;}
        public List<string> Binding {get;set;}
        public string FirstBinding { get {
                return Binding?.FirstOrDefault()??string.Empty;
            }
        }
        public string Description{get;set;}        
    }
}