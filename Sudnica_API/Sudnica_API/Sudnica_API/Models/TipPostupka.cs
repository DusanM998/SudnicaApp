using System.ComponentModel.DataAnnotations;

namespace Sudnica_API.Models
{
    public class TipPostupka
    {
        [Key]
        public int Id { get; set; }
        public string Naslov { get; set; }
    }
}
