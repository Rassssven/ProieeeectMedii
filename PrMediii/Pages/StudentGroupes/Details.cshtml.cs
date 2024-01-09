using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrMediii.Data;
using PrMediii.Modules;

namespace PrMediii.Pages.StudentGroupes
{
    public class DetailsModel : PageModel
    {
        private readonly PrMediii.Data.PrMediiiContext _context;

        public DetailsModel(PrMediii.Data.PrMediiiContext context)
        {
            _context = context;
        }

      public StudentGroups StudentGroups { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.StudentGroups == null)
            {
                return NotFound();
            }

            var studentgroups = await _context.StudentGroups.FirstOrDefaultAsync(m => m.ID == id);
            if (studentgroups == null)
            {
                return NotFound();
            }
            else 
            {
                StudentGroups = studentgroups;
            }
            return Page();
        }
    }
}
