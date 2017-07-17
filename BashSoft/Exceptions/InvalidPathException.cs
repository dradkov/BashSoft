using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Exceptions
{
    public class InvalidPathException : Exception
    {
        private const string InvalidPath = "The Source Does Not Exist";

        public InvalidPathException()
            : base(InvalidPath)
        {
        }

        public InvalidPathException(string message)
           : base(message)
        {

        }

    }
}
