using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic;
public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
    }

    public string CreateToken(Usuario user)
    {
        //Claims
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Nombre),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.Apellido),
            new Claim("username", user.UserName)
        };

        //Decryption
        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        //Create Token with configuration
        var tokenConfiguration = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(60),
            SigningCredentials = credentials,
            Issuer = _config["Token:Issuer"] //secret word for Token decryption 
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenConfiguration);
        return tokenHandler.WriteToken(token);

    }
}
