using GestionaleConcerti.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionaleConcerti.Data
{
    public class ApplicationDbContext : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        string,
        IdentityUserClaim<string>,
        ApplicationUserRole,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Artista> Artisti { get; set; }
        public DbSet<Evento> Eventi { get; set; }
        public DbSet<Biglietto> Biglietti { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.User)
                        .WithMany(u => u.ApplicationUserRole)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();

                userRole.HasOne(ur => ur.Role)
                        .WithMany(r => r.ApplicationUserRole)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();
            });

            builder.Entity<Artista>()
                .HasMany(a => a.Eventi)
                .WithOne(e => e.Artista)
                .HasForeignKey(e => e.ArtistaId);

            builder.Entity<Evento>()
                .HasMany(e => e.Biglietti)
                .WithOne(b => b.Evento)
                .HasForeignKey(b => b.EventoId);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Biglietti)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);
        }
    }
}
