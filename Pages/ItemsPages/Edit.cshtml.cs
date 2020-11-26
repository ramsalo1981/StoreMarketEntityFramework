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
using Microsoft.EntityFrameworkCore;
using StoreMarket.Models;

namespace StoreMarket.Pages.ItemsPages
{
    public class EditModel : PageModel
    {
        private readonly StoreMarket.Models.StoreDbContext _context;
        private readonly IWebHostEnvironment Hosting;

        [BindProperty]
        public IFormFile RFile { get; set; }
        public EditModel(StoreMarket.Models.StoreDbContext context, IWebHostEnvironment Hosting)
        {
            _context = context;
           this.Hosting = Hosting;
        }

        [BindProperty]
        public Item Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item = await _context.Items
                .Include(i => i.Groups).FirstOrDefaultAsync(m => m.Id == id);

            if (Item == null)
            {
                return NotFound();
            }
           ViewData["GroupsId"] = new SelectList(_context.Groups, "Id", "GroupName");
            return Page();
        }

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

            _context.Attach(Item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(Item.Id))
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

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
