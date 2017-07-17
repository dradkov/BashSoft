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
    public class DropDatabaseCommand : Command
    {
        public DropDatabaseCommand(string input, string[] data, Tester judje, StudentRepository repository, IOManager inputOutputmaneger) : base(input, data, judje, repository, inputOutputmaneger)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 1)
            {
                throw new InvalidCommandException(this.Input);

            }
            this.Repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database Droped!");
        }
    }
}
