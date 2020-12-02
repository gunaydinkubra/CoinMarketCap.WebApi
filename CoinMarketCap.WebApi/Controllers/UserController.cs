using CoinMarketCap.WebApi.Data;
using CoinMarketCap.WebApi.DomainObjects;
using CoinMarketCap.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoinMarketCap.WebApi.Controllers
{
    [Authorize()]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CoinMarketCapContext _coinMarketCapContext;
        readonly ILogger<LoginController> _log;
        public UserController(CoinMarketCapContext coinMarketCapContext, ILogger<LoginController> log)
        {
            _coinMarketCapContext = coinMarketCapContext;
            _log = log;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get()
        {
            var users = _coinMarketCapContext.Users.Select(s => new UserObject
            {
                Username = s.Username,
                Password = s.Password
            }).ToList();
            if (users != null && users.Count > 0)
                return Ok(users);
            else
                return NotFound();
        }

        // POST api/<UsersController>
        [HttpPost]
        [AllowAnonymous()]
        public IActionResult Post([FromBody] UserObject userObject)
        {

            try
            {
                if (string.IsNullOrEmpty(userObject.Username))
                {
                    userObject.IsAdded = false;
                    userObject.Message = "Kullanıcı adı boş olamaz!";
                    return Ok(userObject);
                }
                if (string.IsNullOrEmpty(userObject.Password))
                {
                    userObject.IsAdded = false;
                    userObject.Message = "Şifre boş olamaz!";
                    return Ok(userObject);
                }
                var user = _coinMarketCapContext.Users.Where(s => s.Username == userObject.Username && s.Password == userObject.Password).ToList();
                if (user.Count > 0)
                {
                    userObject.IsAdded = false;
                    userObject.Message = "Girmiş olduğunuz bilgiler sistemde mevcut!";
                    return Ok(userObject);
                }

                _coinMarketCapContext.Users.Add(new UserModel
                {
                    Password = userObject.Password,
                    Username = userObject.Username
                });

                _coinMarketCapContext.SaveChanges();
                userObject.IsAdded = true;
                userObject.Message = "Kullanıcı Eklendi.Lütfen Giriş Yapınız.";
                return Ok(userObject);

            }
            catch (System.Exception ex)
            {
                _log.LogError(ex.Message);
                userObject.IsAdded = false;
                userObject.Message = ex.Message;
                return Ok(userObject);

            }

        }


    }
}
