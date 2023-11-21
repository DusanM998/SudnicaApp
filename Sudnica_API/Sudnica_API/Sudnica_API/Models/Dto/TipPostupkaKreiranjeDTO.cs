using System.ComponentModel.DataAnnotations;

namespace Sudnica_API.Models.Dto
{
    public class TipPostupkaKreiranjeDTO
    {
        [Key]
        public int Id { get; set; }
        public string Naslov { get; set; }
    }
}
