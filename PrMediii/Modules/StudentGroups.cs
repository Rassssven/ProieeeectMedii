using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace PrMediii.Modules
{
    public class StudentGroups
    {
        public int ID { get; set; }
        public int? StudentID { get; set; }
        public Student? Student { get; set; }
        public int? ModuleID { get; set; }
        public Module? Module { get; set; }

        [DataType(DataType.Date)]
        public DateTime CourseStart { get; set; }
    }
}
