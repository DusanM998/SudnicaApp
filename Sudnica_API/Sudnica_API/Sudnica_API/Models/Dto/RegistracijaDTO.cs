using System.ComponentModel.DataAnnotations.Schema;

namespace Sudnica_API.Models.Dto
{
    public class RegistracijaDTO
    {
        public string UserName { get; set; }
        public string Ime { get; set; }
        public string Lozinka { get; set; }
        public string Role { get; set; }
        public int ParnicaId { get; set; }
    }
}
