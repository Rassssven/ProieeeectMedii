using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrMediii.Data;
using PrMediii.Modules;


namespace PrMediii.Pages.Modules
{
    [Authorize(Roles = "Admin")]
    public class EditModel : ModuleCoursePageModel
    {
        private readonly PrMediii.Data.PrMediiiContext _context;

        public EditModel(PrMediii.Data.PrMediiiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Module Module { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = await _context.Module
                .Include(b => b.Trainer)
                .Include(b => b.CourseCategories).ThenInclude(b => b.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (module == null)
            {
                return NotFound();
            }

            PopulateAssignedCoursesData(_context, Module);
            ViewData["TrainerID"] = new SelectList(_context.Set<Trainer>(), "ID", "TrainerName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {

            if (id == null)
            {
                return NotFound();
            }

            var moduleToUpdate = await _context.Module
            .Include(i => i.Trainer)
            .Include(i => i.CourseCategories)
                .ThenInclude(i => i.Course)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (moduleToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Module>(
                moduleToUpdate,
                "Module",
                i => i.Title, i => i.Price,
                i => i.Duration, i => i.PublishingDate, i => i.TrainerID))
            {
                UpdateModuleCategories(_context, selectedCategories, moduleToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateModuleCategories(_context, selectedCategories, moduleToUpdate);
            PopulateAssignedCoursesData(_context, moduleToUpdate);
            return Page();

        }
    }
}
