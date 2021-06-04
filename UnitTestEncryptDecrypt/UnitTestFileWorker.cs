using ClassLibraryEncryptDecrypt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnitTestEncryptDecrypt
{
    [TestClass]
    public class UnitTestFileWorker
    {
        private FileWorker _worker;

        private string _testFileName = @"D:\GitHub\giancarlorhodes\EncryptDecrypt\files\testing.txt";
        private string _destFileName = @"D:\GitHub\giancarlorhodes\EncryptDecrypt\files\destination.txt";

        private string _testCSharpFilName = @"D:\GitHub\giancarlorhodes\EncryptDecrypt\files\testing.cs";
        private string _encryptedCSharpFileName = @"D:\GitHub\giancarlorhodes\EncryptDecrypt\files\encryptedtesting.cs";
        private string _decryptedCSharpFileName = @"D:\GitHub\giancarlorhodes\EncryptDecrypt\files\decryptedtesting.cs";

        private string _keysFileName = @"D:\GitHub\giancarlorhodes\EncryptDecrypt\files\keys.txt";

        public UnitTestFileWorker()
        {
            _worker = new FileWorker();

        }

        [TestMethod]
        public void file_to_string_length_greater_than_zero()
        {
            // arrange
            string expected = _worker.FileToString(_testFileName);

            // act
            Console.WriteLine("string length: " + expected.Length);

            // assert
            Assert.IsTrue(expected.Length > 0);
        }


        [TestMethod]
        public void file_to_string_to_file_comparing_lengths()
        {
            // arrange
            string expected = _worker.FileToString(_testFileName);

            // act
            Console.WriteLine("string length: " + expected.Length);
            _worker.DeleteFile(_destFileName);
            _worker.StringToFile(expected, _destFileName);
            string actual = _worker.FileToString(_destFileName);

            // assert
            Assert.IsTrue(expected.Length == actual.Length);
        }


        [TestMethod]
        public void encrypt_and_decrypt_file_and_check_length()
        {
            // arrange
            string s = _worker.FileToString(_testCSharpFilName);
            Crypto c = new Crypto("password");

            // act
            string e = c.EncryptStringAES(s, null);
            _worker.DeleteFile(_encryptedCSharpFileName);
            _worker.StringToFile(e, _encryptedCSharpFileName);

            string d = c.DecryptStringAES(e, null);
            _worker.DeleteFile(_decryptedCSharpFileName);
            _worker.StringToFile(d, _decryptedCSharpFileName);

            // assert
            Assert.AreEqual(s.Length, d.Length);
        }


        [TestMethod]
        public void store_the_keys_in_a_file()
        {
            // Console.WriteLine("Init Vector: " + _worker.PublicVector);
            // Console.WriteLine("Private Key: " + _worker.PrivateKey);

            // arrange
            string _p = "password";
            Guid guid = Guid.NewGuid();
            string _salt = Guid.NewGuid().ToString();
            Crypto c = new Crypto(_p, _salt);
            string s = "some random content";
            

            // act
            string e = c.EncryptStringAES(s, null); // need to call this to create keys
            _worker.DeleteFile(_keysFileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Password: " + _p);
            sb.AppendLine("Salt: " + _salt);
            sb.AppendLine("Init Vector: " + c.PublicVector);
            sb.AppendLine("Private Key: " + c.PrivateKey);
           
            _worker.StringToFile(sb.ToString(), _keysFileName);


            // assert
            Console.WriteLine("Password: " + _p);
            Console.WriteLine("Salt: " + _salt);
            Console.WriteLine("Init Vector: " + c.PublicVector);
            Console.WriteLine("Private Key: " + c.PrivateKey);

        }



    }
}
