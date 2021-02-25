using System;
namespace Dashboard.Models
{
    public class TransactionDto
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public byte[] Signature { get; set; }
    }
}
