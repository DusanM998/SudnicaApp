using SudnicaAPI_Test.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sudnica_API.Models.Dto
{
    public class KontaktKreiranjeDTO
    {
        [Required]
        public string Ime { get; set; }
        public string Telefon1 { get; set; }
        public string Telefon2 { get; set; }
        public string Adresa { get; set; }
        public string Email { get; set; }
        public bool PravnoFizickoLice { get; set; }
        public string Zanimanje { get; set; }
        public int KompanijaId { get; set; }
    }
}
