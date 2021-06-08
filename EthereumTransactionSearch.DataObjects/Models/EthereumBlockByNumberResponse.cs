namespace EthereumTransactionSearch.DataObjects.Models
{
    public class EthereumBlockByNumberResponse
    {
        public string jsonrpc { get; set; }
        public int id { get; set; }
        public EthereumBlockByNumberBlockResponse result { get; set; }
    }
}
