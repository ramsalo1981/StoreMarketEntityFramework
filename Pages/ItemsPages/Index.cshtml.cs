using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreMarket.Models;

namespace StoreMarket.Pages.ItemsPages
{
    public class IndexModel : PageModel
    {
        private readonly StoreMarket.Models.StoreDbContext _context;
        [BindProperty]
        public string SearchName { get; set; }
        public IndexModel(StoreMarket.Models.StoreDbContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; }

        public async Task OnGetAsync()
        {
            Item = await _context.Items
                .Include(i => i.Groups).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (SearchName == null)
            {
                Item = await _context.Items
                .Include(i => i.Groups).ToListAsync();
                return Page();
            }

            // Search by item name (1)
            //Item = await _context.Items
            //    .Include(i => i.Groups)
            //    .Where(s =>s.ItemName.Contains(SearchName))
            //    .ToListAsync();

            // search by item price (2)
            //Item = await _context.Items
            //    .Include(i => i.Groups)
            //    .Where(s => s.ItemPrice == Convert.ToDecimal(SearchName) )
            //    .ToListAsync();

            //serch by both itemname and itemprice and groupname (3)
            decimal d = 0;
            var result = decimal.TryParse(SearchName, out d);
            if (result == true)
            {
                Item = await _context.Items
                .Include(i => i.Groups)
                .Where(s => s.ItemPrice == Convert.ToDecimal(SearchName))
                .ToListAsync();
            }
            else
            {
                Item = await _context.Items
                .Include(i => i.Groups)
                .Where(s => s.ItemName.Contains(SearchName) || s.Groups.GroupName.Contains(SearchName))
                .ToListAsync();
            }

            return Page();
        }
    }
}
