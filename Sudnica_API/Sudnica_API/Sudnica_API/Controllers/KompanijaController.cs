﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Sudnica_API.DbContexts;
using Sudnica_API.Models;
using Sudnica_API.Models.Dto;
using SudnicaAPI_Test.Models;
using System.Net;

namespace Sudnica_API.Controllers
{
    [Route("api/kompanija")]
    public class KompanijaController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ApiResponse _response;

        public KompanijaController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetKompanije()
        {
            _response.Result = _db.Kompanije;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_db.Kompanije);
        }

        [HttpGet("{id:int}", Name = "GetKompanijaById")]
        public async Task<IActionResult> GetKompanijaById(int id)
        {
            if(id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            Kompanija kompanija = _db.Kompanije.FirstOrDefault(k => k.Id == id);

            if(kompanija == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.Result = kompanija;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> KreirajKompaniju([FromBody] KompanijaZaKreiranjeDTO kompanijaZaKreiranjeDTO)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Kompanija kompanijaZaKreiranje = new()
                    {
                        Naziv = kompanijaZaKreiranjeDTO.Naziv,
                        Adresa = kompanijaZaKreiranjeDTO.Adresa,
                    };
                    _db.Kompanije.Add(kompanijaZaKreiranje);
                    _db.SaveChanges();
                    _response.Result = kompanijaZaKreiranje;
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("GetKompanijaById", new { id = kompanijaZaKreiranje.Id }, _response);
                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }

            return _response;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> AzurirajKompaniju(int id, [FromForm] KompanijaZaAzuriranjeDTO kompanijaZaAzuriranjeDTO)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(kompanijaZaAzuriranjeDTO == null || id != kompanijaZaAzuriranjeDTO.Id)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    Kompanija kompanijaIzBaze = await _db.Kompanije.FindAsync(id);

                    if(kompanijaIzBaze == null)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    kompanijaIzBaze.Naziv = kompanijaZaAzuriranjeDTO.Naziv;
                    kompanijaIzBaze.Adresa = kompanijaZaAzuriranjeDTO.Adresa;

                    _db.Kompanije.Update(kompanijaIzBaze);
                    _db.SaveChanges();
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
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
        public async Task<ActionResult<ApiResponse>> ObrisiKompaniju(int id)
        {
            try
            {
                if(id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                Kompanija kompanija = await _db.Kompanije.FindAsync(id);

                if(kompanija == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                _db.Kompanije.Remove(kompanija);
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
