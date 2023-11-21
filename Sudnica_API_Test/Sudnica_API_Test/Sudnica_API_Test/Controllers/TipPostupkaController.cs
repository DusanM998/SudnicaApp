using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Sudnica_API_Test.Models.Dto;
using SudnicaAPI_Test.DbContexts;
using SudnicaAPI_Test.Models;
using System.Net;

namespace SudnicaAPI_Test.Controllers
{
    [Route("api/tipPostupka")]
    public class TipPostupkaController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ApiResponse _response;

        public TipPostupkaController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetTipoviPostupaka()
        {
            try
            {
                IEnumerable<TipPostupka> tipoviPostupaka = _db.TipoviPostupaka
                    .OrderBy(tp => tp.Id);

                _response.Result = tipoviPostupaka;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_db.TipoviPostupaka);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetTipPostupkaById")]
        public async Task<IActionResult> GetTipPostupkaById(int id)
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            TipPostupka tipPostupka = _db.TipoviPostupaka.FirstOrDefault(x => x.Id == id);

            if (tipPostupka == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.Result = tipPostupka;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> KreirajTipPostupka([FromForm] TipPostupkaKreiranjeDTO tipPostupkaKreiranjeDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TipPostupka tipPostupka = new()
                    {
                        Naslov = tipPostupkaKreiranjeDTO.Naslov
                    };
                    _db.TipoviPostupaka.Add(tipPostupka);
                    _db.SaveChanges();
                    _response.Result = tipPostupka;
                    _response.StatusCode = HttpStatusCode.Created;
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> AzurirajTipPostupka(int id, [FromForm] TipPostupkaAzuriranjeDTO tipPostupkaAzuriranjeDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (tipPostupkaAzuriranjeDTO == null || id != tipPostupkaAzuriranjeDTO.Id)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    TipPostupka tipPostupka = await _db.TipoviPostupaka.FindAsync(id);

                    if (tipPostupka == null)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    tipPostupka.Naslov = tipPostupkaAzuriranjeDTO.Naslov;

                    _db.TipoviPostupaka.Update(tipPostupka);
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
        public async Task<ActionResult<ApiResponse>> ObrisiTipPostupka(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                TipPostupka tipPostupka = await _db.TipoviPostupaka.FindAsync(id);

                if (tipPostupka == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                _db.TipoviPostupaka.Remove(tipPostupka);
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
