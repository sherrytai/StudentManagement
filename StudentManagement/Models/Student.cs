using System;
namespace StudentManagement.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }
       
        public Student(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Student()
        {

        }
    }
}
