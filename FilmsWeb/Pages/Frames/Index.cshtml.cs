using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FilmsLibrary.Contexts;
using FilmsLibrary.Models;

namespace FilmsWeb.Pages.Frames
{
    public class IndexModel : PageModel
    {
        private readonly FilmsLibrary.Contexts.FilmsDbContext _context;

        public IndexModel(FilmsLibrary.Contexts.FilmsDbContext context)
        {
            _context = context;
        }

        public IList<Frame> Frame { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Frame = await _context.Frames
                .Include(f => f.Film).ToListAsync();
        }
    }
}
