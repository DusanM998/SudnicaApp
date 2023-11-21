using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sudnica_API_Test.Models;
using SudnicaAPI_Test.DbContexts;
using SudnicaAPI_Test.Models;
using System.Net;

namespace Sudnica_API_Test.Controllers
{
    [Route("api/korisnikParnica")]
    [ApiController]
    public class KorisnikParnicaController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ApiResponse _response;

        public KorisnikParnicaController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetParniceKorinik()
        {
            try
            {
                IEnumerable<KorisnikParnica> korisnikParnica = _db.KorisniciParnice
                    .Include(u => u.Korisnik)
                    .Include(u => u.Parnica)
                        .ThenInclude(u => u.Lokacija)
                    .Include(u => u.Parnica)
                        .ThenInclude(u => u.Sudija)
                        .ThenInclude(u => u.PripadnostKompaniji)
                    .Include(u => u.Parnica)
                        .ThenInclude(u => u.Tuzilac)
                        .ThenInclude(u => u.PripadnostKompaniji)
                    .Include(u => u.Parnica)
                        .ThenInclude(u => u.Tuzenik)
                        .ThenInclude(u => u.PripadnostKompaniji)
                    .Include(u => u.Parnica)
                        .ThenInclude(u => u.ZaduzeniAdvokati)
                    .Include(u => u.Parnica)
                        .ThenInclude(u => u.TipPostupka)
                    .OrderBy(u => u.Id);

                _response.Result = korisnikParnica;
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
