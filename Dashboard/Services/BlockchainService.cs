using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlockchainCore;
using BlockchainCore.Models;
using Dashboard.Interfaces;
using Dashboard.Models;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Dashboard.Services
{
    public class BlockchainService : IBlockchainService
    {
        private readonly ILogger<BlockchainService> _logger;
        private readonly Blockchain _blockchain;
        private readonly IMapper _mapper;

        public BlockchainService(ILogger<BlockchainService> logger, Blockchain blockchainCore, IMapper mapper)
        {
            _logger = logger;
            _blockchain = blockchainCore;
            _mapper = mapper;
        }

        public async Task SignAndCreateTransaction(TransactionDto transaction, string secretKey)
        {
            var tx = new BlockchainCore.Models.Transaction(transaction.FromAddress, transaction.ToAddress, transaction.Amount);
            tx.SignTransaction(secretKey);
            await Task.Factory.StartNew(() => _blockchain.AddTransactionToPool(tx));
        }

        public async Task<double> GetBalance(string address)
        {
            return await Task<double>.Factory.StartNew(() => _blockchain.GetBalance(address));
        }

        public async Task<bool> IsBlockchainValid()
        {
            return await Task<bool>.Factory.StartNew(() => _blockchain.IsBlockchainValid());
        }

        public async Task MinePendingTransactions(string minersAddressForReward)
        {
            await Task.Factory.StartNew(() => _blockchain.MinePendingTransactions(minersAddressForReward));
        }

        public async Task<List<BlockDto>> GetBlockchain()
        {
            return _mapper.Map<List<BlockDto>>(await Task<List<Block>>.Factory.StartNew(() => _blockchain.GetBlockchain()));
        }

        public async Task<List<TransactionDto>> GetPendingTransactions()
        {
            return _mapper.Map<List<TransactionDto>>(await Task<List<Transaction>>.Factory.StartNew(() => _blockchain.PendingTransactions));
        }

        public async Task<WalletDetailsDto> GetWalletDetails(string walletAddress)
        {
            var walletDetails = new WalletDetailsDto();
            walletDetails.Balance = await GetBalance(walletAddress);

            var chain = await GetBlockchain();
            walletDetails.Transactions = chain.SelectMany(block => block.Transactions)
                                              .Where(tx => tx.FromAddress.Equals(walletAddress) || tx.ToAddress.Equals(walletAddress)).ToList();
            walletDetails.WalletAddress = walletAddress;
            return walletDetails;
        }

        public void ApplySettings(SettingsDto settings)
        {
            _blockchain.Difficulty = settings.MiningDifficulty;
            _blockchain.MiningReward = settings.MiningReward;
        }

        public SettingsDto GetCurrentSettings()
        {
            return new SettingsDto() { MiningReward = _blockchain.MiningReward, MiningDifficulty = _blockchain.Difficulty };
        }
    }
}
