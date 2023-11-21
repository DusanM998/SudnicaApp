namespace Sudnica_API.Models.Dto
{
    public class RegistrujAdvokataDTO
    {
        public string UserName { get; set; }
        public string PunoIme { get; set; }
        public string Lozinka { get; set; }
        public string Role { get; set; }
        public int Godine { get; set; }
        public int ParnicaId { get; set; }
    }
}
