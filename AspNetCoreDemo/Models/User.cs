using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Models
{
    public class User
    {        
        public int UserId { get; set; }
        
        public string Name { get; set; }
        
        public string Address { get; set; }
        
        public long ContactNo { get; set; }
        
        public string Email { get; set; }
    }
}
