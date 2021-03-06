namespace EthereumTransactionSearch.DataObjects.Models
{
    public class TransactionByHashResult
    {
        public string blockHash { get; set; }
        public string blockNumber { get; set; }
        public string from { get; set; }
        public string gas { get; set; }
        public string gasPrice { get; set; }
        public string hash { get; set; }
        public string input { get; set; }
        public string nonce { get; set; }
        public string r { get; set; }
        public string s { get; set; }
        public string to { get; set; }
        public string transactionIndex { get; set; }
        public string type { get; set; }
        public string v { get; set; }
        public string value { get; set; }
    }
}
