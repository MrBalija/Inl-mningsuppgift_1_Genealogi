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
        private string ConnectionString { get; set; } = @"Data Source=.\SQLExpress;Integrated Security=true;database={0}";
        public string DatabaseName { get; set; } = "Family_Tree";
        public string DataTableName { get; set; } = "My_Family_Tree";


        /// <summary>
        /// Fethes data tables from database.
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sqlString, params (string, string)[] parameters)
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

        /// <summary>
        /// Executes SQL-commands sent to the database (returns rows affected).
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public long ExecuteSQL(string sqlString, params (string, string)[] parameters)
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

        /// <summary>
        /// Creates a database.
        /// </summary>
        /// <param name="databaseName"></param>
        public static void CreateDatabase(string databaseName)
        {
            if (database.DoesDatabaseExist(databaseName) == false)
            {
                // Create a database based on the 'databaseName' input.
                //var databaseNameParam = ("@databaseName", databaseName);
                var sqlCreateDatabase = @"CREATE DATABASE " + databaseName;
                database.ExecuteSQL(sqlCreateDatabase);

                // Direct the user to the proper database = 'databaseName' input.
                database.DatabaseName = databaseName;
            }
            /*else
            {
                // If database exist, create a database based on the 'tableName' input and add '_New' at the end of the name.
                database.ExecuteSQL($"CREATE DATABASE {databaseName}_New;");

                // Direct the user to the proper database = 'databaseName' input.
                database.DatabaseName = databaseName;
            }*/
        }

        /// <summary>
        /// Checks if database exists.
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        private bool DoesDatabaseExist(string databaseName)
        {
            var databaseNameParam = ("@databaseName", databaseName.ToString());
            var sqlDoesDatabaseExist = @"SELECT name 
                                         FROM sys.databases
                                         WHERE name = @databaseName;";

            var dataBase = GetDataTable(sqlDoesDatabaseExist, databaseNameParam);
            if (dataBase == null)
            {
                return false;
            }
            return dataBase.Rows.Count > 0;
        }

        /// <summary>
        /// Creates a table.
        /// </summary>
        /// <param name="tableName"></param>
        public static void CreateTable(string tableName)
        {
            if (database.DoesTableExist(tableName) == false)
            {
                // Create a table based on the 'tableName' input.
                //var databaseNameParam = ("@databaseName", database.DatabaseName.ToString());
                //var dataTableNameParam = ("@dataTableName", tableName.ToString());
                var sqlCreateTable = @$"USE {database.DatabaseName}
                                       CREATE TABLE {tableName}(
                                       ID int NOT NULL Identity (1,1),
                                       Name varchar(30),
                                       [Last name] varchar(30),
                                       Birthplace varchar(30),
                                       [Country of birth] varchar(30),
                                       Born int,
                                       Mother varchar(30),
                                       Father varchar(30),
                                       [Vital status] varchar(30));";

                database.ExecuteSQL(sqlCreateTable);
                AddTableData(tableName.ToString());
            }
            /*else if (database.DoesTableExist(tableName + "_New") == false)
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
            }*/
        }

        /// <summary>
        /// Adds table data with family and relatives, 3 generations back.
        /// </summary>
        /// <param name="tableName"></param>
        private static void AddTableData(string tableName)
        {
            // Inserts data about persons to the table.
            var sqlAddTableData = @$"INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Majlinda', 'Balija', 'Mitrovicë', 'Kosovo', '1986', 'Dinore Balija', 'Xhafer Balija', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Fisnik', 'Balija', 'Mitrovicë', 'Kosovo', '1988', 'Dinore Balija', 'Xhafer Balija', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Granit', 'Balija', 'Över Kalix', 'Sweden', '1993', 'Dinore Balija', 'Xhafer Balija', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Dinore', 'Balija', 'Bajgorë', 'Kosovo', '1959', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Xhafer', 'Balija', 'Mitrovicë', 'Kosovo', '1959', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                   
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Fexhri', 'Duraku', 'Bajgorë', 'Kosovo', '1957', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Resmijë', 'Istrefi', 'Bajgorë', 'Kosovo', '1961', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Hatiqe', 'Hasani', 'Bajgorë', 'Kosovo', '1963', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Zoja', 'Xhemajli', 'Bajgorë', 'Kosovo', '1967', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Fatmirë', 'Kabashi', 'Mitrovicë', 'Kosovo', '1976', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Shashivar', 'Istrefi', 'Bajgorë', 'Kosovo', '1965', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Kadri', 'Istrefi', 'Bajgorë', 'Kosovo', '1969', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Bekim', 'Istrefi', 'Bajgorë', 'Kosovo', '1971', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Avni', 'Istrefi', 'Mitrovicë', 'Kosovo', '1973', 'Vahide Istrefi', 'Jetish Istrefi', 'Alive');
                                    
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Remzije', 'Zeqiri', 'Mitrovicë', 'Kosovo', '1965', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Enver', 'Baliu', 'Mitrovicë', 'Kosovo', '1961', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Murtez', 'Baliu', 'Mitrovicë', 'Kosovo', '1957', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Sylejman', 'Berisha', 'Mitrovicë', 'Kosovo', '1955', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Sinan', 'Baliu', 'Mitrovicë', 'Kosovo', '1953', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Ismajl', 'Osmani', 'Shtuticë', 'Kosovo', '1951', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Ramadan', 'Baliu', 'Shtuticë', 'Kosovo', '1949', 'Hat Baliu', 'Sadik Baliu', 'Alive');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Nasuf', 'Baliu', 'Shtuticë', 'Kosovo', '1947', 'Hat Baliu', 'Sadik Baliu', 'Deceased');
                                   
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Jetish', 'Istrefi', 'Bajgorë', 'Kosovo', '1931', 'Dylbere Istrefi', 'Imer Istrefi', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Zeka', 'Istrefi', 'Bajgorë', 'Kosovo', '1929', 'Dylbere Istrefi', 'Imer Istrefi', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Fadil', 'Istrefi', 'Bajgorë', 'Kosovo', '1933', 'Dylbere Istrefi', 'Imer Istrefi', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Mexhit', 'Istrefi', 'Bajgorë', 'Kosovo', '1935', 'Dylbere Istrefi', 'Imer Istrefi', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Isa', 'Istrefi', 'Bajgorë', 'Kosovo', '1937', 'Dylbere Istrefi', 'Imer Istrefi', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Mursel', 'Istrefi', 'Bajgorë', 'Kosovo', '1939', 'Dylbere Istrefi', 'Imer Istrefi', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Muhamet', 'Istrefi', 'Bajgorë', 'Kosovo', '1941', 'Dylbere Istrefi', 'Imer Istrefi', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Hanusha', 'Meholli', 'Bajgorë', 'Kosovo', '1929', 'Dylbere Istrefi', 'Imer Istrefi', 'Deceased');
                                    
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Vahide', 'Istrefi', 'Rahov', 'Kosovo', '1937', 'Raba Peci', 'Shiqiri Peci', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Ismja', 'Vallqi', 'Rahov', 'Kosovo', '1927', 'Raba Peci', 'Shiqiri Peci', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Arife', 'Unknown', 'Rahov', 'Kosovo', '1929', 'Raba Peci', 'Shiqiri Peci', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Fazile', 'Unknown', 'Rahov', 'Kosovo', '1931', 'Raba Peci', 'Shiqiri Peci', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Velia', 'Peci', 'Rahov', 'Kosovo', '1933', 'Raba Peci', 'Shiqiri Peci', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Vehbia', 'Peci', 'Rahov', 'Kosovo', '1935', 'Raba Peci', 'Shiqiri Peci', 'Deceased');
                                    
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Sadik', 'Baliu', 'Shtuticë', 'Kosovo', '1928', 'Hateme Baliu', 'Osman Baliu', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Bali', 'Baliu', 'Shtuticë', 'Kosovo', '1930', 'Hateme Baliu', 'Osman Baliu', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                     VALUES ('Sefer', 'Baliu', 'Shtuticë', 'Kosovo', '1932', 'Hateme Baliu', 'Osman Baliu', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Zenel', 'Baliu', 'Shtuticë', 'Kosovo', '1934', 'Hateme Baliu', 'Osman Baliu', 'Deceased');
                                    
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Hat', 'Baliu', 'Likofcë', 'Kosovo', '1932', 'Han Rexhepi', 'Hiti Rexhepi', 'Deceased');
                                    INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                      VALUES ('Haxhi', 'Rexhepi', 'Likofcë', 'Kosovo', '1942', 'Han Rexhepi', 'Hiti Rexhepi', 'Deceased');";
                                  
                                    //INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                    //  VALUES ('Imer', 'Istrefi', 'Bajgorë', 'Kosovo', '1905', 'Unknown', 'Unknown', 'Deceased');
                                    //INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                    //  VALUES ('Dylbere', 'Istrefi', 'Bare', 'Kosovo', '1911', 'Unknown', 'Unknown', 'Deceased');
                                  
                                    //INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                    //  VALUES ('Shiqiri', 'Peci', 'Rahov', 'Kosovo', '1902', 'Unknown', 'Unknown', 'Deceased');
                                    //INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                    //  VALUES ('Raba', 'Peci', 'Rahov', 'Kosovo', '1905', 'Unknown', 'Unknown', 'Deceased');
                                  
                                    //INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                    //  VALUES ('Osman', 'Baliu', 'Shtuticë', 'Kosovo', '1905', 'Unknown', 'Unknown', 'Deceased');
                                    //INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                    //  VALUES ('Hateme', 'Baliu', 'Llaush', 'Kosovo', '1907', 'Unknown', 'Unknown', 'Deceased');
                                  
                                    //INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                    //  VALUES ('Hiti', 'Rexhepi', 'Likofcë', 'Kosovo', '1904', 'Unknown', 'Unknown', 'Deceased');
                                    //INSERT INTO {tableName} (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                    //  VALUES ('Han', 'Rexhepi', 'Prekaz', 'Kosovo', '1907', 'Unknown', 'Unknown', 'Deceased');";

            database.ExecuteSQL(sqlAddTableData);

            // Adds Age-column to the table.
            database.AlterTableAdd(tableName, "Age varchar(30)");

            // Updates the age for the persons; if Age = alive -> calculate age, else if Age = deceased -> add R.I.P. 
            CRUD.UpdateColumnAge(tableName);
        }

        // DOES TABLE EXIST: Checks if table name exists.
        /// <summary>
        /// Checks if table exists.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        internal bool DoesTableExist(string tableName)
        {
            var dataTableName = ("@dataTableName", tableName.ToString());
            var sqlDoesTableExist = @"SELECT name 
                                      FROM sys.tables
                                      WHERE name = @dataTableName;";

            var table = GetDataTable(sqlDoesTableExist, dataTableName);
           
            if (table == null)
            {
                return false;
            }
            return table.Rows.Count > 0;
        }

        /// <summary>
        /// Adds a new column with data type to a table.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnsWithDataType"></param>
        private void AlterTableAdd(string tableName, string columnsWithDataType)
        {
            var sqlColumnsWithDataType = @$"ALTER TABLE {tableName}
                                            ADD {columnsWithDataType}";

            ExecuteSQL(sqlColumnsWithDataType);
        }

        /// <summary>
        /// Adds a new person to a table.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="birthplace"></param>
        /// <param name="countryOfBirth"></param>
        /// <param name="born"></param>
        /// <param name="mother"></param>
        /// <param name="father"></param>
        /// <param name="vitalStatus"></param>
        public static void InsertPersonToTable(string name, string lastName, string birthplace, string countryOfBirth, int born, string mother, string father, string vitalStatus)
        {
            var dataTableNameParam = ("@dataTableName", database.DataTableName.ToString());
            var nameParam = ("@name", name.ToString());
            var lastNameParam = ("@lastName", lastName.ToString());
            var birthplaceParam = ("@birthplace", birthplace.ToString());
            var countryOfBirthParam = ("@countryOfBirth", countryOfBirth.ToString());
            var bornParam = ("@born", born.ToString());
            var motherParam = ("@mother", mother.ToString());
            var fatherParam = ("@father", father.ToString());
            var vitalStatusParam = ("@vitalStatus", vitalStatus.ToString());
            var sqlInsertPersonToTable = @"INSERT INTO @dataTableName (Name, [Last name], Birthplace, [Country of birth], Born, Mother, Father, [Vital status]) 
                                           VALUES (@name, @lastName, @birthplace, @countryOfBirth, @born, @mother, @father, @vitalStatus);";

            database.ExecuteSQL(sqlInsertPersonToTable, dataTableNameParam, nameParam, lastNameParam, birthplaceParam, countryOfBirthParam, bornParam, motherParam,
                fatherParam, vitalStatusParam);
        }
    }
}
