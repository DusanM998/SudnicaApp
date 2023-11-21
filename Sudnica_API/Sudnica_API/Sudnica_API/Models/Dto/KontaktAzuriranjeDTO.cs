namespace Sudnica_API.Models.Dto
{
    public class KontaktAzuriranjeDTO
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Telefon1 { get; set; }
        public string Telefon2 { get; set; }
        public string Adresa { get; set; }
        public string Email { get; set; }
        public bool PravnoFizickoLice { get; set; }
        public string Zanimanje { get; set; }
    }
}
