using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreDemo.Pages
{
    public class IndexModel : PageModel
    {
        UserContext _db;

        [BindProperty]
        public IEnumerable<User> Users { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }

        public IndexModel(UserContext db)
        {
            _db = db;
        }


        public void OnGet()
        {
            ErrorMessage = Request.Query["ErrorMessage"];

            try
            {
                using (_db)
                {
                    Users = _db.Users.ToList();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ErrorMessage = "Something went wrong. Please try again later.";
            }
        }
    }
}
