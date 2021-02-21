using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;

namespace Inlämningsuppgift_1_Genealogi
{
    class CRUD
    {
        public static bool quitSearch;
        public static bool quitCreatePerson;
        public static bool quitDelete;
        public static string personBorn;
        public static Person person = new Person();


        // CREATE: Creates an objekt Person
        public static void Create(Person person)
        {
            quitCreatePerson = false;

            // CLEAR FORM: resets the form.
            ClearCreate(person, out string[] checkBox, out string[] checkedBox);

            var fillInformationCounter = 0;
            while (!quitCreatePerson)
            {
                Console.Clear();
                Program.PrintMenuChoiceHeader(1);
                Console.WriteLine("\n\n- Add a person to the table by filling in current information:\n" +
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
                    quitCreatePerson = true;
                }
            }
        }

        // CLEAR FORM: resets the form so that it is empty when new information for the next person is filled.
        private static void ClearCreate(Person person, out string[] checkBox, out string[] checkedBox)
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

        // READ: looks for a person in the database and prints out the info.
        public static void Read(Person person)
        {
            Search();
        }

        // READ: looks for a person in the database and prints out the info.
        public static void Update(Person person)
        {
            Search();
            bool quitUpdatePerson = false;
            while (!quitUpdatePerson)
            {
                Print(person);
                Console.WriteLine("\n\nWhat information do you wish to update?\n");

                Console.WriteLine("[1] Name");
                Console.WriteLine("[2] Last name");
                Console.WriteLine("[3] Birthplace");
                Console.WriteLine("[4] Country Of Birth");
                Console.WriteLine("[5] Born");
                Console.WriteLine("[6] Mother");
                Console.WriteLine("[7] Father");
                Console.WriteLine("[8] VitalStatus");

                Console.WriteLine("\n{11} Back to menu");

                Console.Write("\n> ");
                if (int.TryParse(Console.ReadLine(), out int updateChoice))
                {
                    ClearLastLine();
                    ClearLastLine();
                    Console.Write("\n\nChange to: ");
                    switch (updateChoice)
                    {
                        case 1:
                            person.Name = Console.ReadLine();
                            break;
                        case 2:
                            person.LastName = Console.ReadLine();
                            break;
                        case 3:
                            person.Birthplace = Console.ReadLine();
                            break;
                        case 4:
                            person.CountryOfBirth = Console.ReadLine();
                            break;
                        case 5:
                            if (int.TryParse(Console.ReadLine(), out int bornUpdate))
                            {
                                person.Born = bornUpdate;
                            }
                            break;
                        case 6:
                            person.Mother = Console.ReadLine();
                            break;
                        case 7:
                            person.Father = Console.ReadLine();
                            break;
                        case 8:
                            person.VitalStatus = Console.ReadLine();
                            break;
                        case 11:
                            quitUpdatePerson = true;
                            break;
                    }
                }

                if (updateChoice < 11)
                {

                    long rowsAffected = SQLDatabase.database.ExecuteSQL(@"
                            UPDATE My_Family_Tree
                            SET [Name] = '@Name', [Last name] = '@LastName', Birthplace = '@Birthplace', [Country Of Birth] = '@CountryOfBirth', 
                            Born = @Born, Mother = '@Mother', Father = '@Father', [Vital status] = '@VitalStatus'
                            WHERE id = @id;",
                            ("@id", person.Id.ToString()), ("@Name", person.Name), ("@LastName", person.LastName), ("@Birthplace", person.Birthplace),
                            ("@CountryOfBirth", person.CountryOfBirth), ("@Born", person.Born.ToString()), ("@Mother", person.Mother), ("@Father", person.Father),
                            ("@VitalStatus", person.VitalStatus));

                    Console.WriteLine($"\nPerson updated! {rowsAffected} row(s) affected!");
                    Thread.Sleep(1500);
                }
            }
        }

        public static void Delete(Person person)
        {
            quitDelete = false;
            Search();
            while (!quitDelete)
            {
                Program.PrintMenuChoiceHeader(Program.menuChoice);
                Print(person);
                Console.WriteLine("\n\nContinue deleting person? (Yes / No)\n\n");
                Console.Write("> ");

                string deleteChoice = Console.ReadLine().ToUpper();
                if (!int.TryParse(deleteChoice, out _))
                {
                    if (deleteChoice == "YES")
                    {
                        long rowsAffected = SQLDatabase.database.ExecuteSQL(@"DELETE FROM My_Family_Tree
                                                          WHERE id = @id;",
                                                          ("@id", person.Id.ToString()));

                        Console.WriteLine($"\nPerson deleted! {rowsAffected} row(s) affected!");
                        Thread.Sleep(1500);
                        quitDelete = true;
                    }
                    else if (deleteChoice.ToString() == "NO")
                    {
                        Console.Clear();
                        Program.PrintMenuChoiceHeader(Program.menuChoice);
                        Console.WriteLine("\n\n- Deletion canceled!");
                        Thread.Sleep(1500);
                        quitDelete = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\n- Please only text.");
                        Thread.Sleep(1100);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\n- Attention, only text accepted.");
                    Thread.Sleep(1100);
                }
            }
        }

        public static Person Search()
        {
            quitSearch = false;
            while (!quitSearch)
            {
                Console.Clear();
                Program.PrintMenuChoiceHeader(Program.menuChoice);

                Console.WriteLine("\n- Please specify search option for the database.\n");
                Console.WriteLine("[1] Search person by Name and Last name");
                Console.WriteLine("[2] Search person by ID");
                Console.WriteLine("[3] Search ALL\n\n");
                Console.WriteLine("{11}. Back to Menu\n\n");

                Console.Write("> ");
                if (int.TryParse(Console.ReadLine(), out int userChoice))
                {
                    switch (userChoice)
                    {
                        case 1:
                            person = SearchByNameLastName();
                            break;
                        case 2:
                            person = SearchById();
                            break;
                        case 3:
                            person = SearchAll();
                            break;
                        case 11:
                            quitSearch = true;
                            quitDelete = true;
                            Program.quitReadPerson = true;
                            Program.quitUpdatePerson = true;
                            Program.quitDeletePerson = true;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("\n\n\n- There's no such option.");
                            break;
                    }
                }
            }
            return person;
        }

        private static Person SearchByNameLastName()
        {
            Console.Clear();
            Program.PrintMenuChoiceHeader(Program.menuChoice);

            Console.Write("\nEnter name: ");
            string searchName = Console.ReadLine();
            Console.Write("\nEnter last name: ");
            string searchLastName = Console.ReadLine();

            DataTable dataTable = SQLDatabase.database.GetDataTable(@$"SELECT * 
                                                                       FROM {SQLDatabase.database.DataTableName} 
                                                                       WHERE Name = '{searchName}' 
                                                                       AND [Last name] = '{searchLastName}'"
                                                                   );
            if (dataTable.Rows.Count == 1)
            {
                Console.Clear();
                Program.PrintMenuChoiceHeader(Program.menuChoice);
                Console.WriteLine("- Search completed! Persons found: " + dataTable.Rows.Count);

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

                if (Program.menuChoice < 3)
                {
                    Print(person);
                }
                else
                {
                    quitSearch = true;
                }
                return person;
            }
            else if (dataTable.Rows.Count > 1)
            {
                Console.Clear();
                Program.PrintMenuChoiceHeader(Program.menuChoice);
                Console.WriteLine("\nSearch completed! Persons found: " + dataTable.Rows.Count + "\n");

                Console.WriteLine("|#| ID | Name | Last name | Birthplace | Country of birth | Born | Mother | Father | Vital status | Age |");
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    Console.WriteLine(@$"[{i + 1}] {row["ID"]}  {row["Name"]}  {row["Last name"]}  {row["Birthplace"]}  {row["Country of birth"]}  {row["Born"]}  {row["Mother"]}  {row["Father"]}  {row["Vital status"]}  {row["Age"]}"
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

                if (Program.menuChoice < 3)
                {
                    Print(person);
                }
                else
                {
                    quitSearch = true;
                }
                return person;
            }
            else
            {
                Console.Clear();
                Console.Write("\n\n\nUnfortunatley there is no such person! :(");

                Console.Write("\n\n(Press to return...");
                Console.ReadKey();
            }
            return person;
        }

        private static Person SearchById()
        {
            Console.Clear();
            Program.PrintMenuChoiceHeader(Program.menuChoice);

            Console.Write("\nEnter ID: ");
            string searchID = Console.ReadLine();

            DataTable dataTable = SQLDatabase.database.GetDataTable(@$"SELECT * 
                                                                       FROM {SQLDatabase.database.DataTableName} 
                                                                       WHERE Id = '{searchID}'"
                                                                   );
            if (dataTable.Rows.Count == 1)
            {
                Console.Clear();
                Program.PrintMenuChoiceHeader(Program.menuChoice);
                Console.WriteLine("\n\nSearch completed! Persons found: " + dataTable.Rows.Count + "\n\n");

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

                if (Program.menuChoice < 3)
                {
                    Print(person);
                }
                else
                {
                    quitSearch = true;
                }
                return person;
            }
            else
            {
                Console.Clear();
                Console.Write("\n\n\nUnfortunatley there is no such person! :(");

                Console.Write("\n\n(Press to return...");
                Console.ReadKey();
            }
            return person;
        }

        public static Person SearchAll()
        {
            DataTable dataTable = SQLDatabase.database.GetDataTable(@$"SELECT * 
                                                                       FROM {SQLDatabase.database.DataTableName}"
                                                                   );

            Console.Clear();
            Program.PrintMenuChoiceHeader(Program.menuChoice);

            Console.WriteLine("\n\n\nSearch completed! Persons found: " + dataTable.Rows.Count + "\n\n");

            Console.WriteLine("|#|ID| Name | Last name | Birthplace | Country of birth | Born | Mother | Father | Vital status | Age |");
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

            if (Program.menuChoice < 3)
            {
                Print(person);
            }
            else
            {
                quitSearch = true;
            }
            return person;
        }

        public static Person SearchParent()
        {
            DataTable dataTable = SQLDatabase.database.GetDataTable(@$"SELECT Name, [Last name] 
                                                                       FROM {SQLDatabase.database.DataTableName}
                                                                       WHERE Mother = '@mother' OR Father = '@father'"
                                                                    );

            Console.Clear();
            Program.PrintMenuChoiceHeader(Program.menuChoice);

            Console.WriteLine("\n\n\nSearch completed! Persons found: " + dataTable.Rows.Count + "\n\n");

            Console.WriteLine("|#|ID| Name | Last name | Birthplace | Country of birth | Born | Mother | Father | Vital status | Age |");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                Console.WriteLine(@$"[{i + 1}] {row["Name"]}  {row["Last name"]}"
                                 );
            }

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
            Console.Clear();
            Program.PrintMenuChoiceHeader(Program.menuChoice);
            Console.WriteLine("\n\n|#| ID | Name | Last name | Birthplace | Country of birth | Born | Mother | Father | Vital status | Age |");
            Console.WriteLine($"{person.Id} {person.Name} {person.LastName} {person.Birthplace} {person.CountryOfBirth} {person.Born} {person.Mother} {person.Father} {person.VitalStatus} {person.Age}");

            if (Program.menuChoice < 3)
            {
                Console.WriteLine("\n\nPress to return...");
                Console.ReadKey();
            }
        }

        public static void PrintChildren(Person person)
        {
            Console.Clear();
            Program.PrintMenuChoiceHeader(Program.menuChoice);
            Console.WriteLine("\n\n|#| ID | Name | Last name | Birthplace | Country of birth | Born | Mother | Father | Vital status | Age |");
            Console.WriteLine($"{person.Name} {person.LastName}");

            if (Program.menuChoice < 3)
            {
                Console.WriteLine("\n\nPress to return...");
                Console.ReadKey();
            }
        }

        public static void ClearLastLine() //Clears last line.
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

    }

}