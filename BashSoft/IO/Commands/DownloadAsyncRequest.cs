namespace BashSoft.IO.Commands
{

    using BashSoft.Exceptions;
    using BashSoft.Contracts;
    using BashSoft.Network;
    using BashSoft.Contracts.DatabaseInterfaces;

    public class DownloadAsyncRequest : Command
    {
        public DownloadAsyncRequest(string input, string[] data, IContentComparer judje, IDatabase repository, IDirectoryManager inputOutputmaneger) : base(input, data, judje, repository, inputOutputmaneger)
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
