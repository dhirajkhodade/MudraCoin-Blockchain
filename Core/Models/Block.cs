using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace BlockchainCore.Models
{
    public class Block
    {
        public DateTime Timestamp { get; set; }
        public string Hash { get; set; }
        public string PrevHash { get; set; }
        public long Nonce { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Block(DateTime timestamp, List<Transaction> transactions,string prevHash="")
        {
            PrevHash = prevHash;
            Timestamp = timestamp;
            Transactions = transactions;
            Nonce = 0;
            Hash = CalculateHash();
        }

        internal string CalculateHash()
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Timestamp + PrevHash + Nonce + JsonSerializer.Serialize(Transactions)));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); //convert to Hex
                }
                return builder.ToString();
            }
        }

        public void MineBlock(int difficulty)
        {
            while (Hash.Substring(0, difficulty) != "".PadRight(difficulty, '0'))
            {
                Nonce++;
                Hash = CalculateHash();
            }
            Console.WriteLine("Block successfully mined: " + Hash);
        }
    }
}
