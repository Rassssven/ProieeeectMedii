using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrMediii.Data;
using PrMediii.Modules;

namespace PrMediii.Pages.StudentGroupes
{
    public class CreateModel : PageModel
    {
        private readonly PrMediii.Data.PrMediiiContext _context;

        public CreateModel(PrMediii.Data.PrMediiiContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var moduleList = _context.Module
                .Include(b => b.Trainer)
                .Select(x => new
                {
                     x.ID,
                     ModuleFullName = x.Title + " - " + x.Trainer.TrainerName
                });

            ViewData["ModuleID"] = new SelectList(moduleList, "ID", "ModuleFullName");
            ViewData["StudentID"] = new SelectList(_context.Student, "ID", "FullName");
            return Page();
        }

        [BindProperty]
        public StudentGroups StudentGroups { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.StudentGroups == null || StudentGroups == null)
            {
                return Page();
            }

            _context.StudentGroups.Add(StudentGroups);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
