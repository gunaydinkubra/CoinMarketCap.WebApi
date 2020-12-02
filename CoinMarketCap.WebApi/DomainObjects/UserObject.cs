using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinMarketCap.WebApi.DomainObjects
{
    public class UserObject
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public string TokenExpireDate { get; set; }
        public string Message { get; set; }
        public bool IsLogin { get; set; } = false;
        public bool IsAdded { get; set; }
    }
}
