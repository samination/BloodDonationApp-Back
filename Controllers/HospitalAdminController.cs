using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DonationBlood.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DonationBlood.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class HospitalAdminController : ControllerBase
    {

        private UserManager<HospitalAdmin> _userManager;
        private SignInManager<HospitalAdmin> _signInManager;
        private  IConfiguration _configuration;
        public HospitalAdminController(UserManager<HospitalAdmin> userManager, SignInManager<HospitalAdmin> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }



        [HttpPost]
        public async Task<object> Register(HospitalAdminModel model)
        {
            var hospitalAdmin = new HospitalAdmin
            {
                UserName = model.Username,
                Email = model.Email,
                Fullname= model.FullName

            };
            var result = await _userManager.CreateAsync(hospitalAdmin, model.Password);
                         await _signInManager.SignInAsync(hospitalAdmin, false);

            return Ok(result);



           /* if (result.Succeeded)
            {
             
                return await GenerateJwtToken(model.Email, hospitalAdmin);
            }

            return 055654654;

            throw new ApplicationException("UNKNOWN_ERROR");*/
        }
        [HttpPost]
        public async Task<object> Login(HospitalAdminModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);


            return Ok(result);

           /* if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return await GenerateJwtToken(model.Email, appUser);
            }*/

          /*  throw new ApplicationException("INVALID_LOGIN_ATTEMPT");*/
        }
        private async Task<object> GenerateJwtToken(string email, HospitalAdmin hospitalAdmin)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, hospitalAdmin.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
