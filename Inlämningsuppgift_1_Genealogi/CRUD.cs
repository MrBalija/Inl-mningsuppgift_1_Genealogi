using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;

namespace Inlämningsuppgift_1_Genealogi
{
    class CRUD
    {
        // FILL FORM: displays a form with the properties/information to fill in for a new person that is added to the list.

        public static bool quitAddPerson;
        public static string personBorn;
        public static Person person = new Person();

        // CRUD --> "C" = Create:
        // CREATE: Creates an objekt Person
        public static void Create(Person person)
        {
            quitAddPerson = false;

            // CLEAR FORM: resets the form.
            ClearCreateForm(person, out string[] checkBox, out string[] checkedBox);

            var fillInformationCounter = 0;
            while (!quitAddPerson)
            {
                Console.Clear();
                Console.WriteLine("\n\n\n- Add a person to the table by filling in current information:\n" +
                                  "  (NOTE: Vital status = Alive or Deceased)\n");
                Console.WriteLine(checkBox[0] + " Name: " + person.Name + "\n" +
                                  checkBox[1] + " Last name: " + person.LastName + "\n" +
                                  checkBox[2] + " Birthplace: " + person.Birthplace + "\n" +
                                  checkBox[3] + " Country of birth: " + person.CountryOfBirth + "\n" +
                                  checkBox[4] + " Born: " + personBorn + "\n" +
                                  checkBox[5] + " Mother: " + person.Mother + "\n" +
                                  checkBox[6] + " Father: " + person.Father + "\n" +
                                  checkBox[7] + " Vital status: " + person.VitalStatus + "\n\n");

                if (fillInformationCounter == 0)
                {
                    Console.Write("> Name: ");
                    person.Name = Console.ReadLine();
                    if (!int.TryParse(person.Name, out _))
                    {
                        checkBox[0] = checkedBox[0] = "[x]";
                        fillInformationCounter++;
                        Console.Clear();
                    }
                    else
                    {
                        person.Name = "";
                        Console.Clear();
                        Console.WriteLine("\n\n\nOnly text accepted.");
                        Thread.Sleep(1500);
                    }
                }
                else if (fillInformationCounter == 1)
                {
                    Console.Write("> Last name: ");
                    person.LastName = Console.ReadLine();
                    if (!int.TryParse(person.LastName, out _))
                    {
                        checkBox[1] = checkedBox[1] = "[x]";
                        fillInformationCounter++;
                        Console.Clear();
                    }
                    else
                    {
                        person.LastName = "";
                        Console.Clear();
                        Console.WriteLine("\n\n\nOnly text accepted.");
                        Thread.Sleep(1500);
                    }
                }
                else if (fillInformationCounter == 2)
                {
                    Console.Write("> Birthplace: ");
                    person.Birthplace = Console.ReadLine();
                    if (!int.TryParse(person.Birthplace, out _))
                    {
                        checkBox[2] = checkedBox[2] = "[x]";
                        fillInformationCounter++;
                        Console.Clear();
                    }
                    else
                    {
                        person.Birthplace = "";
                        Console.Clear();
                        Console.WriteLine("\n\n\nOnly text accepted.");
                        Thread.Sleep(1500);
                    }
                }
                else if (fillInformationCounter == 3)
                {
                    Console.Write("> Country of birth: ");
                    person.CountryOfBirth = Console.ReadLine();
                    if (!int.TryParse(person.CountryOfBirth, out _))
                    {
                        checkBox[3] = checkedBox[3] = "[x]";
                        fillInformationCounter++;
                        Console.Clear();
                    }
                    else
                    {
                        person.CountryOfBirth = "";
                        Console.Clear();
                        Console.WriteLine("\n\n\nOnly text accepted.");
                        Thread.Sleep(1500);
                    }
                }
                else if (fillInformationCounter == 4)
                {
                    Console.Write("> Born: ");
                    personBorn = Console.ReadLine();
                    if (int.TryParse(personBorn, out _))
                    {
                        checkBox[4] = checkedBox[4] = "[x]";
                        fillInformationCounter++;
                        person.Born = Convert.ToInt32(personBorn);
                        Console.Clear();
                    }
                    else
                    {
                        personBorn = "";
                        Console.Clear();
                        Console.WriteLine("\n\n\nOnly integers accepted.");
                        Thread.Sleep(1500);
                    }
                }
                else if (fillInformationCounter == 5)
                {
                    Console.Write("> Mother: ");
                    person.Mother = Console.ReadLine();
                    if (!int.TryParse(person.Mother, out _))
                    {
                        checkBox[5] = checkedBox[5] = "[x]";
                        fillInformationCounter++;
                        Console.Clear();
                    }
                    else
                    {
                        person.Mother = "";
                        Console.Clear();
                        Console.WriteLine("\n\n\nOnly text accepted.");
                        Thread.Sleep(1500);
                    }
                }
                else if (fillInformationCounter == 6)
                {
                    Console.Write("> Father: ");
                    person.Father = Console.ReadLine();
                    if (!int.TryParse(person.Father, out _))
                    {
                        checkBox[6] = checkedBox[6] = "[x]";
                        fillInformationCounter++;
                        Console.Clear();
                    }
                    else
                    {
                        person.Father = "";
                        Console.Clear();
                        Console.WriteLine("\n\n\nOnly text accepted.");
                        Thread.Sleep(1500);
                    }
                }
                else if (fillInformationCounter == 7)
                {
                    Console.Write("> Vital status: ");
                    person.VitalStatus = Console.ReadLine();
                    if (!int.TryParse(person.VitalStatus, out _))
                    {
                        checkBox[7] = checkedBox[7] = "[x]";
                        fillInformationCounter++;
                        Console.Clear();
                    }
                    else
                    {
                        person.VitalStatus = "";
                        Console.Clear();
                        Console.WriteLine("\n\n\nOnly text accepted.");
                        Thread.Sleep(1500);
                    }

                    Console.WriteLine("\n\n\n- PERSON has been ADDED to the Table: " + SQLDatabase.database.DataTableName + "\n");
                    Console.WriteLine(" Name: " + person.Name + "\n" +
                                      " Last name: " + person.LastName + "\n" +
                                      " Birthplace: " + person.Birthplace + "\n" +
                                      " Country of birth: " + person.CountryOfBirth + "\n" +
                                      " Born: " + person.Born + "\n" +
                                      " Mother: " + person.Mother + "\n" +
                                      " Father: " + person.Father + "\n" +
                                      " Vital status: " + person.VitalStatus);

                    Console.WriteLine("\n\n\n(Press to return...)\n");
                    Console.ReadKey();

                    SQLDatabase.InsertPersonToTable(person.Name, person.LastName, person.Birthplace, person.CountryOfBirth,
                                                    Convert.ToInt32(person.Born), person.Mother, person.Father, person.VitalStatus);
                    UpdateColumnAge(SQLDatabase.database.DataTableName);
                    quitAddPerson = true;
                }
            }
        }

        // CLEAR FORM: resets the form so that it is empty when new information for the next person is filled.
        private static void ClearCreateForm(Person person, out string[] checkBox, out string[] checkedBox)
        {
            checkBox = new string[8];
            checkBox[0] = "[ ]";
            checkBox[1] = "[ ]";
            checkBox[2] = "[ ]";
            checkBox[3] = "[ ]";
            checkBox[4] = "[ ]";
            checkBox[5] = "[ ]";
            checkBox[6] = "[ ]";
            checkBox[7] = "[ ]";

            checkedBox = new string[8];
            checkedBox[0] = "[x]";
            checkedBox[1] = "[x]";
            checkedBox[2] = "[x]";
            checkedBox[3] = "[x]";
            checkedBox[4] = "[x]";
            checkedBox[5] = "[x]";
            checkedBox[6] = "[x]";
            checkedBox[7] = "[x]";

            person.Name = "";
            person.LastName = "";
            person.Birthplace = "";
            person.CountryOfBirth = "";
            personBorn = person.Born.ToString();
            personBorn = "";
            person.Mother = "";
            person.Father = "";
            person.VitalStatus = "";
        }


        /*
        public static void ReadPerson(Person person)
        {
            var idParam = ("@id", person.Id);

            var sql = $"SELECT * FROM People WHERE id=@id";
            DataTable dt = SQLDatabase.GetDataTable(sql, idParam);
            SQLDatabase.ReadDataTable(dt);
            Menu.ContinueAndClear();
        }

        // CRUD --> "R" = Read:
        // Söker efter person i databasen, baserat på personobjektet.
        public Person Read(Person person)
        {
            var databas = new Databas { DatabaseName = DatabaseName };

            var row = databas.GetDataTable(@"SELECT TOP 1 * 
                                             FROM People
                                             WHERE firstName LIKE @id",
                                             ("@id", person.Id.ToString())
                                          );
            if (row.Rows.Count == 0)
            {
                return null;
            }
            return GetPersonObject(row.Rows[0]);
        }*/

        private static Person GetPersonObject(DataRow row)
        {
            return new Person
            {
                Id = (int)row["Id"],
                Name = row["Name"].ToString(),
                LastName = row["Last name"].ToString(),
                Birthplace = row["address"].ToString(),
                CountryOfBirth = row["city"].ToString(),
                Born = (int)row["age"],
                Mother = row["city"].ToString(),
                Father = row["city"].ToString(),
                VitalStatus = row["city"].ToString(),
                Age = row["city"].ToString()
            };
        }
        


        internal static Person Search(Person person)
        {
            Console.Clear();
            Console.WriteLine("\n\n\n- Please specify search option for the database.\n\n");
            Console.WriteLine("[1] Search for a person by Name and Last name.\n" +
                              "[2] Search all. \n");

            int searchChoice = Convert.ToInt32(Console.ReadLine());
            switch (searchChoice)
            {
                case 1:
                    person = SearchByNameLastName(SQLDatabase.database.DataTableName);
                    break;
                case 2:
                    person = SearchAll(SQLDatabase.database.DataTableName);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\n\n\n- There's no such option.");
                    break;
            }
            return person;
        }

        internal static Person SearchByNameLastName(string tableName)
        {
            Console.Write("Enter name: ");
            string searchName = Console.ReadLine();

            CodeServices.codeservices.ClearLastLine();
            CodeServices.codeservices.ClearLastLine();
            Console.Write("\nEnter last name: ");
            string searchLastName = Console.ReadLine();

            DataTable dataTable = SQLDatabase.database.GetDataTable(@$"SELECT * 
                                                                       FROM {tableName} 
                                                                       WHERE Name = '{searchName}' 
                                                                       AND [Last name] = '{searchLastName}'"
                                                                   );
            if (dataTable.Rows.Count == 1)
            {
                person.Id = (int)dataTable.Rows[0]["ID"];
                person.Name = (string)dataTable.Rows[0]["Name"];
                person.LastName = (string)dataTable.Rows[0]["Last name"];
                person.Birthplace = (string)dataTable.Rows[0]["Birthplace"];
                person.CountryOfBirth = (string)dataTable.Rows[0]["Country of birth"];
                person.Born = (int)dataTable.Rows[0]["Born"];
                person.Mother = (string)dataTable.Rows[0]["Mother"];
                person.Father = (string)dataTable.Rows[0]["Father"];
                person.VitalStatus = (string)dataTable.Rows[0]["Vital status"];
                person.Age = (string)dataTable.Rows[0]["Age"];

                return person;
            }
            else if (dataTable.Rows.Count > 1)
            {
                Console.Clear();
                Console.WriteLine("\n\n\nSearch completed! Persons found: " + dataTable.Rows.Count + "\n\n");

                Console.WriteLine("ID   |   Name   |   Last name   |   Birthplace   |   Country of birth   |   Born   |   Mother   |   Father   |   Vital status   |   Age   |");
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    Console.WriteLine(@$"[{i + 1}] {row["ID"]}  {row["Name"]}  {row["Last name"]}  {row["Birthplace"]}  {row["Country of birth"]}    
                                                   {row["Born"]}  {row["Mother"]}  {row["Father"]}  {row["Vital status"]}  {row["Age"]}"
                                     );

                }

                Console.WriteLine("\n\n- Pick a person\n");
                Console.Write("> ");
                var choice = Convert.ToInt32(Console.ReadLine());

                person.Id = (int)dataTable.Rows[choice - 1]["ID"];
                person.Name = (string)dataTable.Rows[choice - 1]["Name"];
                person.LastName = (string)dataTable.Rows[choice - 1]["Last name"];
                person.Birthplace = (string)dataTable.Rows[choice - 1]["Birthplace"];
                person.CountryOfBirth = (string)dataTable.Rows[choice - 1]["Country of birth"];
                person.Born = (int)dataTable.Rows[choice - 1]["Born"];
                person.Mother = (string)dataTable.Rows[choice - 1]["Mother"];
                person.Father = (string)dataTable.Rows[choice - 1]["Father"];
                person.VitalStatus = (string)dataTable.Rows[choice - 1]["Vital status"];
                person.Age = (string)dataTable.Rows[choice - 1]["Age"];

                return person;
            }
            else
            {
                Console.Clear();
                Console.Write("\n\n\nUnfortunatley there is no such person! :(");

                Console.Write("\n\n(Press to return to search...");
                Console.ReadKey();
            }
            return person;
        }

        internal static Person SearchAll(string tableName)
        {
            DataTable dataTable = SQLDatabase.database.GetDataTable(@$"SELECT * 
                                                                       FROM {tableName}"
                                                                       );

            Console.Clear();
            Console.WriteLine("\n\n\nSearch completed! Persons found: " + dataTable.Rows.Count + "\n\n");

            Console.WriteLine("| # | ID |  Name  |  Last name | Birthplace | Country of birth |  Born  |  Mother  |  Father  | Vital status |  Age  |");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                Console.WriteLine(@$"[{i + 1}] {row["ID"]} {row["Name"]}  {row["Last name"]}  {row["Birthplace"]}  {row["Country of birth"]}   {row["Born"]}  {row["Mother"]}  {row["Father"]}  {row["Vital status"]}  {row["Age"]}"
                                 );

            }

            Console.WriteLine("\n\n- Pick a person\n");
            Console.Write("> ");
            var choice = Convert.ToInt32(Console.ReadLine());

            person.Id = (int)dataTable.Rows[choice - 1]["ID"];
            person.Name = (string)dataTable.Rows[choice - 1]["Name"];
            person.LastName = (string)dataTable.Rows[choice - 1]["Last name"];
            person.Birthplace = (string)dataTable.Rows[choice - 1]["Birthplace"];
            person.CountryOfBirth = (string)dataTable.Rows[choice - 1]["Country of birth"];
            person.Born = (int)dataTable.Rows[choice - 1]["Born"];
            person.Mother = (string)dataTable.Rows[choice - 1]["Mother"];
            person.Father = (string)dataTable.Rows[choice - 1]["Father"];
            person.VitalStatus = (string)dataTable.Rows[choice - 1]["Vital status"];
            person.Age = (string)dataTable.Rows[choice - 1]["Age"];

            return person;
        }


        // COLUMN AGE: updates the column age with the persons age.
        internal static void UpdateColumnAge(string tableName)
        {
            SQLDatabase.database.ExecuteSQL(@$"UPDATE {tableName}
                                   SET Age = CASE 
                                             WHEN [Vital status] = 'Deceased'
                                             THEN 'R.I.P'
                                             ELSE CONVERT(varchar(30), 2021 - Born)
                                             END"
                                   );
        }


        public static void Print(Person person)
        {
            if (person != null)
            {
                Console.WriteLine($"{person.Id} {person.Name} {person.LastName} {person.Birthplace} {person.CountryOfBirth} {person.Born} {person.Mother} {person.Father} {person.VitalStatus} {person.Age}");
            }
            else
            {
                Console.WriteLine("Person not found\n\n");
            }
        }
    }
}