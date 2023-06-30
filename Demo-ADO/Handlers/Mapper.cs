using Demo_ADO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ADO.Handlers
{
    internal static class Mapper
    {
        public static Student ToStudent(this IDataRecord record) {
            if (record == null) throw new ArgumentNullException();
            return new Student() { 
                Student_Id = (int)record[nameof(Student.Student_Id)],
                First_name = (string)record[nameof(Student.First_name)],
                Last_name = (string)record[nameof(Student.Last_name)],
                Birth_date = (DateTime)record[nameof(Student.Birth_date)],
                Login = (string)record[nameof(Student.Login)],
                Section_Id = (int)record[nameof(Student.Section_Id)],
                Year_result = (int)record[nameof(Student.Year_result)],
                Course_Id = (string)record[nameof(Student.Course_Id)]
            };
        }
    }
}
