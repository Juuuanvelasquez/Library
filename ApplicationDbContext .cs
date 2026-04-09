
using LibraryAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Autores { get; set; }
        public DbSet<Book> Libros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Author configuration
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.NombreCompleto)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(a => a.FechaNacimiento)
                      .IsRequired();

                entity.Property(a => a.CiudadProcedencia)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(a => a.CorreoElectronico)
                      .IsRequired()
                      .HasMaxLength(150);
            });

            //Book configuration
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(l => l.Id);

                entity.Property(l => l.Titulo)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(l => l.Anio)
                      .IsRequired();

                entity.Property(l => l.Genero)
                      .IsRequired()
                      .HasMaxLength(80);

                entity.Property(l => l.NumeroPaginas)
                      .IsRequired();

             // Book and author relation
                entity.HasOne(l => l.Autor)
                      .WithMany(a => a.Libros)
                      .HasForeignKey(l => l.AutorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
