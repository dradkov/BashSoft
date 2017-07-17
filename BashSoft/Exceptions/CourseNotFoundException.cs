using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Exceptions
{
   public class CourseNotFoundException : Exception
    {
        private const string InvalidCourse = "The {0} Course Cannot be found";

        //public CourseNotFoundException(string message)
        //    :base (message)
        //{

        //}
        public CourseNotFoundException(string name)
           : base(string.Format(InvalidCourse,name))
        {

        }
    }
}
