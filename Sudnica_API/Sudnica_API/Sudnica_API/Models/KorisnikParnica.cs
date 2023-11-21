using Sudnica_API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sudnica_API_Test.Models
{
    public class KorisnikParnica
    {
        [Key]
        public int Id { get; set; }
        public string ZaduzeniAdvokatId { get; set; }
        [ForeignKey("ZaduzeniAdvokatId")]
        public ZaduzeniAdvokat ZaduzeniAdvokat { get; set; }

        public int ParnicaId { get; set; }
        [ForeignKey("ParnicaId")]
        public Parnica Parnica { get; set; }
    }
}
