using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashSoft.Repository;
using BashSoft.Testing;
using BashSoft.Exceptions;
using System.Diagnostics;
using BashSoft.DataInfo;

namespace BashSoft.IO.Commands
{
    public class OpenFileCommand : Command
    {
        public OpenFileCommand(string input, string[] data, Tester judje, StudentRepository repository, IOManager inputOutputmaneger)
            : base(input, data, judje, repository, inputOutputmaneger)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }
           
            string fileName = this.Data[1];
            Process.Start(SessionData.currentPath + "\\" + fileName);
        }
    }
}
