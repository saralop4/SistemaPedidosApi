namespace PedidosApi.Aplicacion.Exceptions
{
    public class ProductoNoEncontradoException : Exception
    {

        public ProductoNoEncontradoException(string message) : base(message)
        {
        }
        public ProductoNoEncontradoException() { }
    }
}
