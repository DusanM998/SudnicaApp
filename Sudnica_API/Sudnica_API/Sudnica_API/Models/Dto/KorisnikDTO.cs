using Microsoft.AspNetCore.Identity;

namespace SudnicaAPI_Test.Models
{
    public class KorisnikDTO : IdentityUser
    {
        public string PunoIme { get; set; }
        public int Godine { get; set; }
    }
}
