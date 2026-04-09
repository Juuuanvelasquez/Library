using LibraryAPI.DTOs;

namespace LibraryAPI.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorResponseDTO>> ObtenerTodosAsync();
        Task<AuthorResponseDTO> ObtenerPorIdAsync(int id);
        Task<AuthorResponseDTO> CrearAsync(AuthorDTO dto);
        Task<AuthorResponseDTO> ActualizarAsync(int id, AuthorDTO dto);
        Task EliminarAsync(int id);
    }
}
