using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ADO.Models
{
    internal class Student
    {
        public int Student_Id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public DateTime Birth_date { get; set; }
        public string Login { get; set; }
        public int Section_Id { get; set; }
        public int Year_result { get; set; }
        public string Course_Id { get; set; }
    }
}
