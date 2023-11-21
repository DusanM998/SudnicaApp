using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sudnica_API_Test.Models.Dto;
using SudnicaAPI_Test.DbContexts;
using SudnicaAPI_Test.Models;
using System.Net;

namespace Sudnica_API_Test.Controllers
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

        [HttpGet("{id:int}", Name = "GetKontaktById")]
        public async Task<IActionResult> GetKontaktById(int id)
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            var kontakti = _db.Kontakti
                .Include(u => u.PripadnostKompaniji)
                .Where(u => u.Id == id);

            if (kontakti == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.Result = kontakti;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> KreirajKontakt([FromForm] KontaktKreiranjeDTO kontaktKreiranjeDTO)
        {
            try
            {
                if(ModelState.IsValid)
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
                    _db.Kontakti.Add(kontakt);
                    _db.SaveChanges();
                    _response.Result = kontakt;
                    _response.StatusCode = HttpStatusCode.Created;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false; 
                    return BadRequest();
                }
                
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        /*[HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> AzurirajKontaktFromBody(int id, [FromBody] KontaktAzuriranjeDTO kontaktAzuriranjeDTO)
        {
            try
            {
                if (kontaktAzuriranjeDTO == null || id != kontaktAzuriranjeDTO.Id)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                Kontakt kontaktIzBaze = _db.Kontakti.FirstOrDefault(u => u.Id == id);

                if (kontaktIzBaze == null)
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
        }*/

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> AzurirajKontakt(int id, [FromForm] KontaktAzuriranjeDTO kontaktAzuriranjeDTO)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(kontaktAzuriranjeDTO == null || id!=kontaktAzuriranjeDTO.Id)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    Kontakt kontaktIzBaze = await _db.Kontakti.FindAsync(id);

                    if(kontaktIzBaze == null)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    kontaktIzBaze.Ime = kontaktAzuriranjeDTO.Ime;
                    kontaktIzBaze.Telefon1 = kontaktAzuriranjeDTO.Telefon1;
                    kontaktIzBaze.Telefon1 = kontaktAzuriranjeDTO.Telefon2;
                    kontaktIzBaze.Adresa = kontaktAzuriranjeDTO.Adresa;
                    kontaktIzBaze.Email = kontaktAzuriranjeDTO.Email;
                    kontaktIzBaze.PravnoFizickoLice = kontaktAzuriranjeDTO.PravnoFizickoLice;
                    kontaktIzBaze.Zanimanje = kontaktAzuriranjeDTO.Zanimanje;
                    kontaktIzBaze.KompanijaId = kontaktAzuriranjeDTO.KompanijaId;

                    _db.Kontakti.Update(kontaktIzBaze);
                    _db.SaveChanges();
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }

            return _response;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> DeleteKontakt(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                Kontakt kontaktIzBaze = await _db.Kontakti.FindAsync(id);

                if (kontaktIzBaze == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                _db.Kontakti.Remove(kontaktIzBaze);
                _db.SaveChanges();
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }

            return _response;
        }
    }
}
