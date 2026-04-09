namespace LibraryAPI.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int Anio { get; set; }
        public string Genero { get; set; } = string.Empty;
        public int NumeroPaginas { get; set; }

        // Llave foránea
        public int AutorId { get; set; }
        public Author Autor { get; set; } = null!;
    }
}
