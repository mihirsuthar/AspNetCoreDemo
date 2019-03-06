using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreDemo.Pages
{
    public class NewUserModel : PageModel
    {
        UserContext _db;

        [BindProperty]
        public User user { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public string SuccessMessage { get; set; }

        [BindProperty]
        public IEnumerable<User> Users { get; set; }

        public NewUserModel(UserContext db)
        {
            _db = db;
            ErrorMessage = "";
            SuccessMessage = "";
        }

        public void OnGetUsers()
        {
            ResetNotificationMessages();
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

        public IActionResult OnPostAddUser()
        {
            ResetNotificationMessages();
            try
            {
                using (_db)
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                }
                SuccessMessage = "User Added Successfully.";

                return RedirectToPage("Index", new { SuccessMessage = "User added successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ErrorMessage = "Something went wrong. Please try again later.";
                return RedirectToPage("Index", new { ErrorMessage = "Something went wrong. Please try again later." });
            }
        }

        private void ResetNotificationMessages()
        {
            ErrorMessage = "";
            SuccessMessage = "";
        }
    }
}