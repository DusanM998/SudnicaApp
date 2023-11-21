using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sudnica_API.DbContexts;
using Sudnica_API.Models;
using Sudnica_API.Models.Dto;
using SudnicaAPI_Test.Models;
using System.Net;

namespace Sudnica_API.Controllers
{
    [Route("api/kontakt")]
    [ApiController]
    public class KontaktController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ApiResponse _response;

        public KontaktController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetKontakti()
        {
            try
            {
                IEnumerable<Kontakt> kontakti = _db.Kontakti
                    .Include(u => u.PripadnostKompaniji)
                    .OrderBy(u => u.Id);
                    

                _response.Result = kontakti;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return _response;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse>> GetKontaktById(int id)
        {
            try
            {
                if(id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var kontakti = _db.Kontakti
                    .Include(u => u.PripadnostKompaniji)
                    .Where(u => u.Id == id);

                if(kontakti == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = kontakti;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> KreirajKontakt([FromBody] KontaktKreiranjeDTO kontaktKreiranjeDTO)
        {
            try
            {
                Kontakt kontakt = new()
                {
                    Ime = kontaktKreiranjeDTO.Ime,
                    Telefon1 = kontaktKreiranjeDTO.Telefon1,
                    Telefon2 = kontaktKreiranjeDTO.Telefon2,
                    Adresa = kontaktKreiranjeDTO.Adresa,
                    Email = kontaktKreiranjeDTO.Email,
                    PravnoFizickoLice = kontaktKreiranjeDTO.PravnoFizickoLice,
                    Zanimanje = kontaktKreiranjeDTO.Zanimanje,
                    KompanijaId = kontaktKreiranjeDTO.KompanijaId
                };

                if (ModelState.IsValid)
                {
                    _db.Kontakti.Add(kontakt);
                    _db.SaveChanges();
                }
                _db.SaveChanges();
                _response.Result = kontakt;
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> AzurirajKontakt(int id, [FromBody] KontaktAzuriranjeDTO kontaktAzuriranjeDTO)
        {
            try
            {
                if(kontaktAzuriranjeDTO == null || id != kontaktAzuriranjeDTO.Id)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                Kontakt kontaktIzBaze = _db.Kontakti.FirstOrDefault(u => u.Id == id);

                if(kontaktIzBaze == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }
                if (!string.IsNullOrEmpty(kontaktAzuriranjeDTO.Ime))
                {
                    kontaktIzBaze.Ime = kontaktAzuriranjeDTO.Ime;
                }
                if (!string.IsNullOrEmpty(kontaktAzuriranjeDTO.Telefon1))
                {
                    kontaktIzBaze.Telefon1 = kontaktAzuriranjeDTO.Telefon1;
                }
                if (!string.IsNullOrEmpty(kontaktAzuriranjeDTO.Telefon2))
                {
                    kontaktIzBaze.Telefon2 = kontaktAzuriranjeDTO.Telefon2;
                }
                if (!string.IsNullOrEmpty(kontaktAzuriranjeDTO.Adresa))
                {
                    kontaktIzBaze.Adresa = kontaktAzuriranjeDTO.Adresa;
                }
                if (!string.IsNullOrEmpty(kontaktAzuriranjeDTO.Email))
                {
                    kontaktIzBaze.Email = kontaktAzuriranjeDTO.Email;
                }
                if (!string.IsNullOrEmpty(kontaktAzuriranjeDTO.Zanimanje))
                {
                    kontaktIzBaze.Zanimanje = kontaktAzuriranjeDTO.Zanimanje;
                }

                _db.SaveChanges();
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

    }
}
