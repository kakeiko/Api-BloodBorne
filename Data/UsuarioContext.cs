using API_Bloodborne.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_Bloodborne.Data
{
    public class UsuarioContext : IdentityDbContext<Usuario>
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> opts) : base(opts)
        {

        }

    }
}
