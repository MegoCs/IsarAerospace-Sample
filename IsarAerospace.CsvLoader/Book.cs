using System.Collections.Generic;
using System.Linq;

namespace IsarAerospace.CsvLoader
{
    public class Book
    {
        public string Title { get; set; }
        public string Author {get;set;}
        public int Year {get;set;}
        public decimal Price {get;set;}
        public bool InStock {get;set;}
        public List<string> Binding {get;set;}
        public string Description{get;set;}        
    }
}