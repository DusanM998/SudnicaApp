using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Sudnica_API.DbContexts;
using Sudnica_API.Models;
using Sudnica_API.Models.Dto;
using SudnicaAPI_Test.Models;
using System.Net;

namespace Sudnica_API.Controllers
{
    [Route("api/lokacija")]
    public class LokacijaController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ApiResponse _response;

        public LokacijaController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetLokacije()
        {
            _response.Result = _db.Lokacije;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_db.Lokacije);
        }

        [HttpGet("{id:int}", Name = "GetLokacijaById")]
        public async Task<IActionResult> GetLokacijaById(int id)
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            Lokacija lokacija = _db.Lokacije.FirstOrDefault(k => k.Id == id);

            if (lokacija == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.Result = lokacija;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> KreirajLokaciju([FromBody] LokacijaZaKreiranjeDTO lokacijaZaKreiranjeDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Lokacija lokacijaZaKreiranje = new()
                    {
                        Naslov = lokacijaZaKreiranjeDTO.Naslov
                    };
                    _db.Lokacije.Add(lokacijaZaKreiranje);
                    _db.SaveChanges();
                    _response.Result = lokacijaZaKreiranje;
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("GetLokacijaById", new { id = lokacijaZaKreiranje.Id }, _response);
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> AzurirajLokaciju(int id, [FromForm] LokacijaZaAzuriranjeDTO lokacijaZaAzuriranjeDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (lokacijaZaAzuriranjeDTO == null || id != lokacijaZaAzuriranjeDTO.Id)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    Lokacija lokacijaIzBaze = await _db.Lokacije.FindAsync(id);

                    if (lokacijaIzBaze == null)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    lokacijaIzBaze.Naslov = lokacijaZaAzuriranjeDTO.Naslov;

                    _db.Lokacije.Update(lokacijaIzBaze);
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> ObrisiLokaciju(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                Lokacija lokacija = await _db.Lokacije.FindAsync(id);

                if (lokacija == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                _db.Lokacije.Remove(lokacija);
                _db.SaveChanges();
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
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
