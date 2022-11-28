using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAuthAppItra.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        
        // [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginTime { get; set;}
        public bool IsBlocked { get; set; }

        //[(DisplayValue = false)]
        public bool IsChecked { get; set; }
    }
}
