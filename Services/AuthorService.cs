using LibraryAPI.DTOs;
using LibraryAPI.Entities;
using LibraryAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorResponseDTO>> ObtenerTodosAsync()
        {
            return await _context.Autores
                .Select(a => new AuthorResponseDTO
                {
                    Id = a.Id,
                    NombreCompleto = a.NombreCompleto,
                    FechaNacimiento = a.FechaNacimiento,
                    CiudadProcedencia = a.CiudadProcedencia,
                    CorreoElectronico = a.CorreoElectronico
                }).ToListAsync();
        }

        public async Task<AuthorResponseDTO> ObtenerPorIdAsync(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null) return null;

            return new AuthorResponseDTO
            {
                Id = autor.Id,
                NombreCompleto = autor.NombreCompleto,
                FechaNacimiento = autor.FechaNacimiento,
                CiudadProcedencia = autor.CiudadProcedencia,
                CorreoElectronico = autor.CorreoElectronico
            };
        }

        public async Task<AuthorResponseDTO> CrearAsync(AuthorDTO dto)
        {
            var autor = new Author
            {
                NombreCompleto = dto.NombreCompleto,
                FechaNacimiento = dto.FechaNacimiento,
                CiudadProcedencia = dto.CiudadProcedencia,
                CorreoElectronico = dto.CorreoElectronico
            };

            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            return new AuthorResponseDTO
            {
                Id = autor.Id,
                NombreCompleto = autor.NombreCompleto,
                FechaNacimiento = autor.FechaNacimiento,
                CiudadProcedencia = autor.CiudadProcedencia,
                CorreoElectronico = autor.CorreoElectronico
            };
        }

        public async Task<AuthorResponseDTO> ActualizarAsync(int id, AuthorDTO dto)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null) return null;

            autor.NombreCompleto = dto.NombreCompleto;
            autor.FechaNacimiento = dto.FechaNacimiento;
            autor.CiudadProcedencia = dto.CiudadProcedencia;
            autor.CorreoElectronico = dto.CorreoElectronico;

            await _context.SaveChangesAsync();

            return new AuthorResponseDTO
            {
                Id = autor.Id,
                NombreCompleto = autor.NombreCompleto,
                FechaNacimiento = autor.FechaNacimiento,
                CiudadProcedencia = autor.CiudadProcedencia,
                CorreoElectronico = autor.CorreoElectronico
            };
        }

        public async Task EliminarAsync(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null) return;

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();
        }
    }
}
