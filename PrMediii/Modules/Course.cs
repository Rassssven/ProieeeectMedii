namespace PrMediii.Modules
{
    public class Course
    {
        public int ID { get; set; }
        public string CourseName { get; set; }
        public ICollection<CourseCategory>? CourseCategories { get; set; }

    }
}
