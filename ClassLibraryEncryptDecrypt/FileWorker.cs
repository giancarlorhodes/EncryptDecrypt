using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ClassLibraryEncryptDecrypt
{

    // http://johnrush.github.io/File-Encryption-Tutorial/
    public class FileWorker
    {
        // properties and fields
        private string _destinationFileName { get; set; }

        private string _sourceFileName { get; set; }

        private string _keyFileName { get; set; }

        private string _directoryPath { get; set; }

        //FileStream DestinationStream { get; set; }
        //FileStream SourceStream { get; set; }
      
        AesCryptoServiceProvider Provider { get; set; }

        ICryptoTransform Transform { get; set; }
        public string PrivateKey { get; set; }
        public string PublicVector { get; set; }


        // constructors
        public FileWorker()
        {
            Provider = new AesCryptoServiceProvider();
            Transform = Provider.CreateEncryptor();
            ConvertKeyAndVector();
        }


        public FileWorker(string filesPath, string sourceFileName, string destinationFileName, string keysFileName)
        {
            this._directoryPath = filesPath; // does the path exist on the hard drive
            this._sourceFileName = sourceFileName;
            this._destinationFileName = destinationFileName;
            this._keyFileName = keysFileName;

            Provider = new AesCryptoServiceProvider();
            Transform = Provider.CreateEncryptor();
            ConvertKeyAndVector();
        }


        public string FileToString(string pathAndSourceFileName) 
        {
            string readContents;
            using (StreamReader streamReader = new StreamReader(pathAndSourceFileName, Encoding.UTF8))
            {
                readContents = streamReader.ReadToEnd();
            }
            return readContents;
        }


        public void StringToFile(string content, string pathAndDestinationFileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(pathAndDestinationFileName))
                {
                    writer.Write(content);
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }
        }


        public void DeleteFile(string pathAndTargeFileName)
        {
            try
            {
                // Check if file exists with its full path    
                if (File.Exists(pathAndTargeFileName))
                {
                    // If file found, delete it    
                    File.Delete(pathAndTargeFileName);
                    // Console.WriteLine("File deleted.");
                }
                else {

                    throw new FileNotFoundException();
                    // Console.WriteLine("File not found");
                }
            }           
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // OLD
        //public string[] FileToStringArray()
        //{

        //    // File name  
        //    string fileName = @"D:\GitHub\giancarlorhodes\EncryptDecrypt\files\testing.txt";
        //    List<string> list = new List<string>();

        //    try
        //    {
        //        // Create a StreamReader  
        //        using (StreamReader reader = new StreamReader(fileName))
        //        {
        //            string line;
        //            // Read line by line  
        //            while ((line = reader.ReadLine()) != null)
        //            {
        //                //Console.WriteLine(line);
        //                list.Add(line);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return list.ToArray();

        //}

        // OLD
        //public void EncryptTheFile()
        //{

        //    if (IsPathExisting(_path))
        //    {
        //        string pathAndFile = _path + "\\" + _sourceFile;

        //        if (IsFileExisting(pathAndFile))
        //        {
        //            SourceStream = File.OpenRead(pathAndFile);

        //            pathAndFile = _path + "\\" + _destinationFile;
        //            if (IsFileExisting(pathAndFile))
        //            {
        //                File.Delete(pathAndFile);
        //                DestinationStream = File.Create(pathAndFile);
        //            }
        //            else
        //            {
        //                DestinationStream = File.Create(pathAndFile);
        //            }

        //            pathAndFile = _path + "\\" + _keyFile;
        //            if (IsFileExisting(pathAndFile))
        //            {
        //                File.Delete(pathAndFile);
        //                using (StreamWriter writer = new StreamWriter(pathAndFile))
        //                {

        //                    writer.WriteLine("Init Vector: " + Convert.ToBase64String(Provider.IV));
        //                    writer.WriteLine("Private Key: " + Convert.ToBase64String(Provider.Key));
        //                    //this.PrivateKey = Provider.Key;
        //                    //this.PublicVector = Provider.IV;
        //                }
        //            }
        //            else
        //            {
        //                // file does not exist
        //                using (StreamWriter writer = new StreamWriter(pathAndFile))
        //                {

        //                    writer.WriteLine("Init Vector: " + Convert.ToBase64String(Provider.IV));
        //                    writer.WriteLine("Private Key: " + Convert.ToBase64String(Provider.Key));
        //                    //this.PrivateKey = Provider.Key;
        //                    //this.PublicVector = Provider.IV;
        //                }
        //            }
        //        }
        //        else 
        //        {
        //            // no source file
        //            return;

        //        }
        //    }
        //    else 
        //    {
        //        // not a path
        //        return;

        //    }

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
        //    using (CryptoStream cryptoStream = new CryptoStream(DestinationStream, Transform, CryptoStreamMode.Write))
        //    {
        //        //cryptoStream.Position = 0;

        //        SourceStream.CopyTo(cryptoStream);
        //        //cryptoStream.Flush();
        //        //DestinationStream.Flush();
        //        //cryptoStream.Close();
        //        //DestinationStream.Close();

        //    }
        //}

        // OLD VERSION
        //public void DecryptTheFile(string sourceFileName, string destinationFileName, byte[] privateKeyBytes, byte[] vectorBytes)
        //{
        //    // Decrypt the source file and write it to the destination file.
        //    using (var sourceStream = File.OpenRead(sourceFileName))
        //    using (var destinationStream = File.Create(destinationFileName))
        //    using (var provider = new AesCryptoServiceProvider())
        //    {
        //        //var IV = new byte[provider.IV.Length];
        //        sourceStream.Read(vectorBytes, 0, vectorBytes.Length);
        //        using (var cryptoTransform = provider.CreateDecryptor(privateKeyBytes, vectorBytes))
        //        using (var cryptoStream = new CryptoStream(sourceStream, cryptoTransform, CryptoStreamMode.Read))
        //        {
        //            cryptoStream.CopyTo(destinationStream);
        //        }
        //    }
        //}


        private void ConvertKeyAndVector()
        {
            this.PrivateKey = Convert.ToBase64String(Provider.Key);
            this.PublicVector = Convert.ToBase64String(Provider.IV);
        }

        private bool IsPathExisting(string path)
        {

            // https://stackoverflow.com/questions/1395205/better-way-to-check-if-a-path-is-a-file-or-a-directory
            FileAttributes attr = File.GetAttributes(path);

            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                return true;
            else
                return false;

        }

        private bool IsFileExisting(string file)
        {
            try
            {

                // get the file attributes for file or directory
                FileAttributes attr = File.GetAttributes(file);

                //detect whether its a directory or file
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    return false;
                else
                    return true;
            }
            catch(FileNotFoundException) 
            {

                return false;
            
            }
            catch (Exception)
            {

                return false;
            }         
        }
    }
}
