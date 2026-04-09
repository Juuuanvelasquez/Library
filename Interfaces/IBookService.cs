using LibraryAPI.DTOs;

namespace LibraryAPI.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookResponseDTO>> ObtenerTodosAsync();
        Task<BookResponseDTO> ObtenerPorIdAsync(int id);
        Task<BookResponseDTO> CrearAsync(BookDTO dto);
        Task<BookResponseDTO> ActualizarAsync(int id, BookDTO dto);
        Task EliminarAsync(int id);
    }
}
