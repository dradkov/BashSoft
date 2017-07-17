﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Exceptions
{
   public  class InvalidCommandException : Exception
    {
        private const string InvalidCommand = "The command {0} is invalid";


        //public InvalidCommandException(string message)
        //    : base (message)
        //{

        //}
        public InvalidCommandException(string input)
            :base(string.Format(InvalidCommand,input))
        {

        }
    }
}
