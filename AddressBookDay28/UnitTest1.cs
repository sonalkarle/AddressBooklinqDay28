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
            addressBook.CreateAddressBookTable();
        }

        [Test]
        public void GivenTable_WhenChecked_ShouldRetunrTable()
        {
            Assert.AreEqual(addressBook.AddressBook.TableName, "AddressBook");
        }

        [Test]
        public void GivenContact_WhenAdded_ShouldReturnRow()
        {
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

            Assert.AreEqual(row["Zip"], result["Zip"]);
        }

        [Test]
        public void GivenTable_EditedUsingName_ShouldReturnDataRow()
        {
            Person person = new Person("Sonal", "Karle", "Ghatkopar", "Mumbai", "Maharashtra", "400 075", "91 2837373737", "sonal@gmail.com");
            addressBook.AddContact(person);
            DataRow result = addressBook.EditContactUsingName("Sam Sher", "Address", "kondhwa");
            DataRow row = addressBook.AddressBook.NewRow();
            row["FirstName"] = "Sonal";
            row["LastName"] = "Karle";
            row["Address"] = "Ghatkopar";
            row["City"] = "Mumabi";
            row["State"] = "Maharshtra";
            row["Zip"] = "400 075";
            row["PhoneNumber"] = "91 2837373737";
            row["Email"] = "sonal@gmail.com";


            Assert.AreEqual(row[2], result[2]);
        }

        [Test]
        public void GivenTable_DeletedContact_ShouldReturnTrue()
        {
            Person person = new Person("Sonal", "Karle", "Ghatkopar", "Mumbai", "Maharashtra", "400 075", "91 2837373737", "sonal@gmail.com");
            Person person1 = new Person("Sona", "Karle", "Kurla", "mumbai", "Maharashtra", "400 022", "91 2838973737", "sona@gmail.com");

            addressBook.AddContact(person);
            addressBook.AddContact(person1);
            bool result = addressBook.DeleteContact("Sona Karle");
            Assert.IsTrue(result);
        }
        [Test]
        public void GivenTable_WhenRetrievePersonsBelongToCityOrState_ShouldReturnDataTable()
        {
            Person person = new Person("Sonal", "Karle", "Ghatkopar", "Mumbai", "Maharashtra", "400 075", "91 2837373737", "sonal@gmail.com");
            Person person1 = new Person("Sona", "Karle", "Kurla", "mumbai", "Maharashtra", "400 022", "91 2838973737", "sona@gmail.com");

            addressBook.AddContact(person);
            addressBook.AddContact(person1);
            DataRow row = addressBook.AddressBook.NewRow();
            row["FirstName"] = "Sona";
            row["LastName"] = "Karle";
            row["Address"] = "Kurla";
            row["City"] = "mumabi";
            row["State"] = "Maharshtra";
            row["Zip"] = "400 022";
            row["PhoneNumber"] = "91 2838973737";
            row["Email"] = "sona@gmail.com";
            DataTable table = addressBook.RetrievePersonsFromCityOrState("City", "mumbai");
            Assert.AreEqual(row["City"], table.Rows[0]["City"]);

        }


    }
}