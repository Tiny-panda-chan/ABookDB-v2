using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class BookModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Title")]
        public string Name { get; set; }
        [StringLength(4000)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Pages")]
        public int TotalPages { get; set; }
        public UserModel? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public ICollection<CategoryModel>? Categories { get; set; }
        public ICollection<UrlModel>? Urls { get; set; }
        public ICollection<FileModel>? BookFiles { get; set; }
        public ICollection<ReviewModel>? Comments { get; set; }

    }
}
