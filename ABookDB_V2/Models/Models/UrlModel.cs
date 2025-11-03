using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class UrlModel : RepositoryEntity
    {
        [Key]
        public int Id { get; set; }
        public string UrlAddress { get; set; }
        public BookModel Book { get; set; }
    }
}
