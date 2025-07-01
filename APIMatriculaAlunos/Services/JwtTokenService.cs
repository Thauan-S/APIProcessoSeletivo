using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIMatriculaAlunos.Services
{
    public class JwtTokenService:ITokenService
    {
        // a secret deve ser armazenada de forma segura, por exemplo, em um cofre de segredos ou variáveis de ambiente
        // mas como este codigo é apenas para o processo seletivo, vou deixar a secret hardcoded
        private readonly string _secretKey = "d33b5e2e-e925-40c3-9991-f84aaab0825c";
        private readonly string _issuer = "studentapi";
        private readonly string _audience = "studentapi";

        public string GenerateToken(string studentId, IDictionary<string, string>? additionalClaims = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("studentId", studentId)
            };

            if (additionalClaims != null)
            {
                foreach (var claim in additionalClaims)
                {
                    claims.Add(new Claim(claim.Key, claim.Value));
                }
            }

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
