using System;
using System.Threading;

namespace Inlämningsuppgift_1_Genealogi
{
    class Program
    {
        public static bool quitProgram = false;

        static void Main(string[] args)
        {
            // WELCOME MSG: welcomes the user.
            Console.Title = "Geneanalogy";
            Console.WriteLine("\n\n\n- Welcome to my Family-Tree!");
            Console.WriteLine("\n\nPress to continue...");
            Console.ReadKey();
            //Thread.Sleep(500);
        
            while (!quitProgram)
            {
                Console.Title = "Geneanalogy - My Family Tree";
                Console.Clear();
                Console.WriteLine("*** MENU ***\n\n");

                Menu(); //MENU: presents the menu to the user.
            }
        }

        public static void Menu() //MENU: presents the user with a menu of 7 options.
        {
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
                    case 1: //USER INPUT: takes user to the members list.
                        AddPerson();
                        break;
                    case 2: //USER INPUT: takes user to the general facts list.
                        EditPerson();
                        break;
                    case 3: //USER INPUT: gives the user the option to remove a member from the list.
                        DeletePerson();
                        break;
                    case 4: //USER INPUT: ends the program.
                        ShowParents();
                        break;
                    case 5: //USER INPUT: ends the program.
                        ShowSiblings();
                        break;
                    case 6: //USER INPUT: ends the program.
                        SearchPerson();
                        break;
                    case 7: //USER INPUT: ends the program.
                        Console.Clear();
                        Console.WriteLine("\n\n\n- Until we meet again...\n\n\n");
                        quitProgram = true;
                        break;
                    default: //USER INPUT: checks if user input is an integer between 1-7.
                        Console.Clear();
                        Console.WriteLine("\n\n\n- There is no such option.");
                        Thread.Sleep(1100);
                        break;
                }
            }
            else //CHECKS USER INPUT: if user input is a string, tell user to use only integers.
            {
                Console.Clear();
                Console.WriteLine("\n\n\n- Only integers accepted.");
                Thread.Sleep(1100);
            }
        }

        private static void AddPerson()
        {
            throw new NotImplementedException();
        }

        private static void EditPerson()
        {
            throw new NotImplementedException();
        }

        private static void DeletePerson()
        {
            throw new NotImplementedException();
        }

        private static void ShowParents()
        {
            throw new NotImplementedException();
        }

        private static void ShowSiblings()
        {
            throw new NotImplementedException();
        }

        private static void SearchPerson()
        {
            throw new NotImplementedException();
        }
    }
}
