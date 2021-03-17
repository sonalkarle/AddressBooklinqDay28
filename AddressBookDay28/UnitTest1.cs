using System.Data;
using AddressBookLinq;
using NUnit.Framework;

namespace NUnitTestProject
{
    public class AddressBookLinqTest
    {
        AddressBookService addressBook;
        [SetUp]
        public void Setup()
        {
            addressBook = new AddressBookService();
        }

        /// <summary>
        /// UC-3 Givens the contact when added should return row.
        /// </summary>
        [Test]
        public void GivenContact_WhenAdded_ShouldReturnRow()
        {
            Person contact = new Person("Prachi", "Gore", "Shivajinagr", "Pune", "Maharashtra", "411 222", "91 8564123737", "Gore@gmail.com");
            DataRow result = addressBook.AddContact(contact);
            DataRow row = addressBook.AddressBook.NewRow();
            row["FirstName"] = "Prachi";
            row["LastName"] = "Gore";
            row["Address"] = "Shivajinagr";
            row["City"] = "Pune";
            row["State"] = "Maharashtra";
            row["Zip"] = "411 222";
            row["PhoneNumber"] = "91 8564123737";
            row["Email"] = "Gore@gmail.com";

            Assert.AreEqual(row["State"], result["State"]);
        }

    }
}