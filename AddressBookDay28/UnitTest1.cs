using AddressBooklinq;
using NUnit.Framework;
using System.Data;

namespace AddressBookDay28
{
    public class Tests
    {
        AddressBook addressBook;
        [SetUp]
        public void Setup()
        {
            addressBook = new AddressBook();
        }

        [Test]
        public void Giventable_whenchecked_tablepresentOrnot()
        {
            DataTable result = addressBook.CreateAddressBookTable();
            Assert.AreEqual(result.TableName, "AddressBook");


        }
    }
}