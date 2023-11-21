using System.ComponentModel.DataAnnotations;

namespace Sudnica_API_Test.Models.Dto
{
    public class LokacijaZaKreiranjeDTO
    {
        [Key]
        public int Id { get; set; }
        public string Naslov { get; set; }
    }
}
