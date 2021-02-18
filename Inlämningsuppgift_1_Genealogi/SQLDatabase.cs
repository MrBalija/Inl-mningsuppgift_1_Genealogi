using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Inlämningsuppgift_1_Genealogi
{
    class SQLDatabase
    {

        // PROPERTIES:
        internal string ConnectionString { get; set; } = @"Data Source=.\SQLExpress;Integrated Security=true;database={0}";
        internal string DatabaseName { get; set; } = "My_Family_Tree";


        // GLOBAL VARIABLES:
        internal static SQLDatabase database = new SQLDatabase();


        // DATA TABLE: fetches data tables from the database.
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

        // SQL-EXECUTER: executes SQL-commands sent to the database.
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


        
        internal static void CreateDatabase(string databaseName)
        {

            if (database.DoesDatabaseExist(databaseName) == false)
            {
                // Create a database based on the '@databaseName' input.
                database.ExecuteSQL("CREATE DATABASE @databaseName",("@databaseName", databaseName));

                // Direct the user to proper database = '@databaseName' input.
                database.DatabaseName = databaseName;

            }
            else
            {
                // Direct the user to proper database = '@databaseName' input.
                database.DatabaseName = databaseName;
            }
                // CreateSkapar tabell 'People' med följande kolumner och datatyper.
                database.ExecuteSQL(@"CREATE TABLE People (
                                 ID int NOT NULL Identity (1,1),
                                 firstName varchar(255),
                                 lastName varchar(255),
                                 address varchar(255),
                                 city varchar(255),
                                 shoeSize int);"
                                  );

        }

        internal bool DoesDatabaseExist(string name)
        {
            var dataBase = GetDataTable(@"SELECT name 
                                          FROM sys.databases
                                          WHERE name = @name", ("@name", name)
                                       );
            if (dataBase == null)
            {
                return false;
            }
            return true;
        }
    }
}
