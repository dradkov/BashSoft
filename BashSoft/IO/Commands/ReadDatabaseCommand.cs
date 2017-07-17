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
    public class ReadDatabaseCommand : Command
    {
        public ReadDatabaseCommand(string input, string[] data, Tester judje, StudentRepository repository, IOManager inputOutputmaneger) : base(input, data, judje, repository, inputOutputmaneger)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }
           
                
            string fileName = this.Data[1];
            this.Repository.LoadData(fileName);
        }
    }
}
