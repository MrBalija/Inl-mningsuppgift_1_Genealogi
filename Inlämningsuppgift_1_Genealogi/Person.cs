using System;
using System.Collections.Generic;
using System.Text;

namespace Inlämningsuppgift_1_Genealogi
{
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BirthDay { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Mother { get; set; }
        public int Father { get; set; }
    }
}
