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
            string e = Crypto.EncryptStringAES(s, null);
            _worker.DeleteFile(_encryptedCSharpFileName);
            _worker.StringToFile(e, _encryptedCSharpFileName);

            string d = Crypto.DecryptStringAES(e, null);
            _worker.DeleteFile(_decryptedCSharpFileName);
            _worker.StringToFile(d, _decryptedCSharpFileName);

            // assert
            Assert.AreEqual(s.Length, d.Length);
        }


        //    [TestMethod]
        //public void not_null_private_key_and_public_vector()
        //{
        //    Console.WriteLine("Init Vector: " + _worker.PublicVector);
        //    Console.WriteLine("Private Key: " + _worker.PrivateKey);

        //    Assert.IsTrue(_worker.PublicVector != null && _worker.PrivateKey != null);

        //}


        
    }
}
