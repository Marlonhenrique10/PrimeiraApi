using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Model;

namespace WebApi.Services
{
    // Classe responsavél por gerar o token
    public class TokenServices
    {
        public static object GenerateToken(InfoApi infoApi)
        {
            var key = Encoding.ASCII.GetBytes(Key.Secret); // Minha chave

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                      new Claim("infoApiId", infoApi.id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),

                // Tipo assinatura. (estou passando a minha chave privada e o tipo de criptografia que estou utilizando)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenConfig); // Estou criando o token

            var tokenString = tokenHandler.WriteToken(token); // Estou escrevendo o token

            return new
            {
                token = tokenString
            };
        }
    }
}
