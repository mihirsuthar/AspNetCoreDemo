using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Models
{
    public class User
    {
        [Key]
        [Display(Name = "User Id")]
        [Required(ErrorMessage = "Please enter User Id")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Please enter user name")]
        [StringLength(25, ErrorMessage = "Length of user name must not exceed 25 characters")]
        [Column(TypeName = "char")]
        [Display(Name ="User Name")]
        public string Name { get; set; }
        
        [Display(Name = "Address")]
        public string Address { get; set; }
        
        [Display(Name = "Contact Number")]
        [Phone(ErrorMessage = "Invalid contact number")]
        public long ContactNo { get; set; }
        
        [Display(Name = "Email Id")]
        public string Email { get; set; }
    }

    public class NumericValueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string val = value.ToString();
            int result = 0;
            long lngResult = 0;


            if (string.IsNullOrEmpty(val) || string.IsNullOrWhiteSpace(val))
            {   
                return new ValidationResult("Please enter valid value");
            }

            if (!int.TryParse(val, out result) && !long.TryParse(val, out lngResult))
            {
                return new ValidationResult("Please enter valid value");
            }

            return ValidationResult.Success;
        }
    }
}
