using EthereumTransactionSearch.DataObjects.Models;
using EthereumTransactionSearch.Domain.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.ApplicationServices.Services
{
    public class EthereumTransactionService : IEthereumTransactionService
    {
        private const string rpcEndPoint = "https://mainnet.infura.io/v3/22b2ebe2940745b3835907b30e8257a4";
        private readonly IHexidecimalConverter _hexidecimalConverter;

        public EthereumTransactionService(IHexidecimalConverter hexidecimalConverter)
        {
            _hexidecimalConverter = hexidecimalConverter;
        }

        public async Task<IEnumerable<EthereumTransaction>> GetTransactionBlock(string block, string address)
        {
            var transactionsResult = new EthereumBlockByNumberResponse();
            var transactionDetailRequest = new EthereumTransactionRequest("eth_getBlockByNumber");
            var transactionDetailRequestParams = new List<object> { string.Concat("0x", block), false };
            transactionDetailRequest.Params = transactionDetailRequestParams.ToArray();
            var transactionDetailRequestSerialized = JsonConvert.SerializeObject(transactionDetailRequest);
            using (var webClient = new WebClient())
            {
                var response = webClient.UploadString(rpcEndPoint, "POST", JsonConvert.SerializeObject(transactionDetailRequest));
                transactionsResult = JsonConvert.DeserializeObject<EthereumBlockByNumberResponse>(response);
            }
            var tasks = new List<Task<EthereumTransaction>>();
            if (transactionsResult.result != null && transactionsResult.result.transactions.Any())
            {
                transactionsResult.result.transactions.ForEach(x => tasks.Add(GetTransactionDetails(x)));
                if (address == null)
                {
                    return await Task.WhenAll(tasks);
                }
                var returnTasks = await Task.WhenAll(tasks);
                return returnTasks.Where(x => x.Hash == address);
            }
            return new List<EthereumTransaction>();
        }

        public async Task<EthereumTransaction> GetTransactionDetails(string transaction)
        {
            EthereumTransaction ethTrans = new EthereumTransaction();
            var transactionDetails = new TransactionByHashRoot();
            var transactionDetailRequest = new EthereumTransactionRequest("eth_getTransactionByHash");
            transactionDetailRequest.Params = new[] { transaction };
            using (var webClient = new WebClient())
            {
                var transactionResponse = await webClient.UploadStringTaskAsync(rpcEndPoint, "POST", JsonConvert.SerializeObject(transactionDetailRequest));
                transactionDetails = JsonConvert.DeserializeObject<TransactionByHashRoot>(transactionResponse);
            }
            if (transactionDetails.result != null)
            {
                ethTrans.BlockHash = transactionDetails.result.blockHash;
                ethTrans.BlockNumber = _hexidecimalConverter.StripHexOrReturnEmptyStringIfNull(transactionDetails.result.blockNumber);
                ethTrans.Gas = _hexidecimalConverter.StripHexAndConvertToBase10(transactionDetails.result.gas);
                ethTrans.Hash = transactionDetails.result.hash;
                ethTrans.From = transactionDetails.result.from;
                ethTrans.To = transactionDetails.result.to;
                ethTrans.Value = _hexidecimalConverter.ConvertHexWeiIntoEther(transactionDetails.result.value);
            }
            return ethTrans;
        }
    }
}
