using System.ComponentModel.DataAnnotations;

namespace SudnicaAPI_Test.Models
{
    public class Kompanija
    {
        [Key] 
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }

    }
}
