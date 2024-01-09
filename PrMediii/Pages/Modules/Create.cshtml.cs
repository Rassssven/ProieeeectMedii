using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrMediii.Data;
using PrMediii.Modules;

namespace PrMediii.Pages.Modules
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : ModuleCoursePageModel
    {
        private readonly PrMediii.Data.PrMediiiContext _context;

        public CreateModel(PrMediii.Data.PrMediiiContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["TrainerID"] = new SelectList(_context.Set<Trainer>(), "ID", "TrainerName");
            
            var module = new Module();
            module.CourseCategories = new List<CourseCategory>();
            PopulateAssignedCoursesData(_context, module);

            return Page();
        }

        [BindProperty]
        public Module Module { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newModule = new Module();
            if (selectedCategories != null)
            {
                newModule.CourseCategories = new List<CourseCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new CourseCategory
                    {
                        CourseID = int.Parse(cat)
                    };
                    newModule.CourseCategories.Add(catToAdd);
                }
            }
            Module.CourseCategories = newModule.CourseCategories;
            _context.Module.Add(Module);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
