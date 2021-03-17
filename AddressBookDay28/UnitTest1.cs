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
           Person contact = new Person("Sonal", "Karle", "Ghatkopar", "Mumbai", "Maharashtra", "400 075", "91 9702420754", "sonal@gmail.com", "Friends");
            Person contact1 = new Person("Sona", "Karle", "Gokhalenagar", "Pune", "Maharashtra", "411 016", "91 8806184089", "sona@gmail.com", "Family");
            Person contact2 = new Person("Ajinkya", "Patil", "Gandhinagar", "Ahmdabad", "Gujrat", "512 222", "91 7854373737", "Ajinkya@gmail.com", "Profession");
            Person contact3 = new Person("Yogesh", "Kadam", "Nashik", "Nashik", "Maharashtra", "400 022", "91 8657373737", "kadam@gmail.com", "Family");

            addressBook.AddContact(contact);
            addressBook.AddContact(contact1);
            addressBook.AddContact(contact2);
            addressBook.AddContact(contact3);
        }
        ///  <summary>
        /// UC-2 Givens the table when checked should return table.
        /// </summary>
        [Test]
        public void GivenTable_WhenChecked_ShouldRetrnTable()
        {
            Assert.AreEqual(addressBook.AddressBook.TableName, "AddressBook");
        }
        /// <summary>
        /// UC-3 Givens the contact when added should return row.
        /// </summary>
        [Test]
        public void GivenContact_WhenAdded_ShouldReturnRow()
        {
            Person contact = new Person("Prachi", "Gore", "Shivajinagr", "Pune", "Maharashtra", "411 222", "91 8564123737", "Gore@gmail.com", "Family");
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
       
        /// <summary>
        ///UC5: Givens the table when deleted contact should return true.
        /// </summary>
        [Test]
        public void GivenTable_WhenDeletedContact_ShouldReturnTrue()
        {
            bool result = addressBook.DeleteContact("Ajinkya Patil");
            Assert.IsTrue(result);
        }
        /// <summary>
        ///UC6: Givens the table when retrieve persons belong to city or state should return data table.
        /// </summary>
        [Test]
        public void GivenTable_WhenRetrievePersonsBelongToCityOrState_ShouldReturnDataTable()
        {
            DataRow row = addressBook.AddressBook.NewRow();
            row["FirstName"] = "Prachi";
            row["LastName"] = "Gore";
            row["Address"] = "Shivajinagr";
            row["City"] = "Pune";
            row["State"] = "Maharashtra";
            row["Zip"] = "411 222";
            row["PhoneNumber"] = "91 8564123737";
            row["Email"] = "Gore@gmail.com";

            DataTable table = addressBook.RetrievePersonsFromCityOrState("City", "Pune");
            Assert.AreEqual(row["City"], table.Rows[0]["City"]);
        }
        /// <summary>
        ///  UC 7:Givens the table when queried size of address book by city or state should return expected.
        /// </summary>
        [Test]
        public void GivenTable_WhenQueriedSizeOfAddressBookByCityOrState_ShouldReturnExpected()
        {
            int result = addressBook.GetCountOfPersonsInCityOrState("City", "Nashik");
            Assert.AreEqual(1, result);
        }
        /// <summary>
        /// UC8:Givens the table when retrieve sorted address book in city should return expected.
        /// </summary>
        [Test]
        public void GivenTable_WhenRetrieveSortedAddressBookInCity_ShouldReturnExpected()
        {
            DataTable result = addressBook.GetSortedAddressBookByPersonsNameInCity("Mumbai");
            Assert.AreEqual(result.Rows[0]["FirstName"], "Sonal");
        }
        /// <summary>
        /// UC10:Retrive count of people through its type
        /// </summary>
        [Test]
        public void GivenTable_WhenRetrievePersonsCountByType_ShouldReturnExpected()
        {
            int result = addressBook.GetPersonCountByType("Family");
            Assert.AreEqual(2, result);
        }
    }
}