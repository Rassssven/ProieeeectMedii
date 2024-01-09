using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrMediii.Data;
using PrMediii.Modules;
using PrMediii.Modules.ViewModels;

namespace PrMediii.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly PrMediii.Data.PrMediiiContext _context;

        public IndexModel(PrMediii.Data.PrMediiiContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Course != null)
            {
                Course = await _context.Course.ToListAsync();
            }

        }
    }
}
