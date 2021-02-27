using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Interfaces;
using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Dashboard.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly IBlockchainService _blockchainService;

        public IndexModel(ILogger<IndexModel> logger,IBlockchainService blockchainService,IConfiguration configuration)
        {
            _logger = logger;
            _blockchainService = blockchainService;
            _configuration = configuration;
        }

        public List<BlockDto> Blockchain { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task OnGet()
        {
            var txPoolCount = await _blockchainService.GetPendingTransactions();
            ViewData["TxPoolCount"] = txPoolCount.Count.Equals(0) ? "" : txPoolCount.Count;
            ViewData["MyAddress"] = _configuration.GetSection("AppConfig")["MyWalletAddress"];

            Blockchain = await _blockchainService.GetBlockchain();
        }

        public async Task<PartialViewResult> OnGetTransactionsPartialAsync(int id)
        {
            Blockchain = await _blockchainService.GetBlockchain();

            return new PartialViewResult
            {
                ViewName = "_TransactionsGrid",
                ViewData = new ViewDataDictionary<List<TransactionDto>>(ViewData, Blockchain[id].Transactions)
            };
        }
    }
}

