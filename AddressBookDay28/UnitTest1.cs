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

        [Test]
        public void GivenTable_WhenChecked_ShouldRetunrTable()
        {
            DataTable result = addressBook.CreateAddressBookTable();
            Assert.AreEqual(result.TableName, "AddressBook");
        }

        [Test]
        public void GivenContact_WhenAdded_ShouldReturnTrue()
        {
            addressBook.CreateAddressBookTable();
            Person person = new Person("Sonal", "Karle", "Ghatkopar", "Mumbai", "Maharashtra", "400 075", "91 2837373737", "sonal@gmail.com");
            DataRow result = addressBook.AddContact(person);
            DataRow row = addressBook.AddressBook.NewRow();
            row["FirstName"] = "Sonal";
            row["LastName"] = "Karle";
            row["Address"] = "Ghatkopar";
            row["City"] = "Mumabi";
            row["State"] = "Maharshtra";
            row["Zip"] = "400 075";
            row["PhoneNumber"] = "91 2837373737";
            row["Email"] = "sonal@gmail.com";

            Assert.AreEqual(row["FirstName"], result["FirstName"]);
        }

        [Test]
        public void GivenTable_EditedUsingName_ShouldReturnDataRow()
        {
            Person person = new Person("Sonal", "Karle", "Ghatkopar", "Mumbai", "Maharashtra", "400 075", "91 2837373737", "sonal@gmail.com");
            addressBook.AddContact(person);
            DataRow result = addressBook.EditContactUsingName("Sonal Karle", "Address", "Kurla");
            DataRow row = addressBook.AddressBook.NewRow();
            row["FirstName"] = "Sonal";
            row["LastName"] = "Karle";
            row["Address"] = "Kurla";
            row["City"] = "Mumabi";
            row["State"] = "Maharshtra";
            row["Zip"] = "400 075";
            row["PhoneNumber"] = "91 2837373737";
            row["Email"] = "sonal@gmail.com";


            Assert.AreEqual(row[2], result[2]);
        }
    }
}