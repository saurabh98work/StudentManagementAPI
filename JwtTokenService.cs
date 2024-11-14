using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string username)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        // object JwtSettings = null;
#pragma warning disable CS8604 // Possible null reference argument.
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
#pragma warning restore CS8604 // Possible null reference argument.
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: signingCredentials
        );
        Console.WriteLine($"SecretKey: {jwtSettings["SecretKey"]}");
        Console.WriteLine($"Issuer: {jwtSettings["Issuer"]}");
        Console.WriteLine($"Audience: {jwtSettings["Audience"]}");

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
