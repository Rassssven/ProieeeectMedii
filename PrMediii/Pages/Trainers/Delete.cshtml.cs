﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrMediii.Data;
using PrMediii.Modules;

namespace PrMediii.Pages.Trainers
{
    public class DeleteModel : PageModel
    {
        private readonly PrMediii.Data.PrMediiiContext _context;

        public DeleteModel(PrMediii.Data.PrMediiiContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Trainer Trainer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Trainer == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainer.FirstOrDefaultAsync(m => m.ID == id);

            if (trainer == null)
            {
                return NotFound();
            }
            else 
            {
                Trainer = trainer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Trainer == null)
            {
                return NotFound();
            }
            var trainer = await _context.Trainer.FindAsync(id);

            if (trainer != null)
            {
                Trainer = trainer;
                _context.Trainer.Remove(Trainer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
