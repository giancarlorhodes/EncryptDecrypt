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

            Console.WriteLine("Files have been processed...");
            Console.ReadLine();
        }
    }
}
