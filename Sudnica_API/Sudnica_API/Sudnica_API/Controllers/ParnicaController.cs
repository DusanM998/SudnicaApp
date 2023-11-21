using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sudnica_API.DbContexts;
using Sudnica_API.Models;
using Sudnica_API.Models.Dto;
using Sudnica_API.Utility;
using Sudnica_API_Test.Models;
using SudnicaAPI_Test.Models;
using System.Net;

namespace Sudnica_API.Controllers
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

                if (ModelState.IsValid)
                {
                    _db.Parnice.Add(parnica);
                    _db.SaveChanges();
                    foreach (var advokatDTO in parnicaKreiranjeDTO.ZaduzeniAdvokatiDTO)
                    {
                        ZaduzeniAdvokat zaduzeniAdvokati = new()
                        {
                            ParnicaId = advokatDTO.ParnicaId,
                            UserName = advokatDTO.UserName,
                            PunoIme = advokatDTO.PunoIme,
                            Godine = advokatDTO.Godine,
                            Email = advokatDTO.UserName,
                            NormalizedEmail = advokatDTO.UserName.ToUpper(),
                        };
                        _db.Korisnici.Add(zaduzeniAdvokati);

                        KorisnikParnica korisnikParnica = new()
                        {
                            ParnicaId = parnica.ParnicaId,
                            Parnica = parnica,
                            ZaduzeniAdvokatId = zaduzeniAdvokati.Id,
                            ZaduzeniAdvokat = zaduzeniAdvokati,
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
                var parnice = _db.Parnice
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
    }
}
