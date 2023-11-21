using SudnicaAPI_Test.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sudnica_API.Models.Dto
{
    public class ParnicaKreiranjeDTO
    {
        public DateTime DatumOdrzavanja { get; set; }
        public int LokacijaId { get; set; }
        
        public int SudijaId { get; set; }
        
        public string TipUstanove { get; set; }
        public string IdentifikatorPostupka { get; set; }
        public int BrojSudnice { get; set; }

        public int TuzilacId { get; set; }
        
        public int TuzenikId { get; set; }
        
        public IEnumerable<RegistrujAdvokataDTO> ZaduzeniAdvokatiDTO { get; set; }
        public string Napomena { get; set; }
        public int TipPostupkaId { get; set; }
        
    }
}
