using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AspNetCoreDemo.Models;

namespace AspNetCoreDemo.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly AspNetCoreDemo.Models.UserContext _context;

        public IndexModel(AspNetCoreDemo.Models.UserContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; }

        public async Task OnGetAsync()
        {
            User = await _context.Users.ToListAsync();
        }
    }
}
