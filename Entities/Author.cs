namespace LibraryAPI.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string CiudadProcedencia { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;


        public ICollection<Book> Libros { get; set; } = new List<Book>();
    }
}
