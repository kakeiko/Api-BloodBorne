using API_Bloodborne.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Bloodborne.Data
{
    public class BloodborneContext : DbContext
    {
        public BloodborneContext(DbContextOptions<BloodborneContext> opts) : base(opts)
        {

        }

        public DbSet<Arma> Armas { get; set; }

        public DbSet<Boss> Bosses { get; set; }

        public DbSet<Inimigo> Inimigos { get; set; }

        public DbSet<Item> Itens { get; set; }

        public DbSet<Local> Locais { get; set; }

        public DbSet<Personagem> Personagens { get; set; }

        public DbSet<Roupa> Roupas { get; set; }

        public DbSet<Runa> Runas { get; set; }
    }
}
