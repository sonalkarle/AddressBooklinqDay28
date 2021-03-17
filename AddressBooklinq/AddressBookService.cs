using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
        public DataTable Type;
        public DataTable PersonType;
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

            DataColumn rd = Type.Columns.Add("TypeID", typeof(int));
            rd.AutoIncrement = true;
            rd.AutoIncrementSeed = 1;
            rd.AutoIncrementStep = 1;
            Type.Columns.Add("Type", typeof(string));
            Type.Rows.Add(null, "Family");
            Type.Rows.Add(null, "Friends");
            Type.Rows.Add(null, "Profession");

            PersonType.Columns.Add("TypeID", typeof(int));
            PersonType.Columns.Add("Name", typeof(string));
            AddressBookDB.Tables.Add(Type);
            AddressBookDB.Tables.Add(PersonType);
            ForeignKeyConstraint foreignKeyOnContactTypeTypeID = new ForeignKeyConstraint(
                 "foreignKeyOnContactType_TypeID", Type.Columns["TypeID"], PersonType.Columns["TypeID"]);
            ForeignKeyConstraint foreignKeyOnContactTypeName = new ForeignKeyConstraint(
                 "foreignKeyOnContactType_Name", AddressBook.Columns["Name"], PersonType.Columns["Name"]);

            PersonType.Constraints.Add(foreignKeyOnContactTypeTypeID);
            PersonType.Constraints.Add(foreignKeyOnContactTypeName);
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
            int TypeID = Type.AsEnumerable().Where(type => type.Field<string>("Type").Equals(person.Type)).Select(type => type.Field<int>("TypeID")).FirstOrDefault();
            PersonType.Rows.Add(TypeID, Name);
            return AddressBook.Rows[^1];
        }

 
      
        /// <summary>
        ///  UC 5: Deletes the contact.
        /// </summary>
       
        public bool DeleteContact(string name)
        {
            DataRow row = AddressBook.AsEnumerable().Where(person => person.Field<string>("FirstName") + " " + person.Field<string>("LastName") == name)
                .FirstOrDefault();
            row.Delete();
            return row.RowState.Equals(DataRowState.Detached);
        }
        /// <summary>
        ///UC6: Retrieves the state of the persons from city or.
        /// </summary>

        public DataTable RetrievePersonsFromCityOrState(string field, string CityName)
        {
            DataTable table;
            try
            {
                var rows = AddressBook.AsEnumerable().Where(person => person.Field<string>(field) == CityName);
                table = rows.Any() ? rows.CopyToDataTable() : null;
            }
            catch (Exception)
            {
                throw;
            }
            return table;
        }

        /// <summary>
        /// UC7: Gets the state of the count of persons in city or.
        /// </summary>

        public int GetCountOfPersonsInCityOrState(string column, string cityName)
        {
            int count = 0;
            count = AddressBook.AsEnumerable().Where(person => person.Field<string>(column).Equals(cityName, StringComparison.OrdinalIgnoreCase)).Count();
            return count;
        }

       
      
        /// <summary>
        ///  UC8:Gets the sorted address book by persons name in city.
        /// </summary>
      
        public DataTable GetSortedAddressBookByPersonsNameInCity(string city)
        {
            return AddressBook.AsEnumerable().Where(person => person.Field<string>("City").Equals(city, StringComparison.OrdinalIgnoreCase))
                .OrderBy(person => person.Field<string>("FirstName") + " " + person.Field<string>("LastName")).CopyToDataTable();
        }

        /// <summary>
        /// UC10: Gets the count of persons in particular type.
        /// </summary>

        public int GetPersonCountByType(string type)
        {
            int TypeID = Type.AsEnumerable().Where(Type => Type.Field<string>("Type").Equals(type)).Select(Type => Type.Field<int>("TypeID")).FirstOrDefault();
            return PersonType.AsEnumerable().Count(person => person.Field<int>("TypeID").Equals(TypeID));

        }
    }
}