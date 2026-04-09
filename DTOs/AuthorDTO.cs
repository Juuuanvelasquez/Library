using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTOs
{
    public class AuthorDTO
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [MaxLength(150)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La ciudad de procedencia es obligatoria.")]
        [MaxLength(100)]
        public string CiudadProcedencia { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        [MaxLength(150)]
        public string CorreoElectronico { get; set; } = string.Empty;
    }

    public class AuthorResponseDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string CiudadProcedencia { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}
