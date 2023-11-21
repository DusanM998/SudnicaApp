using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sudnica_API.Models
{
    public class Kontakt
    {
        [Key]
        public int Id { get; set; }
        [Required] 
        public string Ime { get; set; }
        public string Telefon1 { get; set; }
        public string Telefon2 { get; set; }
        public string Adresa { get; set; }
        public string Email { get; set; }
        public bool PravnoFizickoLice { get; set; }
        public string Zanimanje { get; set; }
        public int KompanijaId { get; set; }
        [ForeignKey("KompanijaId")]
        public Kompanija PripadnostKompaniji { get; set; }
    }
}
