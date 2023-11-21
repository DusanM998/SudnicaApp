using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sudnica_API_Test.Models;
using Sudnica_API_Test.Models.Dto;
using Sudnica_API_Test.Utility;
using SudnicaAPI_Test.DbContexts;
using SudnicaAPI_Test.Models;
using System.Net;

namespace Sudnica_API_Test.Controllers
{
    [Route("api/parnica")]
    [ApiController]
    public class ParnicaController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ApiResponse _response;

        public ParnicaController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        [HttpPost]
        public virtual async Task<ActionResult<ApiResponse>> KreirajParnicu([FromForm] ParnicaKreiranjeDTO parnicaKreiranjeDTO)
        {
            try
            {
                Parnica parnica = new()
                {
                    DatumOdrzavanja = parnicaKreiranjeDTO.DatumOdrzavanja,
                    LokacijaId = parnicaKreiranjeDTO.LokacijaId,
                    SudijaId = parnicaKreiranjeDTO.SudijaId,
                    TipUstanove = String.IsNullOrEmpty(parnicaKreiranjeDTO.TipUstanove) ? SD.Default_Tip : parnicaKreiranjeDTO.TipUstanove,
                    IdentifikatorPostupka = parnicaKreiranjeDTO.IdentifikatorPostupka,
                    BrojSudnice = parnicaKreiranjeDTO.BrojSudnice,
                    TuzilacId = parnicaKreiranjeDTO.TuzilacId,
                    TuzenikId = parnicaKreiranjeDTO.TuzenikId,
                    Napomena = parnicaKreiranjeDTO.Napomena,
                    TipPostupkaId = parnicaKreiranjeDTO.TipPostupkaId,
                };

                if(ModelState.IsValid)
                {
                    _db.Parnice.Add(parnica);
                    _db.SaveChanges();
                    foreach(var korisnikDTO in parnicaKreiranjeDTO.ZaduzeniAdvokatiDTO)
                    {
                        Korisnik zaduzeniAdvokati = new()
                        {
                            UserName = korisnikDTO.UserName,
                            PunoIme = korisnikDTO.PunoIme,
                            Godine = korisnikDTO.Godine,
                            Email = korisnikDTO.UserName,
                            NormalizedEmail = korisnikDTO.UserName.ToUpper(),
                        };
                        _db.Korisnici.Add(zaduzeniAdvokati);

                        KorisnikParnica korisnikParnica = new()
                        {
                            ParnicaId = parnica.ParnicaId,
                            Parnica = parnica,
                            KorisnikId = zaduzeniAdvokati.Id,
                            Korisnik = zaduzeniAdvokati,
                        };

                        _db.KorisniciParnice.Add(korisnikParnica);
                    }
                    _db.SaveChanges();
                    _response.Result = parnica;
                    _response.StatusCode = HttpStatusCode.Created;
                    return Ok(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetParnice()
        {
            try
            {
                ICollection<Parnica> parnice = _db.Parnice
                    .Include(u => u.Sudija)
                    .ThenInclude(u => u.PripadnostKompaniji)
                    .Include(u => u.Lokacija)
                    .Include(u => u.Tuzilac)
                    .ThenInclude(u => u.PripadnostKompaniji)
                    .Include(u => u.Tuzenik)
                    .ThenInclude(u => u.PripadnostKompaniji)
                    .Include(u => u.ZaduzeniAdvokati)
                    .Include(u => u.TipPostupka)
                    .OrderBy(u => u.ParnicaId)
                    .ToList();

                _response.Result = parnice;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetParnicaById")]
        public async Task<ActionResult<ApiResponse>> GetParnicaById(int id)
        {
            if(id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            var parnica = _db.Parnice
                .Include(u => u.Sudija)
                .ThenInclude(u => u.PripadnostKompaniji)
                .Include(u => u.Lokacija)
                .Include(u => u.Tuzilac)
                .ThenInclude(u => u.PripadnostKompaniji)
                .Include(u => u.Tuzenik)
                .ThenInclude(u => u.PripadnostKompaniji)
                .Include(u => u.ZaduzeniAdvokati)
                .Include(u => u.TipPostupka)
                .Where(u => u.ParnicaId == id);

            if(parnica == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.Result = parnica;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> DeleteParnica(int id)
        {
            try
            {
                if(id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                Parnica parnica = await _db.Parnice.FindAsync(id);

                if(parnica == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                _db.Parnice.Remove(parnica);
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> AzurirajParnicu(int id, [FromForm] ParnicaAzuriranjeDTO parnicaAzuriranjeDTO)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(parnicaAzuriranjeDTO == null || id != parnicaAzuriranjeDTO.ParnicaId)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    Parnica parnica = await _db.Parnice.FindAsync(id);

                    if(parnica == null)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    parnica.DatumOdrzavanja = parnicaAzuriranjeDTO.DatumOdrzavanja;
                    parnica.LokacijaId = parnicaAzuriranjeDTO.LokacijaId;
                    parnica.SudijaId = parnicaAzuriranjeDTO.SudijaId;
                    parnica.TipUstanove = parnica.TipUstanove;
                    parnica.IdentifikatorPostupka = parnicaAzuriranjeDTO.IdentifikatorPostupka;
                    parnica.BrojSudnice = parnicaAzuriranjeDTO.BrojSudnice;
                    parnica.TuzilacId = parnicaAzuriranjeDTO.TuzilacId;
                    parnica.TuzenikId = parnicaAzuriranjeDTO.TuzenikId;
                    parnica.Napomena = parnicaAzuriranjeDTO.Napomena;
                    parnica.TipPostupkaId = parnicaAzuriranjeDTO.TipPostupkaId;

                    _db.Parnice.Update(parnica);
                    _db.SaveChanges();
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }

            return _response;
        }
    }
}
