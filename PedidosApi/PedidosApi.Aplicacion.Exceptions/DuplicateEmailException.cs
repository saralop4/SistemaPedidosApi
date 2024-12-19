namespace PedidosApi.Aplicacion.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException(string message) : base(message)
        {
        }

        public DuplicateEmailException() { }
    }
}
