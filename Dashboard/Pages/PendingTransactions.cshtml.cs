using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Interfaces;
using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dashboard.Pages
{
    public class PendingTransactionsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlockchainService _blockchainService;
        private string _myWalletAddress { get; } = "7a89dec4cc7e0964ed4c5e517f1cfee7e4f145e8500f55fe0317f97e71b7ba5219a4215b1885ac547da87bd0155d02c9bbe0501d0670a4f481df2b42f2130c02";

        public PendingTransactionsModel(ILogger<IndexModel> logger, IBlockchainService blockchainService)
        {
            _logger = logger;
            _blockchainService = blockchainService;
        }

        public List<TransactionDto> PendingTransactions { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task OnGet()
        {
            var txPoolCount = await _blockchainService.GetPendingTransactions();
            ViewData["TxPoolCount"] = txPoolCount.Count.Equals(0) ? "" : txPoolCount.Count;
            PendingTransactions = await _blockchainService.GetPendingTransactions();
        }

        public async Task<IActionResult> OnPostStartMining()
        {
            await _blockchainService.MinePendingTransactions(_myWalletAddress);
            Message = "Mining Started..!";
            return RedirectToPage("/Index");
        }
    }
}
