using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Sudnica_API.Models
{
    public class Korisnik : IdentityUser
    {
        public string PunoIme { get; set; }
        public int Godine { get; set; }
    }
}
