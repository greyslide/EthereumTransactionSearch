using EthereumTransactionSearch.DataObjects.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.ApplicationServices.Services
{
    public interface IEthereumTransactionService
    {
        public Task<EthereumTransaction> GetTransactionDetails(string transaction);
        public Task<IEnumerable<EthereumTransaction>> GetTransactionBlock(string block, string address = "");
    }
}
