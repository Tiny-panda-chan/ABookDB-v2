using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{
    public class ScrapedFileModel
    {
        public Book BookData { get; set; }
        public File FileData { get; set; }

        public struct File
        {
            public string Name { get; set; }
            public string FileType { get; set; }
            public byte[]? Data { get; set; }
        }
        public struct Book
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int TotalPages { get; set; }
        }
    }
}
