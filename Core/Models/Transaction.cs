using System;
using System.Security.Cryptography;
using System.Text;

namespace BlockchainCore.Models
{
    public class Transaction
    {
        public Transaction(string from, string to, int amount)
        {
            this.From = from;
            this.To = to;
            this.Amount = amount;
        }
        public string From { get; set; }
        public string To { get; set; }
        public int Amount { get; set; }
        public DateTime Timestamp { get; set; }

        public string CalculateHash()
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(this.From + this.To + this.Amount + this.Timestamp));
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
