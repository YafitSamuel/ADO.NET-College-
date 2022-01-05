using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace College_sql.Models
{
    public class Student
    {
        public string firstName;
        public string lastName;
        public DateTime birthDay;
        public string email;
        public int yearCollege;

        public Student(string firstName, string lastName, DateTime birthDay, string email, int yearCollege)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDay = birthDay;
            this.email = email;
            this.yearCollege = yearCollege;
        }
    }
}