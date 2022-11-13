using api.Context;
using api.Domain;
using api.Dto;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;

namespace api.Features.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Employee _employee = new Employee();
        private readonly IConfiguration _configuration;
        private readonly IEmployeeService _employeeService;
        private readonly FixedAssetsContext _db;

        public AuthController(IConfiguration configuration, IEmployeeService employeeService, FixedAssetsContext db)
        {
            _configuration = configuration;
            _employeeService = employeeService;
            _db = db;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {
            var employeeName = _employeeService.GetMyName();
            return Ok(employeeName);
        }

        [HttpPost("register")]
        public async Task<ActionResult<Employee>> Register(EmployeeDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var employee = new Employee()
            {
                Name = request.Name,
                Surname = request.Surname,
                Department = request.Department,
                Username = request.Username,
                IsAdmin = request.IsAdmin,
                TokenCreated = DateTime.Now,
                TokenExpires = DateTime.Now,
                RefreshToken = "",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            _employee = employee;

            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(string username, string password)
        {
            var employee = _db.Employees.Where(e => e.Username == username).FirstOrDefault();

            if (employee == null)
            {
                return BadRequest("Employee not found.");
            }            

            if (!VerifyPasswordHash(password, employee.PasswordHash, employee.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(employee);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);

            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!_employee.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (_employee.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = CreateToken(_employee);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            return Ok(token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            _employee.RefreshToken = newRefreshToken.Token;
            _employee.TokenCreated = newRefreshToken.Created;
            _employee.TokenExpires = newRefreshToken.Expires;
        }

        private string CreateToken(Employee employee)
        {
            string role = "Employee";
            if (employee.IsAdmin)
                role = "Admin";
            if (_db.FixedAssetManagers.Where(m => m.Username == employee.Username).FirstOrDefault() != null)
                role = "FAManager";

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, employee.Username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("JWT:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
