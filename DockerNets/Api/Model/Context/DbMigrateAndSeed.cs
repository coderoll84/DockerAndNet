using Api.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Model.Context
{
    public static class DbMigrateAndSeed
    {
        public static async void MigrateAndSeed(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            {
                using var context = serviceScope.ServiceProvider.GetService<UsuariosContext>();
                {
                    if (context.Database.IsSqlServer())
                    {
                        context.Migrate();
                    }

                    await context.Seed();
                }
            }
        }

        private static void Migrate(this UsuariosContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();
        }

        private static async Task Seed(this UsuariosContext context)
        {
            var usuario = new Usuario { Nombre = "olopez", Mail = "coderoll84@gmail", Creacion = DateTime.UtcNow };
            
            if (!context.Usuarios.Any()) // Seed, if necessary
            {
                context.Usuarios.Add(usuario);
                await context.SaveChangesAsync();
            }
        }
    }
}