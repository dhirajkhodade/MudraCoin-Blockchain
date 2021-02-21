using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using BlockchainCore;
using BlockchainCore.Models;

namespace BlockchainTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MudraCoin!");

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


            var mudraCoin = new Blockchain();

            Console.WriteLine($"Mining Block 1... With Proof-of-Work Difficulty {mudraCoin.difficulty}");
            mudraCoin.addBlock(new Block(DateTime.UtcNow, new List<Transaction>() { new Transaction("Shivaji", "Bajiprabhu", 10) }));

            Console.WriteLine($"Mining Block 2... With Proof-of-Work Difficulty {mudraCoin.difficulty}");
            mudraCoin.addBlock(new Block(DateTime.UtcNow, new List<Transaction>() { new Transaction("Tanaji", "Dadaji", 10) }));

            Console.WriteLine($"Mining Block 3... With Proof-of-Work Difficulty {mudraCoin.difficulty}");
            mudraCoin.addBlock(new Block(DateTime.UtcNow, new List<Transaction>() { new Transaction("Shahaji", "Yesaji", 10) }));
        }
    }
}
