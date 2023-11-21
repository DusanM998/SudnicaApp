using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SudnicaAPI_Test.Models
{
    public class Parnica
    {
        [Key]
        public int ParnicaId { get; set; }
        public DateTime DatumOdrzavanja { get; set; }
        public int LokacijaId { get; set; }
        [ForeignKey("LokacijaId")]
        public Lokacija Lokacija { get; set; }
        public int SudijaId { get; set; }
        [ForeignKey("SudijaId")]
        public virtual Kontakt Sudija { get; set; } 
        public string TipUstanove { get; set; }
        public string IdentifikatorPostupka { get; set; }
        public int BrojSudnice { get; set; }

        public int TuzilacId { get; set; }
        [ForeignKey("TuzilacId")]
        public virtual Kontakt Tuzilac { get; set; } 

        public int TuzenikId { get; set; }
        [ForeignKey("TuzenikId")]
        public virtual Kontakt Tuzenik { get; set; } 

        public virtual ICollection<Korisnik> ZaduzeniAdvokati { get; set; }
        public string Napomena { get; set; }
        public int TipPostupkaId { get; set; }
        [ForeignKey("TipPostupkaId")]
        public TipPostupka TipPostupka { get; set; }
    }
}
