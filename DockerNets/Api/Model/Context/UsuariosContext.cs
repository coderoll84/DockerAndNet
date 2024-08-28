using Api.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Model.Context
{
    public class UsuariosContext : DbContext
    {
        public UsuariosContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Usuario> Usuarios { get; set; }
    }
}
