using StudentManagement.Models;
using StudentManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Repositories
{
    public class BaseRepository
    {
        protected SchoolContext db;

        public BaseRepository(SchoolContext schoolContext)
        {
            db = schoolContext;
        }
    }
}
