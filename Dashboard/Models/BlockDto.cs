using System;
using System.Collections.Generic;

namespace Dashboard.Models
{
    public class BlockDto
    {
        public DateTime Timestamp { get; set; }
        public string Hash { get; set; }
        public string PrevHash { get; set; }
        public long Nonce { get; set; }
        public List<TransactionDto> Transactions { get; set; }
    }
}
