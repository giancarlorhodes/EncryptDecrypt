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


            // act
            var e = Crypto.EncryptStringAES(expected, _p);
            var d = Crypto.DecryptStringAES(e, _p);
            actual = d;

            // assert
            Assert.AreEqual(expected, actual);

        }


    }
}
