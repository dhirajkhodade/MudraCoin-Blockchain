using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Interfaces;
using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dashboard.Pages
{
    public class SettingsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlockchainService _blockchainService;

        public SettingsModel(ILogger<IndexModel> logger, IBlockchainService blockchainService)
        {
            _logger = logger;
            _blockchainService = blockchainService;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public SettingsDto Settings { get; set; }

        public IActionResult OnGet()
        {
            Settings = _blockchainService.GetCurrentSettings();
            return Page();
        }

        public void OnPost()
        {
            _blockchainService.ApplySettings(Settings);
            RedirectToPage("/Index");
        }
    }
}
