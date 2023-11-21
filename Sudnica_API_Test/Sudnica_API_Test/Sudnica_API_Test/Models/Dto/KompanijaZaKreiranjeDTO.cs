using System.ComponentModel.DataAnnotations;

namespace Sudnica_API_Test.Models.Dto
{
    public class KompanijaZaKreiranjeDTO
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
    }
}
