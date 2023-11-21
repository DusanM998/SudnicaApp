using SudnicaAPI_Test.Models;
using System.ComponentModel.DataAnnotations;

namespace Sudnica_API.Models.Dto
{
    public class ZaduzeniAdvokatDTO : KorisnikDTO
    {
        [Required]
        public int ParnicaId { get; set; }
    }
}
