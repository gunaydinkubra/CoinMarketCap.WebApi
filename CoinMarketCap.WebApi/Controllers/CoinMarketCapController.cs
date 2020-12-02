using CoinMarketCap.WebApi.DomainObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoinMarketCap.WebApi.Controllers
{
    [Authorize()]
    [Route("api/[controller]")]
    [ApiController]
    public class CoinMarketCapController : ControllerBase
    {
        private static string API_KEY = "adc9e0b7-a9c2-418b-8686-99f93e2df8af";
        private static string url = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest";

        readonly ILogger<CoinMarketCapController> _log;
        public CoinMarketCapController(ILogger<CoinMarketCapController> log)
        {
            _log = log;
        }
        // GET: api/<CoinMarketCapController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                CoinMarketCapObject coinMarketCapData = new CoinMarketCapObject();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", API_KEY);

                    using (var response = await httpClient.GetAsync(url))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        coinMarketCapData = JsonConvert.DeserializeObject<CoinMarketCapObject>(apiResponse);
                        if (coinMarketCapData.Status.ErrorCode == true)
                        {
                            _log.LogError(coinMarketCapData.Status.ErrorMessage);
                        }
                    }
                }
                return Ok(coinMarketCapData.Data);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(new { message = ex.Message });
            }



        }

    }
}
