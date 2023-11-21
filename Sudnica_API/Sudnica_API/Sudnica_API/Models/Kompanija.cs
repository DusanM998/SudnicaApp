using System.ComponentModel.DataAnnotations;

namespace Sudnica_API.Models
{
    public class Kompanija
    {
        [Key] 
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }

    }
}
