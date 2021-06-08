using System;
using System.Numerics;

namespace EthereumTransactionSearch.DataObjects.Models
{
    public class EthereumTransaction
    {
        public string BlockHash { get; set; }

        public string BlockNumber { get; set; }

        public string Gas { get; set; }

        public string Hash { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public decimal Value { get; set; }
    }
}
