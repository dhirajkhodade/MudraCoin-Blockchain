using System;
using System.Collections.Generic;
using System.Linq;
using BlockchainCore.Models;

namespace BlockchainCore
{
    public class Blockchain
    {
        public List<Block> blockchain;

        public Blockchain()
        {
            blockchain = new List<Block>();
            blockchain.Add(createGenesisBlock());
        }

        private Block createGenesisBlock()
        {
            return new Block(new DateTime(1,1,1,1,1,1,1),new List<Transaction>(),"0");
        }

        public Block getLatestBlock()
        {
            return blockchain.LastOrDefault();
        }

        public void addBlock(Block newBlockToAdd)
        {
            newBlockToAdd.PrevHash = getLatestBlock().Hash;
            newBlockToAdd.Hash = newBlockToAdd.CalculateHash();
            blockchain.Add(newBlockToAdd);
        }

        public bool isBlockchainValid()
        {
            for (int i = 1; i < blockchain.Count; i++)
            {
                var currentBlock = blockchain.ElementAt(i);
                var prevBlock = blockchain.ElementAt(i - 1);

                if (!currentBlock.Hash.Equals(currentBlock.CalculateHash()))
                    return false;

                if (!currentBlock.PrevHash.Equals(prevBlock.Hash))
                    return false;
            }
            return true;
        }
    }
}
