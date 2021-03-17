using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AddressBooklinq
{

    public class AddressBook
    {
        /// <summary>
        /// UC1: Ability to create database
        /// </summary>
        private DataSet AddressBookDB;

        /// <summary>
        /// UC2:AAbility to create table 
        /// </summary>
        public DataTable CreateAddressBookTable()
        {
            DataTable AddressBook = new DataTable("AddressBook");
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
            column.DataType = typeof(int);
            column.ColumnName = "Zip";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "PhoneNumber";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "email";
            AddressBook.Columns.Add(column);

            return AddressBook;
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
    }
}

