using System.ComponentModel.DataAnnotations;

namespace SudnicaAPI_Test.Models
{
    public class Lokacija
    {
        [Key]
        public int Id { get; set; }
        public string Naslov { get; set; }

    }
}
