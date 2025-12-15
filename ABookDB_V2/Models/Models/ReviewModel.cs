using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class ReviewModel : RepositoryEntity
    {
        [Key]
        public int Id { get; set; }
        public string text { get; set; }
        public BookModel book { get; set; }
        public UserModel createdBy { get; set; }
        public DateTime createdOn { get; set; } = DateTime.Now;
    }
}
