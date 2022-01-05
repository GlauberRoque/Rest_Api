using aulaRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace aulaRestApi.Data
{
    public class Contexto : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder opt)
        {
            opt.UseSqlServer(@"Data Source=localhost;initial Catalog=API_NET6;User ID=usuario;password=senha;language=Portuguese;Trusted_Connection=True;");
        }

    }
}
