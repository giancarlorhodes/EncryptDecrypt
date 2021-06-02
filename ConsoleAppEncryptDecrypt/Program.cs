using ClassLibraryEncryptDecrypt;
using System;
using System.IO;
using System.Security.Cryptography;

namespace ConsoleAppEncryptDecrypt
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = "";
            string sourceFilename = "";
            string destinationFilename = "";
            string keyFilename = "";

           

            // http://johnrush.github.io/File-Encryption-Tutorial/
            if (args.Length != 4)
            {
                // TODO: rework the UI
                //Console.WriteLine("You must provide the name of a path to the files.");
                //path = Console.ReadLine();
               

                //Console.WriteLine("You must provide the name of a file to read.");
                //sourceFilename = Console.ReadLine();
                
               
                //Console.WriteLine("You must provide the name of a file to encrypt to.");
                //destinationFilename = Console.ReadLine();
                

                //Console.WriteLine("You must provide the name of a key file to write.");
                //keyFilename = Console.ReadLine();


                //FileWorker worker = new FileWorker(path, sourceFilename, destinationFilename, keyFilename);
                //worker.EncryptTheFile();
                //var encrypedFile = path + "\\" + destinationFilename;
                //var decrypedFile = path + "\\DECRYPTED" + destinationFilename;  
                //worker.DecryptTheFile(encrypedFile, decrypedFile, worker.PrivateKey, worker.PublicVector);


            }
            else 
            {
                // TODO fix
                //sourceFilename = args[0];
                //destinationFilename = args[1];
                return;    
            }




            //using (var sourceStream = File.OpenRead(sourceFilename))
            //using (var destinationStream = File.Create(destinationFilename))
            //using (var destinationStreamKeys = File.Create(destinationKeysFilename))
            //using (var provider = new AesCryptoServiceProvider())
            //using (var cryptoTransform = provider.CreateEncryptor())
            //using (var cryptoStream = new CryptoStream(destinationStream, cryptoTransform, CryptoStreamMode.Write))
            //{

            //    // When using AES we have the key, which is secret and should be protected. But there is 
            //    // another piece of data that is used to make the encryption stronger.It is called the IV 
            //    // (initialization vector). The IV tells the encryptor how to modify the first block to 
            //    // prevent against certain attacks. Changing the IV will change the encryption/decryption 
            //    // results, so we are going to need to have that available when we decrypt the file.The IV 
            //    // is not secret, however.So we don't need to hide it. Therefore, we can write it directly 
            //    // to the output file we are already writing. That way we can't possibly lose it. We do 
            //    // have to be careful though.If we write the IV to the file and the bytes get encrypted 
            //    // then we won't be able to get them back out (since we need them to do the decryption). 
            //    // We have to make sure to write the IV before any encryption happens. 


            //    destinationStreamKeys.Write(provider.IV, 0, provider.IV.Length);
            //    destinationStreamKeys.Write(provider.Key, 0 , provider.Key.Length);


            //    sourceStream.CopyTo(cryptoStream);

            //    // We just get the Key property of the AesCryptoServiceProvider.The key is an array of bytes,
            //    // which won't print very nicely. Using System.Convert.ToBase64String makes it so we can 
            //    // represent all of those byte values as printable characters. We could have converted the 
            //    // byte array to a hex string or any number of things, but Base64 is convenient.

            //    Console.WriteLine(System.Convert.ToBase64String(provider.Key)); // random private key
            //}


            Console.WriteLine("Files have been processed...");
            Console.ReadLine();
        }
    }
}
