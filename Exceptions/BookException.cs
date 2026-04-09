namespace LibraryAPI.Exceptions
{
    public class BookException : Exception
    {
        public BookException()
            : base("No es posible registrar el libro, se alcanzó el máximo permitido.")
        {
        }
    }
}
