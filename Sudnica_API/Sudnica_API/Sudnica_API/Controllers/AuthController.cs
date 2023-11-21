using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sudnica_API.DbContexts;
using Sudnica_API.Models;
using Sudnica_API.Models.Dto;
using Sudnica_API.Utility;
using SudnicaAPI_Test.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Sudnica_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ApiResponse _response;
        private readonly UserManager<Korisnik> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretKey;

        public AuthController(ApplicationDbContext db, IConfiguration configuration,
            UserManager<Korisnik> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _response = new ApiResponse();
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("Registracija")]
        public virtual async Task<IActionResult> Registracija([FromBody] RegistracijaDTO model)
        {
            Korisnik korisnikIzBaze = _db.Korisnici
                .FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());

            if (korisnikIzBaze != null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Korisničko ime već postoji!");
                return BadRequest(_response);
            }

            Korisnik noviKorisnik = new()
            {
                UserName = model.UserName,
                Email = model.UserName,
                NormalizedEmail = model.UserName.ToUpper(),
                PunoIme = model.Ime,
            };

            try
            {
                var result = await _userManager.CreateAsync(noviKorisnik, model.Lozinka);
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                    {
                        //Kreiranje uloga u bazi
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_User));
                    }
                    if (model.Role.ToLower() == SD.Role_Admin)
                    {
                        await _userManager.AddToRoleAsync(noviKorisnik, SD.Role_Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(noviKorisnik, SD.Role_User);
                    }

                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
            }
            catch (Exception ex)
            {

            }
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Greška prilikom registracije!");
            return BadRequest(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginZahtevDTO model)
        {
            Korisnik korisnikIzBaze = _db.Korisnici
                .FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(korisnikIzBaze, model.Lozinka);

            if(isValid == false)
            {
                _response.Result = new LoginOdgovorDTO();
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Korisničko ime ili šifra su netačni!");
                return BadRequest(_response);
            }

            //u suprotnom generisem JWT token
            var uloge = await _userManager.GetRolesAsync(korisnikIzBaze);
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("punoIme", korisnikIzBaze.PunoIme),
                    new Claim("id", korisnikIzBaze.Id.ToString()),
                    new Claim(ClaimTypes.Email, korisnikIzBaze.UserName.ToString()),
                    new Claim(ClaimTypes.Role, uloge.FirstOrDefault()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            LoginOdgovorDTO loginOdgovor = new()
            {
                Email = korisnikIzBaze.Email,
                Token = tokenHandler.WriteToken(token)
            };

            if(loginOdgovor.Email == null || string.IsNullOrEmpty(loginOdgovor.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Korisničko ime ili šifra su netačni!");
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginOdgovor;
            return Ok(_response);
        }
    }
}
