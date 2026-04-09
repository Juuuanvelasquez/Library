using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTOs
{
    public class BookDTO
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        [MaxLength(200)]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El año es obligatorio.")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        [MaxLength(80)]
        public string Genero { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de páginas es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de páginas debe ser mayor a 0.")]
        public int NumeroPaginas { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio.")]
        public int AutorId { get; set; }
    }

    public class BookResponseDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int Anio { get; set; }
        public string Genero { get; set; } = string.Empty;
        public int NumeroPaginas { get; set; }
        public string NombreAutor { get; set; } = string.Empty;
    }
}
