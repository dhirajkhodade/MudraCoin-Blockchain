using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using BlockchainCore;
using BlockchainCore.Models;
using Secp256k1Net;

namespace BlockchainTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MudraCoin!");

            #region Blockchain integrity

            //Uncomment below block to test basic blockchain integrity 

            /*
            var mudraCoin = new Blockchain();

            mudraCoin.addBlock(new Block(DateTime.UtcNow, new List<Transaction>() { new Transaction("Shivaji","Bajiprabhu",10) }));
            mudraCoin.addBlock(new Block(DateTime.UtcNow, new List<Transaction>() { new Transaction("Tanaji", "Dadaji", 10) }));
            mudraCoin.addBlock(new Block(DateTime.UtcNow, new List<Transaction>() { new Transaction("Shahaji", "Yesaji", 10) }));
            mudraCoin.addBlock(new Block(DateTime.UtcNow, new List<Transaction>() { new Transaction("Siddi", "Kanhoji", 10) }));


            Console.WriteLine(JsonSerializer.Serialize(mudraCoin.blockchain));

            Console.WriteLine("Is blockchain valid : " + mudraCoin.isBlockchainValid());

            Console.WriteLine("Tampering with blockchain (•̀ᴗ•́ )");

            mudraCoin.blockchain.ElementAt(2).PrevHash = "123";

            Console.WriteLine("is blockchain valid : " + mudraCoin.isBlockchainValid());

            Console.WriteLine(JsonSerializer.Serialize(mudraCoin.blockchain));
            */

            #endregion

            #region Mining rewad implementation

            // Mining reward implementation

            /*
            var mudraCoin = new Blockchain();

            mudraCoin.AddTransactionToPool(new Transaction("Shivaji", "Bajiprabhu", 10));
            mudraCoin.AddTransactionToPool(new Transaction("Tanaji", "Dadaji", 20));
            mudraCoin.AddTransactionToPool(new Transaction("Shahaji", "Yesaji", 30));

            Console.WriteLine("Starting the miner..!");
            mudraCoin.MinePendingTransactions("Manjhi-TheMiner");

            Console.WriteLine($"Balance of the 'Manjhi-TheMiner' is {mudraCoin.GetBalance("Manjhi-TheMiner")}");

            Console.WriteLine("Starting the miner..!");
            mudraCoin.MinePendingTransactions("Manjhi-TheMiner");
            Console.WriteLine($"Balance of the 'Manjhi-TheMiner' is {mudraCoin.GetBalance("Manjhi-TheMiner")}");

            */

            #endregion

            #region Signing transactions implementation


            // Signing transactions implementation


            var mudraCoin = new Blockchain();
            var privateKey = "105c301c92f5d956ad577105e71aba4d29cf7af04cd47c648244dd8ad677381f";
            //public key i.e. User's wallet address
            var myMudraCoinWalletAddress = "7a89dec4cc7e0964ed4c5e517f1cfee7e4f145e8500f55fe0317f97e71b7ba5219a4215b1885ac547da87bd0155d02c9bbe0501d0670a4f481df2b42f2130c02";

            var tx = new Transaction(myMudraCoinWalletAddress, "Bajiprabhu", 10);
            //We are signing the transaction with user's private key so that this user is the only one who can spend the coins in this wallet
            tx.SignTransaction(privateKey);
            mudraCoin.AddTransactionToPool(tx);


            Console.WriteLine("Starting the miner..!");
            mudraCoin.MinePendingTransactions(myMudraCoinWalletAddress);

            Console.WriteLine($"Balance of the 'Manjhi-TheMiner' is {mudraCoin.GetBalance(myMudraCoinWalletAddress)}");

            //Console.WriteLine(JsonSerializer.Serialize(mudraCoin.Chain));

            Console.WriteLine($"Is blockchain Valid :{mudraCoin.IsBlockchainValid()}");

            Console.WriteLine("Tampering transaction in blockchain (•̀ᴗ•́ )");

            //This tampering with transaction to change the amout will invalidate the transaction signeture
            //resulting invalid transaction and bloack in out block chain
            mudraCoin.Chain[1].Transactions[0].Amount = 1000;

            Console.WriteLine("Transaction Tampered..! (•̀ᴗ•́ )");


            Console.WriteLine($"Is blockchain Valid :{mudraCoin.IsBlockchainValid()}");


            #endregion

        }

        #region helper functions for testing

        //Ignore this function as this is just to create Public Private key pairs for testing
        public static void GenerateKeys()
        {
            using (var secp256k1 = new Secp256k1())
            {
                // privatekey= "105c301c92f5d956ad577105e71aba4d29cf7af04cd47c648244dd8ad677381f"
                // publickey = "7a89dec4cc7e0964ed4c5e517f1cfee7e4f145e8500f55fe0317f97e71b7ba5219a4215b1885ac547da87bd0155d02c9bbe0501d0670a4f481df2b42f2130c02"

                // Generate a private key.
                var privateKey = new byte[32];
                var rnd = System.Security.Cryptography.RandomNumberGenerator.Create();
                do { rnd.GetBytes(privateKey); }
                while (!secp256k1.SecretKeyVerify(privateKey));

                var prk = ToHex(privateKey);

                // Create public key from private key.
                var publicKey = new byte[64];
                Debug.Assert(secp256k1.PublicKeyCreate(publicKey, privateKey));

                var pbk = ToHex(publicKey);


                // Sign a message hash.
                var messageBytes = Encoding.UTF8.GetBytes("Hello world.");
                var messageHash = System.Security.Cryptography.SHA256.Create().ComputeHash(messageBytes);
                var signature = new byte[64];
                Debug.Assert(secp256k1.Sign(signature, messageHash, privateKey));

                // Serialize a DER signature from ECDSA signature
                byte[] signatureDer = new byte[Secp256k1.SERIALIZED_DER_SIGNATURE_MAX_SIZE];
                int outL = 0;
                Debug.Assert(secp256k1.SignatureSerializeDer(signatureDer, signature, out outL));
                Array.Resize(ref signatureDer, outL);

                // Verify message hash.
                Debug.Assert(secp256k1.Verify(signature, messageHash, publicKey));

            }
        }

        static string ToHex(byte[] data) => String.Concat(data.Select(x => x.ToString("x2")));
        #endregion


    }
}
