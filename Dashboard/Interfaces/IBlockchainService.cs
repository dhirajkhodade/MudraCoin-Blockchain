using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Models;

namespace Dashboard.Interfaces
{
    public interface IBlockchainService
    {
        Task MinePendingTransactions(string minersAddressForReward);
        Task SignAndCreateTransaction(TransactionDto transaction, string secretKey);
        Task<double> GetBalance(string address);
        Task<bool> IsBlockchainValid();
        void ApplySettings(SettingsDto settings);
        SettingsDto GetCurrentSettings();
        Task<List<BlockDto>> GetBlockchain();
        Task<List<TransactionDto>> GetPendingTransactions();
        Task<WalletDetailsDto> GetWalletDetails(string walletAddress);
    }
}
