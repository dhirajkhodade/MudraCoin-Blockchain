using System.Threading.Tasks;
using Dashboard.Interfaces;
using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dashboard.Pages
{
    public class WalletDetailsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlockchainService _blockchainService;

        public WalletDetailsModel(ILogger<IndexModel> logger, IBlockchainService blockchainService)
        {
            _logger = logger;
            _blockchainService = blockchainService;
        }

        public WalletDetailsDto WalletDetails { get; set; }

        public async Task<IActionResult> OnGet(string walletAddress)
        {
            WalletDetails = await _blockchainService.GetWalletDetails(walletAddress);
            
            return Page();
        }
    }
}
