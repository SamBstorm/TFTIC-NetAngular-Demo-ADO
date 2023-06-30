using Demo_ADO.Handlers;
using Demo_ADO.Models;
using System.Data.SqlClient;
using System.Data;

namespace Demo_ADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBSlide;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM student";
                    connection.Open();
                    int nbStudent = (int)command.ExecuteScalar();
                    Console.WriteLine($"Il y a {nbStudent} étudiants dans la DB.");
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT first_name, last_name FROM student";
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["first_name"]} {reader[1]}");
                        }
                    }
                }
            }

            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "StudentGetAll";
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(reader.ToStudent());
                        }
                    }
                }
            }

            Console.WriteLine(students.SingleOrDefault(s => s.Last_name == "Cruise").First_name);

            Student student_14 = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "StudentGet";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("id", 14);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) student_14 = reader.ToStudent();
                    }
                }
            }

            Console.WriteLine($"L'étudiant 14 est {student_14.First_name} {student_14.Last_name}");

            Student nouvelEtudiant = new Student()
            {
                Student_Id = 26,
                First_name = "Yves",
                Last_name = "Amisi",
                Birth_date = new DateTime(1986, 10, 28),
                Login = "ayves",
                Section_Id = 1010,
                Year_result = 20,
                Course_Id = "0"
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO student VALUES (@student_id, @first_name, @last_name, @birth_date, @login, @section_id, @year_result, @course_id)";
                    command.Parameters.AddWithValue("student_id", nouvelEtudiant.Student_Id);
                    command.Parameters.AddWithValue("first_name", nouvelEtudiant.First_name);
                    command.Parameters.AddWithValue("last_name", nouvelEtudiant.Last_name);
                    command.Parameters.AddWithValue("birth_date", nouvelEtudiant.Birth_date);
                    command.Parameters.AddWithValue("year_result", nouvelEtudiant.Year_result);
                    command.Parameters.AddWithValue("login", nouvelEtudiant.Login);
                    command.Parameters.AddWithValue("section_id", nouvelEtudiant.Section_Id);
                    command.Parameters.AddWithValue("course_id", nouvelEtudiant.Course_Id);
                    connection.Open();
                    int nbRow = command.ExecuteNonQuery();

                    if(nbRow > 0) Console.WriteLine("Bien inséré!");
                    else Console.WriteLine("Oups! Pas su insérer...");
                }
            }
        }
    }
}