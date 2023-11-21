using System.ComponentModel.DataAnnotations;

namespace Sudnica_API.Models
{
    public class ZaduzeniAdvokat : Korisnik
    {
        [Required]
        public int ParnicaId { get; set; }
    }
}
