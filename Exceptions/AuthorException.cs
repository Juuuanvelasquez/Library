namespace LibraryAPI.Exceptions
{
    public class AuthorException : Exception
    {
        public AuthorException()
            : base("El autor no está registrado.")
        {
        }
    }
}
