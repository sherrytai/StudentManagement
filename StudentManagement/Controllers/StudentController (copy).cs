using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        //static List<Student> students = new List<Student>();
        private SchoolContext schoolContext;

        public StudentController(SchoolContext schoolContext)
        {
            this.schoolContext = schoolContext;
            schoolContext.Database.EnsureCreated();
        }

        // GET: api/values
        [HttpGet]
        public List<Student> Get()
        {
            return schoolContext.Students.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            foreach (var student in schoolContext.Students)
            {
                if (student.Id == id)
                    return student;
            }
            return null;
        }

        // POST api/values
        [HttpPost]
        public int AddStudent([FromBody]Student student)
        {
            //var student = new Student(students.Count, name);
           
            
            schoolContext.Students.Add(student);
            schoolContext.SaveChanges();
            return student.Id;

        }

        // PUT api/values
        [HttpPut]
        public void Put([FromBody]Student student)
        {
            /*foreach (var x in students)
            {
                if (x.Id == student.Id)
                    x.Name = student.Name;
            }*/
            schoolContext.Students.First(x => x.Id == student.Id).Name = student.Name;
            schoolContext.SaveChanges();
            /*var localStudent = students.First(x => x.Id == student.Id);
            localStudent.Name = student.Name;*/
        }

        // DELETE api/values
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            schoolContext.Students.Remove(schoolContext.Students.First(x => x.Id == id));
            schoolContext.SaveChanges();
        }
    }
}
