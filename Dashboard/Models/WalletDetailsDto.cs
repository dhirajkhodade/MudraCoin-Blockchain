using System;
using System.Collections.Generic;

namespace Dashboard.Models
{
    public class WalletDetailsDto
    {
        public string WalletAddress { get; set; }
        public double Balance { get; set; }
        public List<TransactionDto> Transactions { get; set; }
    }
}
