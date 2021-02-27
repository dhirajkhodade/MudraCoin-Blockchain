using System;
using System.Threading.Tasks;
using Dashboard.Interfaces;
using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dashboard.Pages
{
    public class CreateTransactionModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlockchainService _blockchainService;
        private readonly IConfiguration _configuration;

        public CreateTransactionModel(ILogger<IndexModel> logger, IBlockchainService blockchainService, IConfiguration configuration)
        {
            _logger = logger;
            _blockchainService = blockchainService;
            _configuration = configuration;
        }

        [BindProperty]
        public string MyWalletAddress { get; set; }

        [BindProperty]
        public TransactionDto TransactionToCreate { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGet()
        {
            MyWalletAddress = _configuration.GetSection("AppConfig")["MyWalletAddress"];
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
                    await _blockchainService.SignAndCreateTransaction(TransactionToCreate, _configuration.GetSection("AppConfig")["MySigningKey"]);
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
