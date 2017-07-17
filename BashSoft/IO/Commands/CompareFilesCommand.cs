using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashSoft.Repository;
using BashSoft.Testing;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class CompareFilesCommand : Command
    {
        public CompareFilesCommand(string input, string[] data, Tester judje, StudentRepository repository, IOManager inputOutputmaneger) : base(input, data, judje, repository, inputOutputmaneger)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 3)
            {
                throw new InvalidCommandException(this.Input);
               
            }
            string firstPath = this.Data[1];
            string secondPath = this.Data[2];
            this.Judge.CompareContent(firstPath, secondPath);
        }
    }
}
