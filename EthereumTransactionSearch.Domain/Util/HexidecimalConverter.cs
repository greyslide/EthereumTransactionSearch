using Nethereum.Web3;
using System;
using System.Globalization;
using System.Numerics;

namespace EthereumTransactionSearch.Domain.Util
{
    public class HexidecimalConverter : IHexidecimalConverter
    {
        public string StripHexAndConvertToBase10(string hexGas)
        {
            var strippedHex = StripHex(hexGas);
            return BigInteger.Parse(strippedHex, NumberStyles.AllowHexSpecifier).ToString();
        }

        public string StripHexOrReturnEmptyStringIfNull(string hex)
        {
            return hex != null ? StripHex(hex) : "";
        }

        public string StripHex(string hex)
        {
            return hex.Substring(2, hex.Length - 2);
        }

        public decimal ConvertHexWeiIntoEther(string hex)
        {
            if (hex == null || hex.Equals("0x0"))
            {
                return 0;
            }
            else
            {
                var strippedHex = StripHex(hex);
                var number = BigInteger.Parse(strippedHex, NumberStyles.AllowHexSpecifier);
                return (number < 0) ? Web3.Convert.FromWei(number * -1) * -1 : Web3.Convert.FromWei(number);
            }
        }
    }
}
