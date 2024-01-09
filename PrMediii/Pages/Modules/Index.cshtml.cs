using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrMediii.Data;
using PrMediii.Modules;

namespace PrMediii.Pages.Modules
{
    public class IndexModel : PageModel
    {
        private readonly PrMediii.Data.PrMediiiContext _context;

        public IndexModel(PrMediii.Data.PrMediiiContext context)
        {
            _context = context;
        }

        public IList<Module> Module { get; set; } = default!;
        public CourseData ModuleD { get; set; }
        public int ModuleID { get; set; }
        public int CoursesID { get; set; }

        public string TitleSort { get; set; }
        public string TrainerSort { get; set; }

        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? id, int? courseID, string sortOrder, string searchString)
        {
            ModuleD = new CourseData();

            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            TrainerSort = sortOrder == "trainer" ? "trainer_desc" : "trainer";

            CurrentFilter = searchString;

            ModuleD.Modules = await _context.Module
                .Include(b => b.Trainer)
                .Include(b => b.CourseCategories)
                .ThenInclude(b => b.Course)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                ModuleD.Modules = ModuleD.Modules.Where(s => s.Trainer.TrainerName.Contains(searchString)

               || s.Trainer.TrainerName.Contains(searchString)
               || s.Title.Contains(searchString));

                if (id != null)
                {
                    ModuleID = id.Value;
                    Module module = ModuleD.Modules
                    .Where(i => i.ID == id.Value).Single();
                    ModuleD.Courses = module.CourseCategories.Select(s => s.Course);
                }

                switch (sortOrder)
                {
                    case "title_desc":
                        ModuleD.Modules = ModuleD.Modules.OrderByDescending(s =>
                       s.Title);
                        break;
                    case "trainer_desc":
                        ModuleD.Modules = ModuleD.Modules.OrderByDescending(s =>
                       s.Trainer.TrainerName);
                        break;
                    case "trainer":
                        ModuleD.Modules = ModuleD.Modules.OrderBy(s =>
                       s.Trainer.TrainerName);
                        break;
                    default:
                        ModuleD.Modules = ModuleD.Modules.OrderBy(s => s.Title);
                        break;

                }
            }
        }
    }
}
