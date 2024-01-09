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
    public class EditModel : PageModel
    {
        private readonly PrMediii.Data.PrMediiiContext _context;

        public EditModel(PrMediii.Data.PrMediiiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StudentGroups StudentGroups { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.StudentGroups == null)
            {
                return NotFound();
            }

            var studentgroups =  await _context.StudentGroups.FirstOrDefaultAsync(m => m.ID == id);
            if (studentgroups == null)
            {
                return NotFound();
            }
            StudentGroups = studentgroups;
           ViewData["ModuleID"] = new SelectList(_context.Module, "ID", "ID");
           ViewData["StudentID"] = new SelectList(_context.Student, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(StudentGroups).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentGroupsExists(StudentGroups.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StudentGroupsExists(int id)
        {
          return (_context.StudentGroups?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
