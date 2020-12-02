using CoinMarketCap.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinMarketCap.WebApi.Data
{
    public class CoinMarketCapContext : DbContext
    {
        public CoinMarketCapContext(DbContextOptions<CoinMarketCapContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
