namespace EthereumTransactionSearch.DataObjects.Models
{
    public class TransactionByHashRoot
    {
        public string jsonrpc { get; set; }
        public int id { get; set; }
        public TransactionByHashResult result { get; set; }
    }
}
