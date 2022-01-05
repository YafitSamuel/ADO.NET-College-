using College_sql.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;

namespace College_sql.Controllers.api
{
    public class StudentController : ApiController
    {

        //connection string point to the database (server name, database name, security policy)
        string connectionString = "Data Source=LAPTOP-A88V89UT;Initial Catalog=CollegeDB;Integrated Security=True;Pooling=False";

        // GET: api/Student
        public IHttpActionResult Get()
        {
            List<Student> allData = ShowAllData(connectionString);
            return Ok(new { allData });
        }

        // GET: api/Student/5
        public IHttpActionResult Get(int id)
        {
            List<Student> studens = ShowOneStudent(id, connectionString);
            return Ok(new { studens });
        }

        // POST: api/Student
        public IHttpActionResult Post([FromBody] Student student)
        {
            DateTime year = new DateTime(2012, 11, 4);
            Student student11 = new Student("yafit", "samuel", year, "rve@fer", 2001);
            int Add = AddNewStudent(connectionString, student11);
            return Ok(new { Add });
        }

        // PUT: api/Student/5
        public IHttpActionResult Put(int id, [FromBody] Student student)
        {
            DateTime year = new DateTime(2012, 11, 4);
            Student student12 = new Student("rona", "samuel", year, "rve@fer", 2001);
            int UpDate = UpDateStudent(id, connectionString, student12);
            return Ok(new { UpDate });

        }

        // DELETE: api/Student/5
        public IHttpActionResult Delete(int id)
        {

            int delete = DeleteStudent(connectionString, id);
            return Ok(new { delete });

        }





        // functions using the ADO.NET

        //retriving the data from STUDENT table, using SELECT

        public List<Student> ShowAllData(string conctionString)
        {
            List<Student> listOfStudent = new List<Student>();
            try
            {
                using (SqlConnection connection = new SqlConnection(conctionString))
                {
                    connection.Open();
                    string query = @"SELECT * FROM STUDENT";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader dataFromDataBase = command.ExecuteReader();

                    if (dataFromDataBase.HasRows)
                    {

                        while (dataFromDataBase.Read())
                        {
                            listOfStudent.Add(new Student(dataFromDataBase.GetString(1), dataFromDataBase.GetString(2), dataFromDataBase.GetDateTime(3), dataFromDataBase.GetString(4), dataFromDataBase.GetInt32(5)));
                        }
                        connection.Close();
                        return listOfStudent;
                    }
                    else
                    {
                        connection.Close();
                        return listOfStudent;
                    }
                }
            }
            catch (SqlException)
            {
                throw;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Student> ShowOneStudent(int id, string connectionString)
        {
            List<Student> listOfStudent = new List<Student>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"SELECT * FROM STUDENT WHERE ID ={id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader rows = command.ExecuteReader();

                    if (rows.HasRows)
                    {

                        while (rows.Read())
                        {
                            listOfStudent.Add(new Student(rows.GetString(1), rows.GetString(2), rows.GetDateTime(3), rows.GetString(4), rows.GetInt32(5)));


                        }
                    }
                    return listOfStudent;
                }

            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //  updating an existing entry in the STUDENT table, using UPDATE-SET
        public static int UpDateStudent(int id, string connectionString, Student student)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $@"UPDATE STUDENT 
                                SET firstName = '{student.firstName}', lastName='{student.lastName}', age={student.birthDay}, birthday='{student.email}', email='{student.yearCollege}'
            WHERE Id = {id}";
                SqlCommand command = new SqlCommand(query, connection);
                int rowsEffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsEffected;
            }
        }

        // deleting an existing entry in STUDENT table, using DELETE
        public static int DeleteStudent(string connectionString, int id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $@"DELETE FROM STUDENT
                                    WHERE Id = {id}";
                SqlCommand command = new SqlCommand(query, connection);
                int rowEffected = command.ExecuteNonQuery();
                connection.Close();
                return rowEffected;
            }
        }
        // adding new entry to the STUDENT table, using INSERT INTO
        public static int AddNewStudent(string connectionString, Student student)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $@"INSERT INTO STUDENT(firstName,lastName,birthday,email,yearCollege)
                                    VALUES('{student.firstName}','{student.lastName}',{student.birthDay},'{student.email}',{student.yearCollege})";
                SqlCommand command = new SqlCommand(query, connection);
                int rowsEffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsEffected;

            }
        }
    }


}
