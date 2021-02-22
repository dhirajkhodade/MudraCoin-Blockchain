using System;
using System.Security.Cryptography;
using System.Text;

namespace BlockchainCore.Models
{
    public class Transaction
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }

        public Transaction(string fromAddress, string toAddress, double amount)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Amount = amount;
        }

        private string CalculateHash()
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(FromAddress + ToAddress + Amount + Timestamp));
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
