using System;
using System.Threading.Tasks;
using Dashboard.Interfaces;
using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dashboard.Pages
{
    public class CreateTransactionModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlockchainService _blockchainService;
        private readonly string _mySigningKey = "105c301c92f5d956ad577105e71aba4d29cf7af04cd47c648244dd8ad677381f";

        public CreateTransactionModel(ILogger<IndexModel> logger, IBlockchainService blockchainService)
        {
            _logger = logger;
            _blockchainService = blockchainService;
        }

        [BindProperty]
        public string MyWalletAddress { get; } = "7a89dec4cc7e0964ed4c5e517f1cfee7e4f145e8500f55fe0317f97e71b7ba5219a4215b1885ac547da87bd0155d02c9bbe0501d0670a4f481df2b42f2130c02";

        [BindProperty]
        public TransactionDto TransactionToCreate { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var txPoolCount = await _blockchainService.GetPendingTransactions();
            ViewData["TxPoolCount"] = txPoolCount.Count.Equals(0) ? "" : txPoolCount.Count;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TransactionToCreate.FromAddress = MyWalletAddress;
                    await _blockchainService.SignAndCreateTransaction(TransactionToCreate, _mySigningKey);
                }
                catch (Exception)
                {
                    throw;
                }
                Message = "New Transaction Added To Pool !";
                return RedirectToPage("/CreateTransaction");
            }
            return Page();
        }

        public async Task<IActionResult> OnGetGetTransactionCount()
        {
            var txCount = await _blockchainService.GetPendingTransactions();
            return new JsonResult(txCount.Count);
        }
    }
}
