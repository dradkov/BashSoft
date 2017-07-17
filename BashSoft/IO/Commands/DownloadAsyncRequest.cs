using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashSoft.Repository;
using BashSoft.Testing;
using BashSoft.Network;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class DownloadAsyncRequest : Command
    {
        public DownloadAsyncRequest(string input, string[] data, Tester judje, StudentRepository repository, IOManager inputOutputmaneger) : base(input, data, judje, repository, inputOutputmaneger)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
                
            }

            string url = this.Data[1];
            DownloadManager.DownloadAsync(url);

        }
    }
}
