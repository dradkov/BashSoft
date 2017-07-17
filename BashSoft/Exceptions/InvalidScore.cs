using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Exceptions
{
   public  class InvalidScore : Exception
    {

        private const string InvalidLenght = "The Score must be Less than Tasks";

        public InvalidScore(string message)
            :base (message)
        {

        }
        public InvalidScore()
            :base(InvalidLenght)
        {

        }
    }
}
