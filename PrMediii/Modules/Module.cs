using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrMediii.Modules
{
    public class Module
    {
        public int ID { get; set; }

        [Display(Name = "Module Title")]
        public string Title { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        [Range(0.01, 2000)]
        public int Price { get; set; }
        public decimal Duration { get; set; }

        [Display(Name = "Course Start")]
        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }

        public int? TrainerID { get; set; }
        public Trainer? Trainer { get; set; }

        public ICollection<CourseCategory> CourseCategories { get; set; }
    }
}
