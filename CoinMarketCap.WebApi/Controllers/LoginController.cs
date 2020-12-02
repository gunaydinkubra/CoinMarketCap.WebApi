using CoinMarketCap.WebApi.Data;
using CoinMarketCap.WebApi.DomainObjects;
using CoinMarketCap.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoinMarketCap.WebApi.Controllers
{
    [AllowAnonymous()]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly CoinMarketCapContext _coinMarketCapContext;
        readonly ILogger<LoginController> _log;
        public LoginController(CoinMarketCapContext coinMarketCapContext, ILogger<LoginController> log)
        {
            _coinMarketCapContext = coinMarketCapContext;
            _log = log;
        }
        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Login([FromBody] UserObject userObject)
        {
            UserObject loginUserObject = new UserObject();
            try
            {
                var user = _coinMarketCapContext.Users.Where(w => w.Username == userObject.Username && w.Password == userObject.Password).FirstOrDefault();
                if (user != null)
                {
                    TokenHelper tokenHelper = new TokenHelper();
                    string token;
                    DateTime tokenExpireDate;

                    tokenHelper.CreateToken(userObject.Username, userObject.Password, out token, out tokenExpireDate);
                    loginUserObject.Username = user.Username;
                    loginUserObject.Password = user.Password;
                    loginUserObject.Token = token;
                    loginUserObject.TokenExpireDate = tokenExpireDate.ToString();

                    if (string.IsNullOrEmpty(token) == false)
                        loginUserObject.IsLogin = true;

                    else
                    {
                        _log.LogError("Token oluşturulamadı!");
                        loginUserObject.Message = "Token oluşturulamadı!";
                        loginUserObject.IsLogin = false;
                        return Ok(loginUserObject);

                    }
                    return Ok(loginUserObject);
                }

                else
                {
                    _log.LogInformation("Kullanıcı bulunamadı!");
                    loginUserObject.Message = "Kullanıcı bulunamadı!";
                    loginUserObject.IsLogin = false;
                    return Ok(loginUserObject);

                }
            }
            catch (Exception ex)
            {
                loginUserObject.Message = ex.Message;
                loginUserObject.IsLogin = false;
                _log.LogError(ex.Message);
                return BadRequest(loginUserObject);
            }


        }
    }
}
