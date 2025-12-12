using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FilmsLibrary.Contexts;
using FilmsLibrary.Models;

namespace FilmsWeb.Pages.Frames
{
    public class CreateModel : PageModel
    {
        private readonly FilmsLibrary.Contexts.FilmsDbContext _context;

        public CreateModel(FilmsLibrary.Contexts.FilmsDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FilmId"] = new SelectList(_context.Films, "FilmId", "Name");
            return Page();
        }

        [BindProperty]
        public Frame Frame { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Frame.Film");
            ModelState.Remove("Frame.FilmName");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var file = HttpContext.Request.Form.Files.FirstOrDefault();

            if (file?.Length > 0 && file?.Length >> 20 <= 2)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                Frame.FileName = file.FileName;
            }

            _context.Frames.Add(Frame);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
