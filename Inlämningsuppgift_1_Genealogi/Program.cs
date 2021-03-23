using System;
using System.Data;
using System.Threading;

namespace Inlämningsuppgift_1_Genealogi
{
    class Program
    {
        public static bool quitProgram = false;
        public static bool quitReadPerson;
        public static bool quitUpdatePerson;
        public static bool quitDeletePerson;
        public static bool quitShowGrandparents;
        public static bool quitShowChildren;
        public static int menuChoice;


        static void Main(string[] args)
        {
            // DATABASE: creates a database if it doesn't exist.
            SQLDatabase.CreateDatabase(SQLDatabase.database.DatabaseName); //"Family_Tree");

            // TABLE: creates table with .
            SQLDatabase.CreateTable(SQLDatabase.database.DataTableName);

            // WELCOMES the user.
            //WelcomeIntro();

            //MENU: presents the menu to the user.
            Menu();
        }

        private static void WelcomeIntro() // WELCOME: welcome the user.
        {
            Console.Title = "";
            Console.WriteLine("\n\n\n- Welcome to my Family-Tree!");
            Console.WriteLine("\n\nPress to continue...");
            Console.ReadKey();
            Console.Clear();

            Console.Write("\n\nConnecting to database");
            Thread.Sleep(800);
            Console.Write(".");
            Thread.Sleep(800);
            Console.Write(".");
            Thread.Sleep(800);
            Console.Write(".");
            Thread.Sleep(800);
            Console.Write(".");
            Thread.Sleep(800);
            Console.Clear();

            Console.Title = "Connected to: " + SQLDatabase.database.DatabaseName;
            Console.Write("\n\nConnection established!");
            Thread.Sleep(1500);
        }

        private static void Menu() //MENU: presents the user with a menu of 7 options.
        {
            while (!quitProgram)
            {
                Console.Title = "Geneanalogy    |    Database: " + SQLDatabase.database.DatabaseName + "    |    Table: " + SQLDatabase.database.DataTableName;
                Console.Clear();
                Console.WriteLine("-------------------");
                Console.WriteLine("      M E N U     ");
                Console.WriteLine("-------------------\n");
                Console.WriteLine("- EDITING");
                Console.WriteLine("[1] Create person");
                Console.WriteLine("[2] Read person");
                Console.WriteLine("[3] Update person");
                Console.WriteLine("[4] Delete person\n");

                Console.WriteLine("- SHOW LISTS");
                Console.WriteLine("[5] List ALL relatives after 'Birthplace'");
                Console.WriteLine("[6] List ALL relatives after year 'Born'");
                Console.WriteLine("[7] List ALL relatives that starts with a certain letter");
                Console.WriteLine("[8] Show grandparents for an individual");
                Console.WriteLine("[9] Show children for a parent");
                Console.WriteLine("[10] Show ALL members of my Family Tree\n\n");
                Console.WriteLine("{11} QUIT program\n\n");

                Console.Write("> ");
                if (int.TryParse(Console.ReadLine(), out menuChoice)) //CHECKS USER INPUT: if user input is an integer, pass.
                {
                    switch (menuChoice)
                    {
                        case 1:
                            CreatePerson();                            
                            break;
                        case 2:
                            ReadPerson();
                            break;
                        case 3:
                            UpdatePerson();
                            break;
                        case 4:
                            DeletePerson();
                            break;
                        case 5:
                            ListAllAfterBirthplace();
                            break;
                        case 6:
                            ListAllAfterYearBorn();
                            break;
                        case 7:
                            ListAllAfterLetter();
                            break;
                        case 8:
                            ShowGrandparents();
                            break;
                        case 9:
                            ShowChildren();
                            break;
                        case 10:
                            ShowAllMembers();
                            break;
                        case 11: //CHOICE 7: end the program.
                            Console.Clear();
                            Console.WriteLine("\n\n\n- Until we meet again...\n\n\n");
                            quitProgram = true;
                            break;
                        default: //CHECKS USER INPUT: if user input isn't an integer between 1-7, tell user "no such option".
                            Console.Clear();
                            Console.WriteLine("\n\n\n- There is no such option.");
                            Thread.Sleep(1100);
                            break;
                    }
                }
                else //CHECKS USER INPUT: if user input is a string, tell user "only integers accepted".
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\n- Only integers accepted.");
                    Thread.Sleep(1100);
                }
            }
        }

        private static void CreatePerson()
        {
            CRUD.Create(CRUD.person);
        }
        
        private static void ReadPerson()
        {
            quitReadPerson = false;
            while (!quitReadPerson)
            {
                CRUD.Read(CRUD.person);
            }
        }
       
        private static void UpdatePerson()
        {
            quitUpdatePerson = false;
            while (!quitUpdatePerson)
            {
                CRUD.Update();
            }
        }

        private static void DeletePerson()
        {
            quitDeletePerson = false;
            while (!quitDeletePerson)
            {
                CRUD.Delete();
            }
        }

        private static void ListAllAfterBirthplace()
        {
            Console.Clear();
            PrintMenuChoiceHeader(menuChoice);

            DataTable dataTable = SQLDatabase.database.GetDataTable(@"SELECT *
                                                                      FROM My_Family_Tree
                                                                      ORDER BY Birthplace;"
                                                                   );
            PrintTableInformation(dataTable);
        }

        private static void ListAllAfterYearBorn()
        {
            Console.Clear();
            PrintMenuChoiceHeader(menuChoice);

            DataTable dataTable = SQLDatabase.database.GetDataTable(@"SELECT *
                                                                      FROM My_Family_Tree
                                                                      ORDER BY Born;"
                                                                   );
            PrintTableInformation(dataTable);
        }

        private static void ListAllAfterLetter()
        {
            var quitListAllAfterLetter = false;
            while (!quitListAllAfterLetter)
            {
                Console.Clear();
                PrintMenuChoiceHeader(menuChoice);

                Console.WriteLine("\n\n- Enter a starting letter of the name of the person to search for:");
                Console.WriteLine("  (Write 'Quit' to exit)\n\n");
                Console.Write("> ");
                string userStartingLetter = Console.ReadLine();

                string quitToMenu = userStartingLetter.ToUpper(); // Stores user input to check if the user enters "QUIT".

                userStartingLetter += "%";
                var startingLetterParam = ("@startingLetter", userStartingLetter);
                var sql = (@"SELECT *
                             FROM My_Family_Tree
                             WHERE Name LIKE @startingLetter;"
                          );

                DataTable dataTable = SQLDatabase.database.GetDataTable(sql, startingLetterParam);

                if (!int.TryParse(quitToMenu, out _))
                {
                    if (quitToMenu == "QUIT")
                    {
                        quitListAllAfterLetter = true;
                    }
                    else if (dataTable.Rows.Count > 0)
                    {
                        PrintTableInformation(dataTable);
                    }
                    else if (dataTable.Rows.Count < 1)
                    {
                        Console.Write("\n\n\nUnfortunatley there is no such person! :(");
                        Console.Write("\n\n(Press to return...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.Write("\n\n\n- Attention, only letters accepted.");
                    Thread.Sleep(1100);
                }
            }
        }

        private static void ShowGrandparents()
        {
            quitShowGrandparents = false;
            while (!quitShowGrandparents)
            {
                CRUD.SearchGrandparents();
            }
        }

        private static void ShowChildren()
        {
            quitShowChildren = false;
            while (!quitShowChildren)
            {
                CRUD.SearchChildren();
            }
        }

        private static void ShowAllMembers()
        {
            Console.Clear();
            PrintMenuChoiceHeader(menuChoice);

            DataTable dataTable = SQLDatabase.database.GetDataTable(@"SELECT *
                                                                      FROM My_Family_Tree;"
                                                                   );
            PrintTableInformation(dataTable);
        }
        
        public static void PrintMenuChoiceHeader(int menuChoice)
        {
            switch (menuChoice)
            {
                case 1:
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("   - Create: lets you create and add a new person to the table. ");
                    Console.WriteLine("----------------------------------------------------------------");
                    break;
                case 2:
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("   - READ: fetches and shows full information of an person.     ");
                    Console.WriteLine("----------------------------------------------------------------");
                    break;
                case 3:
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("   - UPDATE: updates information of an existing person.         ");
                    Console.WriteLine("----------------------------------------------------------------");
                    break;
                case 4:
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("   - DELETE: removes a person indefinately from the table.      ");
                    Console.WriteLine("----------------------------------------------------------------");
                    break;
                case 5:
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("   - LISTS ALL relatives in the table by Birthplace.            ");
                    Console.WriteLine("----------------------------------------------------------------");
                    break;
                case 6:
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("   - LISTS ALL relatives in the table after year born.          ");
                    Console.WriteLine("----------------------------------------------------------------");
                    break;
                case 7:
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("   - LISTS ALL relatives starting with a certain letter.        ");
                    Console.WriteLine("----------------------------------------------------------------");
                    break;
                case 8:
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("   - Show grandparents for a person.                            ");
                    Console.WriteLine("----------------------------------------------------------------");
                    break;
                case 9:
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("   - Show children for a parent.                                ");
                    Console.WriteLine("----------------------------------------------------------------");
                    break;
                case 10:
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("   - Show ALL members of my Family Tree.                        ");
                    Console.WriteLine("----------------------------------------------------------------");
                    break;
                default:
                    // DO nothing :).
                    break;
            }
        }

        private static void PrintTableInformation(DataTable dataTable)
        {
            Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(String.Format("| {0,-4} | {1,-2} | {2,-10} | {3,-10} | {4,-10} | {5,-16} | {6,-4} | {7,-15} | {8,-14} | {9,-12} | {10,5} |",
                "#", "ID", "Name", "Last name", "Birthplace", "Country of birth", "Born", "Mother", "Father", "Vital status", "Age"));
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------");

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                Console.WriteLine(String.Format("| {0,-4} | {1,-2} | {2,-10} | {3,-10} | {4,-10} | {5,-16} | {6,-4} | {7,-15} | {8,-14} | {9,-12} | {10,5} |",
                    @$"[{i + 1}]", @$"{row["ID"]}", @$"{row["Name"]}", @$"{row["Last name"]}", @$"{row["Birthplace"]}", @$"{row["Country of birth"]}", @$"{row["Born"]}",
                    @$"{row["Mother"]}", @$"{row["Father"]}", @$"{row["Vital status"]}", @$"{row["Age"]}"));
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("\n\nPress any to go back...");
            Console.ReadKey();
        }
    }
}
