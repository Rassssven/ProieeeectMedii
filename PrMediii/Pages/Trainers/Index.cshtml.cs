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

namespace PrMediii.Pages.Trainers
{
    public class IndexModel : PageModel
    {
        private readonly PrMediii.Data.PrMediiiContext _context;

        public IndexModel(PrMediii.Data.PrMediiiContext context)
        {
            _context = context;
        }

        public IList<Trainer> Trainer { get;set; } = default!;

        public TrainerIndexData TrainerData { get; set; }
        public int TrainerID { get; set; }
        public int ModuleID { get; set; }

        public async Task OnGetAsync(int? id, int? moduleID)
        {
            TrainerData = new TrainerIndexData();

            TrainerData.Trainers = await _context.Trainer
            .Include(i => i.Modules)
            .OrderBy(i => i.TrainerName)
            .ToListAsync();

            if (id != null)
            {
                TrainerID = id.Value;
                Trainer trainer = TrainerData.Trainers
                .Where(i => i.ID == id.Value).Single();
                TrainerData.Modules = trainer.Modules;
            }
        }
    }
}
