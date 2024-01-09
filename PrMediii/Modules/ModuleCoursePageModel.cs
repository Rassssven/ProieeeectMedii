using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PrMediii.Data;


namespace PrMediii.Modules
{
    public class ModuleCoursePageModel : PageModel
    {
        public List<AssignedCoursesData> AssignedCoursesDataList;

        public void PopulateAssignedCoursesData(PrMediiiContext context, Module Module)
        {
            var allCourses = context.Course;
            var moduleCourses = new HashSet<int>(
                Module.CourseCategories.Select(c => c.CourseID));
            AssignedCoursesDataList = new List<AssignedCoursesData>();
            foreach (var cat in allCourses)
            {
                AssignedCoursesDataList.Add(new AssignedCoursesData
                {
                    CourseID = cat.ID,
                    Name = cat.CourseName,
                    Assigned = moduleCourses.Contains(cat.ID)
                });
            }
        }

        public void UpdateModuleCategories(PrMediiiContext context,
            string[] selectedCategories, Module moduleToUpdate)
        {
            if (selectedCategories == null)
            {
                moduleToUpdate.CourseCategories = new List<CourseCategory>();
                return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var moduleCategories = new HashSet<int>
                (moduleToUpdate.CourseCategories.Select(c => c.Course.ID));
            foreach (var cat in context.Course)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!moduleCategories.Contains(cat.ID))
                    {
                        moduleToUpdate.CourseCategories.Add(
                            new CourseCategory
                            {
                                ModuleID = moduleToUpdate.ID,
                                CourseID = cat.ID
                            });
                    }
                }
                else
                {
                    if (moduleCategories.Contains(cat.ID))
                    {
                        CourseCategory courseToRemove
                            = moduleToUpdate
                            .CourseCategories
                            .SingleOrDefault(i => i.CourseID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }

        }
    }
}