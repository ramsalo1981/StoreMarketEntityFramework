using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreMarket.Models;

namespace StoreMarket.Pages.ItemsPages
{
    public class CreateModel : PageModel
    {
        private readonly StoreMarket.Models.StoreDbContext _context;
        private readonly IWebHostEnvironment Hosting;

        [BindProperty]
        public IFormFile RFile { get; set; }
        public CreateModel(StoreMarket.Models.StoreDbContext context, IWebHostEnvironment Hosting)
        {
            _context = context;
           this.Hosting = Hosting;
        }

        public IActionResult OnGet()
        {
        ViewData["GroupsId"] = new SelectList(_context.Groups, "Id", "GroupName");
            return Page();
        }

        [BindProperty]
        public Item Item { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (RFile.FileName != null)
            {
                var FName = Path.GetFileName(RFile.FileName);
                var NewPath = Path.Combine(Hosting.WebRootPath, "Images", FName);
                RFile.CopyTo(new FileStream(NewPath, FileMode.Create));
                Item.ItemImage = FName;
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Items.Add(Item);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
