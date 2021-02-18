using System;
using System.Collections.Generic;
using System.Text;

namespace Inlämningsuppgift_1_Genealogi
{
    class Person
    {

        // PROPERTIES:
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Birthplace { get; set; }
        public string CountryOfBirth { get; set; }
        public string Born { get; set; }
        public int Mother { get; set; }
        public int Father { get; set; }
        public int VitalStatus { get; set; }

    }
}
