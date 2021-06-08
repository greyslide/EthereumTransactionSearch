namespace EthereumTransactionSearch.Domain.Util
{
    public interface IHexidecimalConverter
    {
        public decimal ConvertHexWeiIntoEther(string hex);
        public string StripHex(string hex);
        public string StripHexAndConvertToBase10(string hexGas);
        public string StripHexOrReturnEmptyStringIfNull(string hex);
    }
}
