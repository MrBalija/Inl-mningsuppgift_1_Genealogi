using System;
using System.Threading;

namespace Inlämningsuppgift_1_Genealogi
{
    class Program
    {
        public static bool quitProgram = false;
        internal static SQLDatabase person = new SQLDatabase ();

        static void Main(string[] args)
        {
            WelcomeIntro(); // WELCOME MSG
            Menu(); //MENU: presents the menu to the user.
        }


        internal static void WelcomeIntro() // WELCOME: welcome the user.
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
        }

        internal static void Menu() //MENU: presents the user with a menu of 7 options.
        {
            while (!quitProgram)
            {
                Console.Title = "Geneanalogy - My Family Tree    |    DATABASE: " + person.DatabaseName;
                Console.Clear();
                Console.WriteLine("*** MENU ***\n");
                Console.WriteLine("1. Add person");
                Console.WriteLine("2. Edit person");
                Console.WriteLine("3. Delete person");
                Console.WriteLine("4. Show parents");
                Console.WriteLine("5. Show siblings");
                Console.WriteLine("6. Search for a person");
                Console.WriteLine("7. Quit\n");
                string userMenuPickString = Console.ReadLine();

                if (int.TryParse(userMenuPickString, out int userMenuPick)) //CHECKS USER INPUT: if user input is an integer, pass.
                {
                    switch (userMenuPick)
                    {
                        case 1:
                            AddPerson(); //CHOICE 1: take user to 'Add person' page - to add a person to the table.
                            break;
                        case 2:
                            EditPerson(); //CHOICE 2: take user to the 'Edit person' page - to edit existing person.
                            break;
                        case 3:
                            DeletePerson(); //CHOICE 3: take user to the 'Delete person' page - to delete existing person.
                            break;
                        case 4:
                            ShowParents(); //CHOICE 4: take user to the 'Show parents' page - lists the parents for a person.
                            break;
                        case 5:
                            ShowSiblings(); //CHOICE 5: take user to the 'Show siblings' page - lists the siblings of a person.
                            break;
                        case 6:
                            SearchPerson(); //CHOICE 6: take user to the 'Search' page - user can search after a person by name, age, year etc.
                            break;
                        case 7: //CHOICE 7: end the program.
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

        internal static void AddPerson()
        {
            throw new NotImplementedException();
        }

        internal static void EditPerson()
        {
            throw new NotImplementedException();
        }

        internal static void DeletePerson()
        {
            throw new NotImplementedException();
        }

        internal static void ShowParents()
        {
            throw new NotImplementedException();
        }

        internal static void ShowSiblings()
        {
            throw new NotImplementedException();
        }

        internal static void SearchPerson()
        {
            throw new NotImplementedException();
        }

    }
}
