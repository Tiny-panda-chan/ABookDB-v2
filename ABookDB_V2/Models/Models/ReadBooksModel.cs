using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class ReadBooksModel
    {
        [Key]
        public int Id { get; set; }
        public BookModel Book { get; set; }
        public UserModel User { get; set; }
        public int Chapter { get; set; }
        public int Page { get; set; } //last page user read
        public ReadStage ReadStage { get; set; } = ReadStage.NotStarted;

        public void UpdateReadStage()
        {
            if (Book.TotalPages <= Page)
            {
                ReadStage = ReadStage.Finished;
            }
            else
            {
                ReadStage = ReadStage.InProgress;
            }
        }
    }



    public enum ReadStage
    {
        NotStarted,
        InProgress,
        Finished
    }
}
