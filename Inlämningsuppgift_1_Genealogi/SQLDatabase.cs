using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Inlämningsuppgift_1_Genealogi
{
    class SQLDatabase
    {

        // GLOBAL VARIABLES:
        internal static SQLDatabase database = new SQLDatabase();

        // PROPERTIES:
        internal string ConnectionString { get; set; } = @"Data Source=.\SQLExpress;Integrated Security=true;database={0}";
        internal string DatabaseName { get; set; } = "Master";


        // DATA TABLE: Fetches data tables from the database.
        internal DataTable GetDataTable(string sqlString, params (string, string)[] parameters)
        {
            var dataTable = new DataTable(); // Förbered Datatable
            var connectionString = string.Format(ConnectionString, DatabaseName);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Koppla till databasen
                using (var command = new SqlCommand(sqlString, connection)) // Förbered ett kommando
                {
                    foreach (var item in parameters)
                    {
                        command.Parameters.AddWithValue(item.Item1, item.Item2); // Lägg in parametern, t.ex. @param, @name, @age etc. bara den finns med i SQL strängen.
                    }

                    // Förbered en adapter för att omvandla informationen
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable); // Kopiera från databasen till dataTable
                    }
                }
                connection.Close(); // Stäng kopplingen till databasen
            }
            return dataTable; // Returnera DataTable
        }

        // SQL-EXECUTER: Executes SQL-commands sent to the database.
        internal long ExecuteSQL(string sqlString, params (string, string)[] parameters)
        {
            long rowsAffected = 0;
            var connectionString = string.Format(ConnectionString, DatabaseName);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sqlString, connection))
                {
                    foreach (var item in parameters)
                    {
                        command.Parameters.AddWithValue(item.Item1, item.Item2);
                    }
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }

        // DATABASE: Creates a database.
        internal static void CreateDatabase(string databaseName)
        {
            if (database.DoesDatabaseExist(databaseName) == false)
            {
                // Create a database based on the 'databaseName' input.
                database.ExecuteSQL($"CREATE DATABASE {databaseName};");

                // Direct the user to the proper database = 'databaseName' input.
                database.DatabaseName = databaseName;
            }
            else
            {
                // Direct the user to the proper database = 'databaseName' input.
                database.DatabaseName = databaseName;
            }
        }

        // DOES DATABASE EXIST: Checks if database name exists.
        internal bool DoesDatabaseExist(string name)
        {
            var dataBase = GetDataTable(@$"SELECT name 
                                           FROM sys.databases
                                           WHERE name = '{name}';"
                                       );
            if (dataBase == null)
            {
                return false;
            }
            return dataBase.Rows.Count > 0;
        }

        // TABLE: Creates a table.
        internal static void CreateTable(string tableName)
        {
            if (database.DoesTableExist(tableName) == false)
            {
                // Create a table based on the 'tableName' input.
                database.ExecuteSQL(@$"USE {database.DatabaseName}
                                       CREATE TABLE {tableName}(
                                       ID int NOT NULL Identity (1,1),
                                       Name varchar(30),
                                       [Last name] varchar(30),
                                       Birthplace varchar(30),
                                       [Country of birth] varchar(30),
                                       Born int,
                                       Mother int,
                                       Father int,
                                       [Vital status] varchar(30));"
                                   );

                AddTableData();
            }
        }

        internal static void AddTableData()
        {
            database.ExecuteSQL(@"insert into People (Name, Last name, Birthplace, Country of birth, Born, Mother, Father, Vital status) 
                                    values ('Majlinda', 'Balija', 'Mitrovicë', 'Kosovo', '1986', 'Dinore Balija', 'Xhafer Balija', 'Alive');
                                  insert into People (Name, Last name, Birthplace, Country of birth, Born, Mother, Father, Vital status) 
                                    values ('Fisnik', 'Balija', 'Mitrovicë', 'Kosovo', '1988', 'Dinore Balija', 'Xhafer Balija', 'Alive');
                                  insert into People (Name, Last name, Birthplace, Country of birth, Born, Mother, Father, Vital status) 
                                    values ('Granit', 'Balija', 'Över Kalix', 'Sweden', '1993', 'Dinore Balija', 'Xhafer Balija', 'Alive');
                                  insert into People (Name, Last name, Birthplace, Country of birth, Born, Mother, Father, Vital status) 
                                    values ('Dinore', 'Balija', 'Bajgorë', 'Kosovo', '1959', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                  insert into People (Name, Last name, Birthplace, Country of birth, Born, Mother, Father, Vital status) 
                                    values ('Xhafer', 'Balija', 'Mitrovicë', 'Kosovo', '1959', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                 "
                               );
        }

            // DOES TABLE EXIST: Checks if database name exists.
            internal bool DoesTableExist(string name)
        {
            var table = GetDataTable(@$"SELECT name 
                                           FROM sys.tables
                                           WHERE name = '{name}';"
                                       );
            if (table == null)
            {
                return false;
            }
            return table.Rows.Count > 0;
        }


    }
}
