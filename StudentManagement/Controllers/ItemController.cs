using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        //static List<Student> students = new List<Student>();
        private SchoolContext itemContext;

        public ItemController(SchoolContext itemContext)
        {
            this.itemContext = itemContext;
            itemContext.Database.EnsureCreated();
        }

        // GET: api/values
        [HttpGet]
        public List<Student> Get()
        {
            return itemContext.Students.ToList();
        }

        // GET api/student/{id}
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            foreach (var student in itemContext.Students)
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


            itemContext.Students.Add(student);
            itemContext.SaveChanges();
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
            var studentFromDB = itemContext.Students.First(x => x.Id == student.Id);
            studentFromDB.Name = student.Name;
            studentFromDB.Age = student.Age;
            itemContext.SaveChanges();
            /*var localStudent = students.First(x => x.Id == student.Id);
            localStudent.Name = student.Name;*/
        }

        // DELETE api/student/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            itemContext.Students.Remove(itemContext.Students.First(x => x.Id == id));
            itemContext.SaveChanges();
        }
    }
}
