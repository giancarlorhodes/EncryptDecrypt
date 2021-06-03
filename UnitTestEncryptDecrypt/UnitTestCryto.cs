using ClassLibraryEncryptDecrypt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnitTestEncryptDecrypt
{


    [TestClass]
    public class UnitTestCryto
    {
        private Crypto _crypto { get; set; }

        public UnitTestCryto()
        {
            _crypto = new Crypto();
        }

        [TestMethod]
        public void create_a_guid_and_compare_to_cloned_guid()
        {

            // arrange
            Guid guid = Guid.NewGuid();
            Console.WriteLine($"Guid: {guid}");

            // act
            var bytes = guid.ToByteArray();
            foreach (var byt in bytes)
                Console.Write($"{byt:X2} ");
            Console.WriteLine();
            var guid2 = new Guid(bytes);
            Console.WriteLine($"Guid: {guid2} (Same as First Guid: {guid2.Equals(guid)})");

            // assert
            Assert.AreEqual(guid, guid2);
        }

        [TestMethod]
        public void encrypt_and_decrpt_some_text_and_they_should_be_equal_length()
        {

            // arrange
            string expected = "some random text";
            string actual = "";
            string _p = "password";
            Crypto c = new Crypto(_p);


            // act
            var e = c.EncryptStringAES(expected, _p);
            var d = Crypto.DecryptStringAES(e, _p);
            actual = d;

            // assert
            Assert.AreEqual(expected, actual);

        }


        [TestMethod]
        public void same_salt_and_password_results_in_same_private_key_and_vector()
        {

            // arrange            
            string salt = Guid.NewGuid().ToString();
            string _p = "password";

            // act
            Crypto expected = new Crypto(_p, salt);
            Crypto actual = new Crypto(_p, salt);

            // assert
            // this method is where key and vector get set
            expected.EncryptStringAES("test", _p);
            actual.EncryptStringAES("test", _p);

            Console.WriteLine("Private Key expected: " + expected.PrivateKey 
                + ", Private Key actual: " + actual.PrivateKey);
            Assert.AreEqual(expected.PrivateKey, actual.PrivateKey);
           
            Console.WriteLine("Public Vector expected: " + expected.PublicVector 
                + ", Public Vector actual: " + actual.PublicVector);
            Assert.AreNotEqual(expected.PublicVector, actual.PublicVector);
           
          
        }


    }
}
