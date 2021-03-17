using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace AddressBookLinq
{
    public class AddressBookService
    {
        /// <summary>
        /// UC1: Ability to create database
        /// </summary>
        private readonly DataSet AddressBookDB = new DataSet("AddressBookService");
        public DataTable AddressBook;
        /// <summary>
        /// UC2:AAbility to create table 
        /// </summary>
        public AddressBookService()
        {
            AddressBook = new DataTable("AddressBook");
            PersonType = new DataTable("ContactType");
            Type = new DataTable("Type");

            AddressBook.Columns.Add("FirstName", typeof(string));
            AddressBook.Columns.Add("LastName", typeof(string));
            AddressBook.Columns.Add("Address", typeof(string));
            AddressBook.Columns.Add("City", typeof(string));
            AddressBook.Columns.Add("State", typeof(string));
            AddressBook.Columns.Add("Zip", typeof(string));
            AddressBook.Columns.Add("PhoneNumber", typeof(string));
            AddressBook.Columns.Add("Email", typeof(string));
            AddressBook.Columns.Add("Name", typeof(string));
            AddressBook.PrimaryKey = new DataColumn[] { AddressBook.Columns["Name"] };
            AddressBookDB.Tables.Add(AddressBook);
        }
        /// <summary>
        /// Prints the table.
        /// </summary>
        public void PrintTable(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn column in dataTable.Columns)
                {
                    Console.WriteLine(column.ColumnName + " : " + row[column] + " ");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// UC 3: Adds the contact.
        /// </summary>

        public DataRow AddContact(Person person)
        {
            string Name = person.FirstName + " " + person.LastName;
            AddressBook.Rows.Add(person.FirstName, person.LastName,
                person.Address, person.City, person.State, person.Zip, person.PhoneNumber, person.Email, Name);
                 return AddressBook.Rows[^1];
        }




    }
}
