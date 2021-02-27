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

            //var a = _configuration.GetSection("AppConfig")["MyWalletAddress"];

            //var privateKey = "105c301c92f5d956ad577105e71aba4d29cf7af04cd47c648244dd8ad677381f";
            //var myMudraCoinWalletAddress = "7a89dec4cc7e0964ed4c5e517f1cfee7e4f145e8500f55fe0317f97e71b7ba5219a4215b1885ac547da87bd0155d02c9bbe0501d0670a4f481df2b42f2130c02";
            //var tx = new TransactionDto();
            //tx.Amount = 2;
            //tx.FromAddress = myMudraCoinWalletAddress;
            //tx.ToAddress = "test";
            //await _blockchainService.SignAndCreateTransaction(tx, privateKey);
            //await _blockchainService.MinePendingTransactions(myMudraCoinWalletAddress);

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

