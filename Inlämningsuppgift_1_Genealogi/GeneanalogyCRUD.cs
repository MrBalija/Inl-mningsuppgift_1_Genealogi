using System;
using System.Collections.Generic;
using System.Text;

namespace Inlämningsuppgift_1_Genealogi
{
    class GeneanalogyCRUD
    {

        // CRUD --> "C" = Create:
        // CREATE: Creates an objekt Person
        public void Create(Person person)
        {
            //SQLDatabase.database.DatabaseName = DatabaseName;

            SQLDatabase.database.ExecuteSQL(@$"INSERT INTO People (Name, Last name, Birthplace, Country of birth, Born, Mother, Father, Vital status)
                                  VALUES ({person.Name}, {person.LastName}, {person.Birthplace}, {person.CountryOfBirth}, 
                                          {person.Born}, {person.Mother}, {person.Father}, {person.VitalStatus})"
                              );
        }

    }
}
