using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FilmsLibrary.Contexts;
using FilmsLibrary.Models;

namespace FilmsWeb.Pages.Films
{
    public class IndexModel : PageModel
    {
        private readonly FilmsLibrary.Contexts.FilmsDbContext _context;

        public IndexModel(FilmsLibrary.Contexts.FilmsDbContext context)
        {
            _context = context;
        }

        public IList<Film> Film { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Film = await _context.Films.ToListAsync();
        }
    }
}
