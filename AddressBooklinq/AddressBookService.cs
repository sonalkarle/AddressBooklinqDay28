using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace AddressBookLinq
{
    public class AddressBookService
    {
        private DataSet AddressBookDB = new DataSet("AddressBookService");
        public DataTable AddressBook;
        public DataTable CreateAddressBookTable()
        {
            AddressBook = new DataTable("AddressBook");
            DataColumn column;
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "FirstName";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "LastName";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Address";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "City";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "State";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Zip";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "PhoneNumber";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Email";
            AddressBook.Columns.Add(column);
            AddressBookDB.Tables.Add(AddressBook);
            return AddressBook;
        }

        public DataRow AddContact(Person person)
        {
            AddressBook.Rows.Add(person.FirstName, person.LastName,
                person.Address, person.City, person.State, person.Zip, person.PhoneNumber, person.Email);
            return AddressBook.Rows[AddressBook.Rows.Count - 1];
        }

        public DataRow EditContactUsingName(string name, string Column, string data)
        {
            DataRow row = AddressBook.AsEnumerable().Where(person => person.Field<string>("FirstName") + " " + person.Field<string>("LastName") == name)
                .FirstOrDefault();
            row[Column] = data;
            return row;
        }

        
        public void printTable(DataTable dataTable)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    Console.Write(row[column] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
