﻿namespace Sudnica_API_Test.Models.Dto
{
    public class ParnicaAzuriranjeDTO
    {
        public int ParnicaId { get; set; }
        public DateTime DatumOdrzavanja { get; set; }
        public int LokacijaId { get; set; }

        public int SudijaId { get; set; }

        public string TipUstanove { get; set; }
        public string IdentifikatorPostupka { get; set; }
        public int BrojSudnice { get; set; }

        public int TuzilacId { get; set; }

        public int TuzenikId { get; set; }

        //public ICollection<RegistracijaDTO> ZaduzeniAdvokatiDTO { get; set; }
        public string Napomena { get; set; }
        public int TipPostupkaId { get; set; }
    }
}