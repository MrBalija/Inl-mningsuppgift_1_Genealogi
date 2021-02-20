using System;
using System.Threading;

namespace Inlämningsuppgift_1_Genealogi
{
    class Program
    {
        public static bool quitProgram = false;


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
            SQLDatabase.CreateTable(SQLDatabase.DataTableName);

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
                Console.Title = "Geneanalogy    |    Database: " + SQLDatabase.database.DatabaseName + "    |    Table: " + SQLDatabase.database.DatabaseName;
                Console.Clear();
                Console.WriteLine("*** MENU ***\n");
                Console.WriteLine("1. Add person");
                Console.WriteLine("2. Edit person");
                Console.WriteLine("3. Delete person");
                Console.WriteLine("4. Show parents");
                Console.WriteLine("5. Show siblings");
                Console.WriteLine("6. Search");
                Console.WriteLine("7. Quit\n");

                Console.Write("> ");
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
            bool quitAddPerson = false;
            var fillInformationCounter = 0;
            Person addPerson = new Person();

            // CLEAR FORM: resets the form.
            ClearForm(addPerson, out string[] checkBox, out string[] checkedBox);

            while (!quitAddPerson)
            {
                Console.Clear();
                Console.WriteLine("\n\n\n- Add a person to the table by filling in current information:\n");
                Console.WriteLine(checkBox[0] + " Name: " + addPerson.Name + "\n" +
                                  checkBox[1] + " Last name: " + addPerson.LastName + "\n" +
                                  checkBox[2] + " Birthplace: " + addPerson.Birthplace + "\n" +
                                  checkBox[3] + " Country of birth: " + addPerson.CountryOfBirth + "\n" +
                                  checkBox[4] + " Born: " + addPerson.Born + "\n" +
                                  checkBox[5] + " Mother: " + addPerson.Mother + "\n" +
                                  checkBox[6] + " Father: " + addPerson.Father + "\n" +
                                  checkBox[7] + " Vital status: " + addPerson.VitalStatus + "\n\n");

                if (fillInformationCounter == 0)
                {
                    Console.Write("> Name: ");
                    addPerson.Name = Console.ReadLine();
                    checkBox[0] = checkedBox[0] = "[x]";
                    fillInformationCounter++;
                    Console.Clear();
                }
                else if (fillInformationCounter == 1)
                {
                    Console.Write("> Last name: ");
                    addPerson.LastName = Console.ReadLine();
                    checkBox[1] = checkedBox[1] = "[x]";
                    fillInformationCounter++;
                    Console.Clear();
                }
                else if (fillInformationCounter == 2)
                {
                    Console.Write("> Birthplace: ");
                    addPerson.Birthplace = Console.ReadLine();
                    checkBox[2] = checkedBox[2] = "[x]";
                    fillInformationCounter++;
                    Console.Clear();
                }
                else if (fillInformationCounter == 3)
                {
                    Console.Write("> Country of birth: ");
                    addPerson.CountryOfBirth = Console.ReadLine();
                    checkBox[3] = checkedBox[3] = "[x]";
                    fillInformationCounter++;
                    Console.Clear();
                }
                else if (fillInformationCounter == 4)
                {
                    Console.Write("> Born: ");
                    addPerson.Born = Console.ReadLine();
                    checkBox[4] = checkedBox[4] = "[x]";
                    fillInformationCounter++;
                    Console.Clear();
                }
                else if (fillInformationCounter == 5)
                {
                    Console.Write("> Mother: ");
                    addPerson.Mother = Console.ReadLine();
                    checkBox[5] = checkedBox[5] = "[x]";
                    fillInformationCounter++;
                    Console.Clear();
                }
                else if (fillInformationCounter == 6)
                {
                    Console.Write("> Father: ");
                    addPerson.Father = Console.ReadLine();
                    checkBox[6] = checkedBox[6] = "[x]";
                    fillInformationCounter++;
                    Console.Clear();
                }
                else if (fillInformationCounter == 7)
                {
                    Console.Write("> Vital status: ");
                    addPerson.VitalStatus = Console.ReadLine();
                    checkBox[7] = checkedBox[7] = "[x]";
                    fillInformationCounter++;
                    Console.Clear();

                    Console.WriteLine("\n\n\n- Person added to the Table: " + SQLDatabase.DataTableName + "\n");
                    Console.WriteLine(checkBox[0] + " Name: " + addPerson.Name + "\n" +
                                      checkBox[1] + " Last name: " + addPerson.LastName + "\n" +
                                      checkBox[2] + " Birthplace: " + addPerson.Birthplace + "\n" +
                                      checkBox[3] + " Country of birth: " + addPerson.CountryOfBirth + "\n" +
                                      checkBox[4] + " Born: " + addPerson.Born + "\n" +
                                      checkBox[5] + " Mother: " + addPerson.Mother + "\n" +
                                      checkBox[6] + " Father: " + addPerson.Father + "\n" +
                                      checkBox[7] + " Vital status: " + addPerson.VitalStatus);

                    Console.WriteLine("\n\n\n- (Press to return...)\n");
                    Console.ReadKey();
                    
                    SQLDatabase.InsertPersonToTable(addPerson.Name, addPerson.LastName, addPerson.Birthplace, addPerson.CountryOfBirth,
                                                    Convert.ToInt32(addPerson.Born), addPerson.Mother, addPerson.Father, addPerson.VitalStatus);
                    quitAddPerson = true;
                }
            }

        }

        internal static void ClearForm(Person addPerson, out string[] checkBox, out string[] checkedBox)
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

            addPerson.Name = "";
            addPerson.LastName = "";
            addPerson.Birthplace = "";
            addPerson.CountryOfBirth = "";
            addPerson.Born = "";
            addPerson.Mother = "";
            addPerson.Father = "";
            addPerson.VitalStatus = "";
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
