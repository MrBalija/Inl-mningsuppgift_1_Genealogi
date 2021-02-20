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
        internal static string DataTableName { get; set; } = "My_Family_Tree";


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
                // If database exist, create a databse based on the 'tableName' input and add '_New' at the end of the name.
                database.ExecuteSQL($"CREATE DATABASE {databaseName}_New;");

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
                                       Mother varchar(30),
                                       Father varchar(30),
                                       [Vital status] varchar(30));"
                                   );
                AddTableData(tableName);
            }
            else
            {
                // If table exist, create a table based on the 'tableName' input and add '_New' at the end of the name.
                database.ExecuteSQL(@$"USE {database.DatabaseName}
                                       CREATE TABLE {tableName}_New(
                                       ID int NOT NULL Identity (1,1),
                                       Name varchar(30),
                                       [Last name] varchar(30),
                                       Birthplace varchar(30),
                                       [Country of birth] varchar(30),
                                       Born int,
                                       Mother varchar(30),
                                       Father varchar(30),
                                       [Vital status] varchar(30));"
                                   );
                AddTableData(tableName);
            }
        }

        //TABLE DATA: Data with family and relatives, 3 generations back.
        internal static void AddTableData(string tableName)
        {
            database.ExecuteSQL(@$"insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Majlinda', 'Balija', 'Mitrovicë', 'Kosovo', '1986', 'Dinore Balija', 'Xhafer Balija', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Fisnik', 'Balija', 'Mitrovicë', 'Kosovo', '1988', 'Dinore Balija', 'Xhafer Balija', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Granit', 'Balija', 'Över Kalix', 'Sweden', '1993', 'Dinore Balija', 'Xhafer Balija', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Dinore', 'Balija', 'Bajgorë', 'Kosovo', '1959', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Xhafer', 'Balija', 'Mitrovicë', 'Kosovo', '1959', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                  
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Fexhri', 'Duraku', 'Bajgorë', 'Kosovo', '1957', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Resmijë', 'Istrefi', 'Bajgorë', 'Kosovo', '1961', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Hatiqe', 'Hasani', 'Bajgorë', 'Kosovo', '1963', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Zoja', 'Xhemajli', 'Bajgorë', 'Kosovo', '1967', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Fatmirë', 'Kabashi', 'Mitrovicë', 'Kosovo', '1976', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Shashivar', 'Istrefi', 'Bajgorë', 'Kosovo', '1965', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Kadri', 'Istrefi', 'Bajgorë', 'Kosovo', '1969', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Bekim', 'Istrefi', 'Bajgorë', 'Kosovo', '1971', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Avni', 'Istrefi', 'Mitrovicë', 'Kosovo', '1973', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                   
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Remzije', 'Zeqiri', 'Mitrovicë', 'Kosovo', '1965', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Enver', 'Baliu', 'Mitrovicë', 'Kosovo', '1961', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Murtez', 'Baliu', 'Mitrovicë', 'Kosovo', '1957', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Sylejman', 'Berisha', 'Mitrovicë', 'Kosovo', '1955', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Sinan', 'Baliu', 'Mitrovicë', 'Kosovo', '1953', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Ismajl', 'Osmani', 'Shtuticë', 'Kosovo', '1951', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Ramadan', 'Baliu', 'Shtuticë', 'Kosovo', '1949', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Nasuf', 'Baliu', 'Shtuticë', 'Kosovo', '1947', 'Hat Baliu', 'Sadik Baliu', 'Deceased');
                                  
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Jetish', 'Istrefi', 'Bajgorë', 'Kosovo', '1931', 'Dylbere Istrefi', 'Ismer Istrefi', 'Deceased');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Zeka', 'Istrefi', 'Bajgorë', 'Kosovo', '1929', 'Dylbere Istrefi', 'Ismer Istrefi', 'Alive');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Fadil', 'Istrefi', 'Bajgorë', 'Kosovo', '1933', 'Dylbere Istrefi', 'Ismer Istrefi', 'Deceased');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Mexhit', 'Istrefi', 'Bajgorë', 'Kosovo', '1935', 'Dylbere Istrefi', 'Ismer Istrefi', 'Deceased');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Isa', 'Istrefi', 'Bajgorë', 'Kosovo', '1937', 'Dylbere Istrefi', 'Ismer Istrefi', 'Deceased');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Mursel', 'Istrefi', 'Bajgorë', 'Kosovo', '1939', 'Dylbere Istrefi', 'Ismer Istrefi', 'Deceased');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Muhamet', 'Istrefi', 'Bajgorë', 'Kosovo', '1941', 'Dylbere Istrefi', 'Ismer Istrefi', 'Deceased');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Hanusha', 'Meholli', 'Bajgorë', 'Kosovo', '1929', 'Dylbere Istrefi', 'Ismer Istrefi', 'Deceased');
                                   
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Vahide', 'Istrefi', 'Rahov', 'Kosovo', '1937', 'Raba Peci', 'Shiqiri Peci', 'Deceased');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Ismja', 'Vallqi', 'Rahov', 'Kosovo', '1927', 'Raba Peci', 'Shiqiri Peci', 'Deceased');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Arife', 'Unknown', 'Rahov', 'Kosovo', '1929', 'Raba Peci', 'Shiqiri Peci', 'Deceased');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Fazile', 'Unknown', 'Rahov', 'Kosovo', '1931', 'Raba Peci', 'Shiqiri Peci', 'Deceased');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Velia', 'Peci', 'Rahov', 'Kosovo', '1933', 'Raba Peci', 'Shiqiri Peci', 'Deceased');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Vehbia', 'Peci', 'Rahov', 'Kosovo', '1935', 'Raba Peci', 'Shiqiri Peci', 'Deceased');
                                   
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Sadik', 'Baliu', 'Shtuticë', 'Kosovo', '1928', 'Hateme Baliu', 'Osman Baliu', 'Deceased');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Bali', 'Baliu', 'Shtuticë', 'Kosovo', '1930', 'Hateme Baliu', 'Osman Baliu', 'Deceased');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                    values ('Sefer', 'Baliu', 'Shtuticë', 'Kosovo', '1932', 'Hateme Baliu', 'Osman Baliu', 'Deceased');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Zenel', 'Baliu', 'Shtuticë', 'Kosovo', '1934', 'Hateme Baliu', 'Osman Baliu', 'Deceased');
                                   
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Hat', 'Baliu', 'Likofcë', 'Kosovo', '1932', 'Han Rexhepi', 'Hiti Rexhepi', 'Deceased');
                                   insert into {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('Haxhi', 'Rexhepi', 'Likofcë', 'Kosovo', '1942', 'Han Rexhepi', 'Hiti Rexhepi', 'Deceased');"
                               );
            /*
            database.AlterTableAdd(tableName, "Age int");


            database.ExecuteSQL(@$"insert into {tableName} (Age)
                                   VALUES(
                                    CASE
                                       WHEN [Vital status] = 'Alive' THEN CONVERT(varchar(30), DATEDIFF(year, 'Birthday', 2020))
                                       ELSE 'RIP')
                                    END"
                                );
            */
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

        // ADD COLUMN: Adds a column to a desired table with field name and data type of the field.
        internal void AlterTableAdd(string table, string fieldsWithDataType)
        {
            ExecuteSQL(@$"ALTER TABLE {table}
                          ADD {fieldsWithDataType}"
                      );
        }

        internal static void InsertPersonToTable (string name, string lastName, string birthplace, string countryOfBirth, 
                                           int born, string mother, string father, string vitalStatus)
        {
            database.ExecuteSQL(@$"insert into {DataTableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     values ('{name}', '{lastName}', '{birthplace}', '{countryOfBirth}', '{born}', '{mother}', '{father}', '{vitalStatus}');"
                               );
        }
    }
}
