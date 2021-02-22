using System;
using System.Collections.Generic;
using System.Linq;
using BlockchainCore.Models;

namespace BlockchainCore
{
    public class Blockchain
    {
        public List<Block> Chain;
        public int Difficulty { get; set; }
        public double MiningReward { get; set; }
        public List<Transaction> PendingTransactions { get; set; }

        public Blockchain()
        {
            Chain = new List<Block>();
            Chain.Add(CreateGenesisBlock());
            Difficulty = 4;
            MiningReward = 12.5;
            PendingTransactions = new List<Transaction>();
        }

        private Block CreateGenesisBlock()
        {
            return new Block(new DateTime(1,1,1,1,1,1,1),new List<Transaction>(),"0");
        }

        public Block GetLatestBlock()
        {
            return Chain.LastOrDefault();
        }

        public void MinePendingTransactions(string minersAddressForReward)
        {
            var newBlock = new Block(DateTime.UtcNow,PendingTransactions);
            newBlock.MineBlock(Difficulty);
            Chain.Add(newBlock);

            //lets slide in new transaction to give the miner minning reward
            PendingTransactions = new List<Transaction>() { (new Transaction("System", minersAddressForReward, MiningReward)) };
        }

        public void CreateTransaction(Transaction transaction)
        {
            PendingTransactions.Add(transaction);
        }

        public double GetBalance(string address)
        {
            var balance = 0.0;
            foreach (var block in Chain)
            {
                foreach (var transaction in block.Transactions)
                {
                    if (transaction.FromAddress.Equals(address))
                    {
                        balance -= transaction.Amount;
                    }
                    if (transaction.ToAddress.Equals(address))
                    {
                        balance += transaction.Amount;
                    }
                }
            }
            return balance;
        }

        public bool IsBlockchainValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                var currentBlock = Chain.ElementAt(i);
                var prevBlock = Chain.ElementAt(i - 1);

                if (!currentBlock.Hash.Equals(currentBlock.CalculateHash()))
                    return false;

                if (!currentBlock.PrevHash.Equals(prevBlock.Hash))
                    return false;
            }
            return true;
        }
    }
}
