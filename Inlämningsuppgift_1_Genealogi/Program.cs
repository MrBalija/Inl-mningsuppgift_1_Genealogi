using System;
using System.Threading;

namespace Inlämningsuppgift_1_Genealogi
{
    class Program
    {
        public static bool quitProgram = false;
        public static bool quitReadPerson;
        public static bool quitUpdatePerson;
        public static bool quitDeletePerson;
        public static int menuChoice;
        static void Main(string[] args)
        {
            /*
            var test = SQLDatabase.database.DoesTableExist("Bug");
            Console.WriteLine(test);
            Console.ReadKey();
            */
            // DATABASE: creates a database if it doesn't exist.
            SQLDatabase.CreateDatabase("Family_Database");

            // TABLE: creates table with .
            SQLDatabase.CreateTable(SQLDatabase.database.DataTableName);

            // WELCOMES the user.
            //WelcomeIntro();

            //MENU: presents the menu to the user.
            Menu();
        }

        internal static void WelcomeIntro() // WELCOME: welcome the user.
        {
            Console.Title = "";
            Console.WriteLine("\n\n\n- Welcome to My Family-Tree!");
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

        internal static void Menu() //MENU: presents the user with a menu of 7 options.
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
                Console.WriteLine("[9] Show siblings for an individual");
                Console.WriteLine("[10] Show ALL members of my Family Tree\n");
                Console.WriteLine("{11}. QUIT program\n\n");

                Console.Write("> ");
                if (int.TryParse(Console.ReadLine(), out menuChoice)) //CHECKS USER INPUT: if user input is an integer, pass.
                {
                    switch (menuChoice)
                    {
                        case 1:
                            CRUD.Create(CRUD.person);
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
                            ShowSiblings();
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
             CRUD.Update(CRUD.person);
        }

        internal static void DeletePerson()
        {
            throw new NotImplementedException();
        }


        private static void ListAllAfterBirthplace()
        {
            throw new NotImplementedException();
        }

        private static void ListAllAfterYearBorn()
        {
            throw new NotImplementedException();
        }

        private static void ListAllAfterLetter()
        {
            throw new NotImplementedException();
        }

        internal static void ShowGrandparents()
        {
            throw new NotImplementedException();
        }

        internal static void ShowSiblings()
        {
            throw new NotImplementedException();
        }

        private static void ShowAllMembers()
        {
            throw new NotImplementedException();
        }

        internal static void SearchPerson()
        {
            bool quitSearch = false;

            while (!quitSearch)
            {
                CRUD.Search(CRUD.person);
            }
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
                default:
                    // DO nothing :).
                    break;
            }
        }

    }
}
