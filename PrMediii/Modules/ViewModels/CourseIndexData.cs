using System.Security.Policy;

namespace PrMediii.Modules.ViewModels
{
    public class CourseIndexData
    {
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Module> Moduless { get; set; }
    }
}
