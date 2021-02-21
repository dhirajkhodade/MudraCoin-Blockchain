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
        public long Nounce { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Block(DateTime timestamp, List<Transaction> transactions,string prevHash="")
        {
            PrevHash = prevHash;
            Timestamp = timestamp;
            Transactions = transactions;
            Nounce = 0;
            Hash = CalculateHash();
        }

        public string CalculateHash()
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Timestamp + PrevHash + Nounce + JsonSerializer.Serialize(Transactions)));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); //convert to Hex
                }
                return builder.ToString();
            }
        }
    }
}
