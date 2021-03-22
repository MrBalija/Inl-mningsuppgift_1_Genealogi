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
        public static bool quitCreate;
        public static bool quitUpdate;
        public static bool quitDelete;
        public static string personBorn;
        public static Person person = new Person();
        public static string searchLastName;

        // CREATE: Creates an objekt Person
        public static void Create(Person person)
        {
            quitCreate = false;

            // CLEAR FORM: resets the form.
            ClearCreate(person, out string[] checkBox, out string[] checkedBox);

            var fillInformationCounter = 0;
            while (!quitCreate)
            {
                Console.Clear();
                Program.PrintMenuChoiceHeader(Program.menuChoice);
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
                    quitCreate = true;
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

        // READ: looks for a person in the table and prints out the information.
        public static void Read(Person person)
        {
            Search();
        }

        // UPDATE: looks for a person in the table and presents the user with the option to update any information.
        public static void Update()
        {
            quitUpdate = false;
            Search();
            while (!quitUpdate)
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

                Console.WriteLine("\n{11} Go back");

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
                            quitUpdate = true;
                            break;
                    }
                }

                if (updateChoice < 11)
                {
                    /* Updates information with new changes made to the person. Unchanged information is updated with "default" info = first information
                       added to the person when table was created or if changes has been made previously, it is updated again with those changes.  */
                    var personIdParam = ("@id", person.Id.ToString());
                    var personNameParam = ("@Name", person.Name.ToString());
                    var personLastNameParam = ("@LastName", person.LastName.ToString());
                    var personBirthplaceParam = ("@Birthplace", person.Birthplace.ToString());
                    var personCountryOfBirthParam = ("@CountryOfBirth", person.CountryOfBirth.ToString());
                    var personBornParam = ("@Born", person.Born.ToString());
                    var personMotherParam = ("@Mother", person.Mother.ToString());
                    var personFatherParam = ("@Father", person.Father.ToString());
                    var personVitalStatusParam = ("@VitalStatus", person.VitalStatus.ToString());

                    var sqlUpdate = @"UPDATE My_Family_Tree
                                      SET [Name] = @Name, [Last name] = @LastName, Birthplace = @Birthplace, [Country Of Birth] = @CountryOfBirth, 
                                      Born = @Born, Mother = @Mother, Father = @Father, [Vital status] = @VitalStatus
                                      WHERE id = @id;";

                    long rowsAffected = SQLDatabase.database.ExecuteSQL(sqlUpdate, personNameParam, personLastNameParam, personBirthplaceParam,
                        personCountryOfBirthParam, personBornParam, personMotherParam, personFatherParam, personVitalStatusParam, personIdParam);

                    Console.WriteLine($"\nPerson updated! {rowsAffected} row(s) affected!");
                    Thread.Sleep(1000);
                }
            }
        }

        // DELETE: looks for a person in the table and presents the user with the option to delete a person from the table.
        public static void Delete()
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
                        var personIdParam = ("@id", person.Id.ToString());

                        var sqlDelete = @"DELETE FROM My_Family_Tree
                                          WHERE id = @id;";

                        long rowsAffected = SQLDatabase.database.ExecuteSQL(sqlDelete, personIdParam);

                        Console.WriteLine($"\nPerson deleted! {rowsAffected} row(s) affected!");
                        Thread.Sleep(1500);
                        quitDelete = true;
                    }
                    else if (deleteChoice == "NO")
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

        // SEARCH: presents the user with the option to search for a person either by name & last, id or list all people in the table.
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
                            quitUpdate = true;
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

        // SEARCH by Name and Last name: user can search for a person by name and last name.
        private static Person SearchByNameLastName()
        {
            Console.Clear();
            Program.PrintMenuChoiceHeader(Program.menuChoice);

            Console.Write("\n\nEnter name: ");
            string searchName = Console.ReadLine();

            Console.Write("Enter last name: ");
            string searchLastName = Console.ReadLine();


            var personNameParam = ("@name", searchName.ToString());

            var personLastNameParam = ("@lastName", searchLastName.ToString());

            var sqlSearchByNameLastName = @$"SELECT * 
                                             FROM My_Family_Tree 
                                             WHERE Name = @name 
                                             AND [Last name] = @lastName;";

            DataTable dataTable = SQLDatabase.database.GetDataTable(sqlSearchByNameLastName, personNameParam, personLastNameParam);

            if (dataTable.Rows.Count == 1)  // If only one person is found, print information of that person.
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
            else if (dataTable.Rows.Count > 1) // If more than one person is found, print persons and their information and give user the option to pick a person.
            {
                Console.Clear();
                Program.PrintMenuChoiceHeader(Program.menuChoice);
                Console.WriteLine("\n\nSearch completed! Persons found: " + dataTable.Rows.Count + "\n\n");

                Console.WriteLine("|#| ID | Name | Last name | Birthplace | Country of birth | Born | Mother | Father | Vital status | Age |");
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    Console.WriteLine(@$"[{i + 1}] {row["ID"]}  {row["Name"]}  {row["Last name"]}  {row["Birthplace"]}  {row["Country of birth"]}  {row["Born"]}  {row["Mother"]}  {row["Father"]}  {row["Vital status"]}  {row["Age"]}");
                }

                Console.WriteLine("\n\n- Pick a person.\n");
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

                Console.Write("\n\n(Press to return...)");
                Console.ReadKey();
            }
            return person;
        }

        // SEARCH by Id: user can search for a person by ID.
        private static Person SearchById()
        {
            Console.Clear();
            Program.PrintMenuChoiceHeader(Program.menuChoice);

            Console.Write("\nEnter ID: ");
            string searchID = Console.ReadLine();

            var personIdParam = ("@id", searchID.ToString());

            var sqlSearchById = @$"SELECT * 
                                   FROM My_Family_Tree 
                                   WHERE Id = @id;";

            DataTable dataTable = SQLDatabase.database.GetDataTable(sqlSearchById, personIdParam);

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

        // SEARCH All: presents the user with a list of all people in the table.
        public static Person SearchAll()
        {
            DataTable dataTable = SQLDatabase.database.GetDataTable(@$"SELECT * 
                                                                       FROM My_Family_Tree"
                                                                   );

            Console.Clear();
            Program.PrintMenuChoiceHeader(Program.menuChoice);
            Console.WriteLine("\n\nSearch completed! Persons found: " + dataTable.Rows.Count + "\n\n");

            Console.WriteLine("|#|ID| Name | Last name | Birthplace | Country of birth | Born | Mother | Father | Vital status | Age |");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                Console.WriteLine(@$"[{i + 1}] {row["ID"]} {row["Name"]}  {row["Last name"]}  {row["Birthplace"]}  {row["Country of birth"]}   {row["Born"]}  {row["Mother"]}  {row["Father"]}  {row["Vital status"]}  {row["Age"]}");
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

        // SEARCH Grandparents: user are presented with the grandparents of a person of their search choice in the table.
        public static Person SearchGrandparents()
        {
            Console.Clear();
            Program.PrintMenuChoiceHeader(Program.menuChoice);

            Console.WriteLine("\n\n- Enter a person to display their grandparents.");
            Console.WriteLine("  (Enter 'QUIT' to exit)");

            Console.Write("\n\nEnter name: ");
            string searchName = Console.ReadLine().ToUpper();

            if (searchName != "QUIT")
            {
                Console.Write("Enter last name: ");
                searchLastName = Console.ReadLine().ToUpper();
            }

            if (searchName == "QUIT" || searchLastName == "QUIT")
            {
                Program.quitShowGrandparents = true;
            }
            else
            {
                var personNameParam = ("@name", searchName.ToString());

                var personLastNameParam = ("@lastName", searchLastName.ToString());

                var sqlSearchGrandparents = @$"SELECT ID, Mother, Father 
                                               FROM My_Family_Tree 
                                               WHERE Name = @name 
                                               AND [Last name] = @lastName;";

                DataTable dtMotherAndFather = SQLDatabase.database.GetDataTable(sqlSearchGrandparents, personNameParam, personLastNameParam);

                if (dtMotherAndFather.Rows.Count == 0)
                {
                    Console.Clear();
                    Console.Write("\n\n\nUnfortunatley there is no such person! :(");

                    Console.Write("\n\n(Press to return...)");
                    Console.ReadKey();
                }
                else 
                {
                    var personId = (int)dtMotherAndFather.Rows[0]["ID"];
                    if (personId > 22) // CHECKS FOR GRANDPARENTS: if the person entered has an ID higher than 22, then no grandparents exist for that person.
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\nUnfortunatley the person entered does not have grandparents listed! :(");

                        Console.Write("\n\n(Press to return...)");
                        Console.ReadKey();
                    }
                    else
                    {
                        // Full name of the Mother and Father of the person from data table are stored in a variable - later to be use to fetch grandparents.
                        var storeFullNameMother = (string)dtMotherAndFather.Rows[0]["Mother"];
                        var storeFullNameFather = (string)dtMotherAndFather.Rows[0]["Father"];

                        /* Since the Mother & Father column holds full name (name and last name), we need to split it into two strings: name and last name.
                           This so that we can make a new search using the name and last name separately.  */
                        string[] motherArray = storeFullNameMother.Split(" ");
                        string[] fatherArray = storeFullNameFather.Split(" ");
                        var motherNameParam = ("@motherName", motherArray[0].ToString());  // Stores the name of the persons mother as a param.
                        var motherLastNameParam = ("@motherLastName", motherArray[1].ToString());  // Stores the last name of the persons mother as a param.
                        var fatherNameParam = ("@fatherName", fatherArray[0].ToString());  // Stores the name of the persons father as a param.
                        var fatherLastNameParam = ("@fatherLastName", fatherArray[1].ToString());  // Stores the last name of the persons father as a param.

                        // MOTHER-side: fetch grandmother and grandfather:
                        var sqlSearchGrandparentsMotherSide = @$"SELECT ID, Mother, Father 
                                                                 FROM My_Family_Tree 
                                                                 WHERE Name = @motherName 
                                                                 AND [Last name] = @motherLastName;";

                        DataTable dtGrandparentsMotherSide = SQLDatabase.database.GetDataTable(sqlSearchGrandparentsMotherSide, motherNameParam, motherLastNameParam);
                        var fullNameGrandmotherMotherSide = (string)dtGrandparentsMotherSide.Rows[0]["Mother"];
                        var fullNameGrandfatherMotherSide = (string)dtGrandparentsMotherSide.Rows[0]["Father"];


                        // FATHER-side: fetch grandmother and grandfather:
                        var sqlSearchGrandparentsFatherSide = @$"SELECT ID, Mother, Father 
                                                                 FROM My_Family_Tree 
                                                                 WHERE Name = @fatherName 
                                                                 AND [Last name] = @fatherLastName;";

                        DataTable dtGrandparentsFathersSide = SQLDatabase.database.GetDataTable(sqlSearchGrandparentsFatherSide, fatherNameParam, fatherLastNameParam);
                        var fullNameGrandmotherFatherSide = (string)dtGrandparentsFathersSide.Rows[0]["Mother"];
                        var fullNameGrandfatherFatherSide = (string)dtGrandparentsFathersSide.Rows[0]["Father"];


                        Console.Clear();
                        Program.PrintMenuChoiceHeader(Program.menuChoice);

                        Console.WriteLine("\n\n(MOTHER-side) Grandparents:");
                        Console.WriteLine("Grandmother: " + fullNameGrandmotherMotherSide);
                        Console.WriteLine("Grandfather: " + fullNameGrandfatherMotherSide);

                        Console.WriteLine("\n\n(FATHER-side) Grandparents:");
                        Console.WriteLine("Grandmother: " + fullNameGrandmotherFatherSide);
                        Console.WriteLine("Grandfather: " + fullNameGrandfatherFatherSide);

                        Console.Write("\n\n(Press to return...)");
                        Console.ReadKey();
                    } 
                }
            }
            return person;
        }

        // SEARCH Children: user are presented with children of a parent of their search choice in the table.
        public static Person SearchChildren()
        {
            Console.Clear();
            Program.PrintMenuChoiceHeader(Program.menuChoice);

            Console.WriteLine("\n\n- Enter a parent to display their children.");
            Console.WriteLine("  (Enter 'QUIT' to exit)");

            Console.Write("\nParent full name: ");
            string searchParent = Console.ReadLine().ToUpper();

            if (searchParent == "QUIT")
            {
                Program.quitShowChildren = true;
            }
            else
            {
                var searchParentParam = ("@parent", searchParent.ToString());

                var sqlSearchChildren = @$"SELECT ID, Name, [Last name], Mother, Father 
                                           FROM My_Family_Tree 
                                           WHERE Mother = @parent 
                                           OR Father = @parent;";

                DataTable dataTable = SQLDatabase.database.GetDataTable(sqlSearchChildren, searchParentParam);

                if (dataTable.Rows.Count == 1)
                {
                    Console.Clear();
                    Program.PrintMenuChoiceHeader(Program.menuChoice);
                    Console.WriteLine("\n\nSearch completed! Persons found: " + dataTable.Rows.Count + "\n\n");

                    person.Id = (int)dataTable.Rows[0]["ID"];
                    person.Name = (string)dataTable.Rows[0]["Name"];
                    person.LastName = (string)dataTable.Rows[0]["Last name"];
                    person.Mother = (string)dataTable.Rows[0]["Mother"];
                    person.Father = (string)dataTable.Rows[0]["Father"];

                    PrintChildren(person);
                }
                else if (dataTable.Rows.Count > 1)
                {
                    Console.Clear();
                    Program.PrintMenuChoiceHeader(Program.menuChoice);
                    Console.WriteLine("\n\nSearch completed! Persons found: " + dataTable.Rows.Count + "\n\n");

                    Console.WriteLine("|#|ID| Name | Last name | Mother | Father |");
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        DataRow row = dataTable.Rows[i];
                        Console.WriteLine(@$"[{i + 1}] {row["ID"]}  {row["Name"]}  {row["Last name"]}  {row["Mother"]}  {row["Father"]}"
                                         );
                    }

                    Console.WriteLine("\n\n- Pick a child for further details.\n");
                    Console.Write("> ");
                    var choice = Convert.ToInt32(Console.ReadLine());

                    var personId = (int)dataTable.Rows[choice - 1]["ID"];
                    
                    var personIdParam = ("@id", personId.ToString());

                    var sqlShowChild = @$"SELECT * 
                                          FROM My_Family_Tree
                                          WHERE ID = @id;";

                    DataTable dataTableShowChild = SQLDatabase.database.GetDataTable(sqlShowChild, personIdParam);

                    person.Id = (int)dataTableShowChild.Rows[0]["ID"];
                    person.Name = (string)dataTableShowChild.Rows[0]["Name"];
                    person.LastName = (string)dataTableShowChild.Rows[0]["Last name"];
                    person.Birthplace = (string)dataTableShowChild.Rows[0]["Birthplace"];
                    person.CountryOfBirth = (string)dataTableShowChild.Rows[0]["Country of birth"];
                    person.Born = (int)dataTableShowChild.Rows[0]["Born"];
                    person.Mother = (string)dataTableShowChild.Rows[0]["Mother"];
                    person.Father = (string)dataTableShowChild.Rows[0]["Father"];
                    person.VitalStatus = (string)dataTableShowChild.Rows[0]["Vital status"];
                    person.Age = (string)dataTableShowChild.Rows[0]["Age"];

                    PrintChildren(person);
                }
                else
                {
                    Console.Clear();
                    Console.Write("\n\n\nUnfortunatley the person does not excist or has no children listed in the table! :(");

                    Console.Write("\n\n(Press to return...)");
                    Console.ReadKey();
                }
            }
            return person;
        }

        // COLUMN AGE: updates the column age with the persons age, alternatively 'R.I.P' if person has passed away.
        public static void UpdateColumnAge(string tableName)
        {
            SQLDatabase.database.ExecuteSQL(@$"UPDATE {tableName}
                                               SET Age = CASE 
                                               WHEN [Vital status] = 'Deceased'
                                               THEN 'R.I.P'
                                               ELSE CONVERT(varchar(30), 2021 - Born)
                                               END"
                                            );
        }

        // PRINT Person: prints full information of a person.
        public static void Print(Person person)
        {
            Console.Clear();
            Program.PrintMenuChoiceHeader(Program.menuChoice);
            Console.WriteLine("\n\n| ID | Name | Last name | Birthplace | Country of birth | Born | Mother | Father | Vital status | Age |");
            Console.WriteLine($"{person.Id} {person.Name} {person.LastName} {person.Birthplace} {person.CountryOfBirth} {person.Born} {person.Mother} {person.Father} {person.VitalStatus} {person.Age}");

            if (Program.menuChoice < 3)
            {
                Console.WriteLine("\n\nPress to return...");
                Console.ReadKey();
            }
        }

        // PRINT Children: prints full information of a child.
        public static void PrintChildren(Person person)
        {
            Console.Clear();
            Program.PrintMenuChoiceHeader(Program.menuChoice);
            Console.WriteLine("\n\n| ID | Name | Last name | Birthplace | Country of birth | Born | Mother | Father | Vital status | Age |");
            Console.WriteLine($"{person.Id} {person.Name} {person.LastName} {person.Birthplace} {person.CountryOfBirth} {person.Born} {person.Mother} {person.Father} {person.VitalStatus} {person.Age}");

            Console.WriteLine("\n\nPress to return...");
            Console.ReadKey();
        }

        //CLEAR: clears last line.
        public static void ClearLastLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

    }

}