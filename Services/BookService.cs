using LibraryAPI.DTOs;
using LibraryAPI.Entities;
using LibraryAPI.Exceptions;
using LibraryAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public BookService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<BookResponseDTO>> ObtenerTodosAsync()
        {
            return await _context.Libros
                .Include(l => l.Autor)
                .Select(l => new BookResponseDTO
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Anio = l.Anio,
                    Genero = l.Genero,
                    NumeroPaginas = l.NumeroPaginas,
                    NombreAutor = l.Autor.NombreCompleto
                }).ToListAsync();
        }

        public async Task<BookResponseDTO> ObtenerPorIdAsync(int id)
        {
            var libro = await _context.Libros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (libro == null) return null;

            return new BookResponseDTO
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                Anio = libro.Anio,
                Genero = libro.Genero,
                NumeroPaginas = libro.NumeroPaginas,
                NombreAutor = libro.Autor.NombreCompleto
            };
        }

        public async Task<BookResponseDTO> CrearAsync(BookDTO dto)
        {
            // ── Regla 1: Verificar que el autor existe ──────────
            var autorExiste = await _context.Autores.AnyAsync(a => a.Id == dto.AutorId);
            if (!autorExiste)
                throw new AuthorException();

            // ── Regla 2: Verificar máximo de libros permitidos ──
            var maxLibros = _configuration.GetValue<int>("MaxLibrosPermitidos");
            var totalLibros = await _context.Libros.CountAsync();
            if (totalLibros >= maxLibros)
                throw new BookException();

            var libro = new Book
            {
                Titulo = dto.Titulo,
                Anio = dto.Anio,
                Genero = dto.Genero,
                NumeroPaginas = dto.NumeroPaginas,
                AutorId = dto.AutorId
            };

            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            var autor = await _context.Autores.FindAsync(dto.AutorId);

            return new BookResponseDTO
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                Anio = libro.Anio,
                Genero = libro.Genero,
                NumeroPaginas = libro.NumeroPaginas,
                NombreAutor = autor.NombreCompleto
            };
        }

        public async Task<BookResponseDTO> ActualizarAsync(int id, BookDTO dto)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return null;

            // Verificar que el autor existe
            var autorExiste = await _context.Autores.AnyAsync(a => a.Id == dto.AutorId);
            if (!autorExiste)
                throw new AuthorException();

            libro.Titulo = dto.Titulo;
            libro.Anio = dto.Anio;
            libro.Genero = dto.Genero;
            libro.NumeroPaginas = dto.NumeroPaginas;
            libro.AutorId = dto.AutorId;

            await _context.SaveChangesAsync();

            var autor = await _context.Autores.FindAsync(dto.AutorId);

            return new BookResponseDTO
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                Anio = libro.Anio,
                Genero = libro.Genero,
                NumeroPaginas = libro.NumeroPaginas,
                NombreAutor = autor.NombreCompleto
            };
        }

        public async Task EliminarAsync(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return;

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
        }
    }
}
