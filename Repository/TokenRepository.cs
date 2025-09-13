using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NZwalks.API.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZwalks.API.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration) // to be able to use the app settings 
        {
            this._configuration = configuration;
        }



        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            // create claims 
            var Claims = new List<Claim>();
            // emial cliam
            Claims.Add(new Claim(ClaimTypes.Email, user.Email));
            // role claim
            foreach (var role in roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Create Token 

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var cardentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create Token 
            var Token = new JwtSecurityToken(

                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                Claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: cardentials


                );
            // return the token 
            return new JwtSecurityTokenHandler().WriteToken(Token);


        }
    }
}
