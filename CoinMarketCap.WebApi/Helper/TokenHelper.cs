using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoinMarketCap.WebApi.Helper
{
    public class TokenHelper
    {

        public void CreateToken(string username, string password, out string token, out DateTime tokenExpireDate)
        {
            var someClaims = new Claim[]{
                new Claim(JwtRegisteredClaimNames.UniqueName,username),
                new Claim(JwtRegisteredClaimNames.NameId,Guid.NewGuid().ToString())
            };
            DateTime expiresDate = DateTime.Now.AddDays(3);
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("smrFsFT09jD0EWdjr3O8xDOUTpYQGQIKexFIjZEbV8bvERL27aptr7oDkqLevT9qkaHjSC17ZSRBkJroZgETcMnu0PJABR55jokGzr0YP74WuMueOOP6OROjeeHUTDHD"));
            var createToken = new JwtSecurityToken(
                issuer: "CoinMarketCap.com",
                audience: "api.CoinMarketCap.com",
                claims: someClaims,
                expires: expiresDate,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            token = new JwtSecurityTokenHandler().WriteToken(createToken);
            tokenExpireDate = expiresDate;
        }
    }
}
