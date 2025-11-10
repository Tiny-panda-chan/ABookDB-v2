using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class ReadBooksModel : RepositoryEntity
    {
        [Key]
        public int Id { get; set; }
        public BookModel Book { get; set; }
        public UserModel User { get; set; }
        public int Chapter { get; set; }
        public int Page { get; set; } //last page user read
        public ReadStage ReadStage { get; set; } = ReadStage.NotStarted;
    }



    public enum ReadStage
    {
        NotStarted,
        InProgress,
        Finished
    }
}
