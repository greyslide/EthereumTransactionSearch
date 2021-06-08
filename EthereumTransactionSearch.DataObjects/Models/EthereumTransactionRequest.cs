namespace EthereumTransactionSearch.DataObjects.Models
{
    public class EthereumTransactionRequest
    {
        public EthereumTransactionRequest(string method)
        {
            Method = method;
            Jsonrpc = "2.0";
            Id = 1;
        }

        public string Jsonrpc { get; set; }
        public int Id { get; set; }
        public string Method { get; set; }
        public object[] Params { get; set; }
    }
}
