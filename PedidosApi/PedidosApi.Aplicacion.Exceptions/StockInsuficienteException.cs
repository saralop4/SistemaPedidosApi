namespace PedidosApi.Aplicacion.Exceptions
{
    public class StockInsuficienteException : Exception
    {

        public StockInsuficienteException(string message) : base(message)
        {
        }


        public StockInsuficienteException() { }

    }
}
