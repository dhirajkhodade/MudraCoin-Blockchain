using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Interfaces;
using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dashboard.Pages
{
    public class PendingTransactionsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlockchainService _blockchainService;
        private readonly IConfiguration _configuration;

        public PendingTransactionsModel(ILogger<IndexModel> logger, IBlockchainService blockchainService, IConfiguration configuration)
        {
            _logger = logger;
            _blockchainService = blockchainService;
            _configuration = configuration;
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
            await _blockchainService.MinePendingTransactions(_configuration.GetSection("AppConfig")["MyWalletAddress"]);
            Message = "Mining Started..!";
            return RedirectToPage("/Index");
        }
    }
}
