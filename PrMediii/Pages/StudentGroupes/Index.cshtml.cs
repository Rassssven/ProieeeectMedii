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
    public class IndexModel : PageModel
    {
        private readonly PrMediii.Data.PrMediiiContext _context;

        public IndexModel(PrMediii.Data.PrMediiiContext context)
        {
            _context = context;
        }

        public IList<StudentGroups> StudentGroups { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.StudentGroups != null)
            {
                StudentGroups = await _context.StudentGroups
                .Include(s => s.Module)
                    .ThenInclude(b => b.Trainer)
                .Include(s => s.Student).ToListAsync();
            }
        }
    }
}
