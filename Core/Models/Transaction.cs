using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BlockchainCore.Utilities;
using Secp256k1Net;

namespace BlockchainCore.Models
{
    public class Transaction
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public byte[] Signature { get; set; }

        public Transaction(string fromAddress, string toAddress, double amount)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Amount = amount;
            Timestamp = DateTime.UtcNow;
        }

        private Span<byte> CalculateHash()
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(FromAddress + ToAddress + Amount + Timestamp));
               
            }
        }

        public void SignTransaction(string secretKey)
        {
            var secp256k1 = new Secp256k1();
            Span<byte> publicKey = new byte[64];
            secp256k1.PublicKeyCreate(publicKey, secretKey.ToBytes());

            //As only wallet owner can sign it's own transactions so
            //Public key genrated from secretKey must be equal to sender's wallet address (i.e. FromAddress)
            if (!publicKey.ToHex().Equals(FromAddress))
                throw new Exception("You cannot sign transactions for other wallets.");

            var txHash = CalculateHash();
            Span<byte> signature = new byte[64];
            secp256k1.Sign(signature, txHash, secretKey.ToBytes());
            Signature = signature.ToArray();
        }

        public bool IsValid()
        {
            if (FromAddress.Equals("System"))
                return true;
            if (Signature == null || Signature.Count().Equals(0))
            {
                throw new Exception("Transaction not signed..!");
            }
            var secp256k1 = new Secp256k1();
            return secp256k1.Verify(Signature, CalculateHash(), FromAddress.ToBytes());
        }

        //static string ToHex(byte[] data) => String.Concat(data.Select(x => x.ToString("x2")));

    }
}
