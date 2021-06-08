using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using EthereumTransactionSearch.ApplicationServices.Services;
using EthereumTransactionSearch.DataObjects.Models;

namespace EthereumTransactionSearch.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EthereumTransactionController : ControllerBase
    {
        private readonly ILogger<EthereumTransactionController> _logger;
        private readonly IEthereumTransactionService _ethereumTransactionService;

        public EthereumTransactionController(ILogger<EthereumTransactionController> logger, IEthereumTransactionService ethereumTransactionService)
        {
            _logger = logger;
            _ethereumTransactionService = ethereumTransactionService;
        }

        [HttpGet]
        public async Task<IEnumerable<EthereumTransaction>> Get([FromQuery] string block = "", [FromQuery] string address = "")
        {
            try
            {
                return await _ethereumTransactionService.GetTransactionBlock(block, address);
            } catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return new List<EthereumTransaction>();
            }
        }
    }
}
