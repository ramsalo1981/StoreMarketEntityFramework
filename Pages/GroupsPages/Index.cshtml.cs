using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreMarket.Models;

namespace StoreMarket.Pages.GroupsPages
{
    public class IndexModel : PageModel
    {
        private readonly StoreMarket.Models.StoreDbContext _context;

        public IndexModel(StoreMarket.Models.StoreDbContext context)
        {
            _context = context;
        }

        public IList<Group> Group { get;set; }

        public async Task OnGetAsync()
        {
            Group = await _context.Groups.ToListAsync();
        }
    }
}
