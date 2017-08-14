namespace BashSoft.IO.Commands
{
    using BashSoft.Attributes;
    using BashSoft.Exceptions;
    using BashSoft.Network;

    [Alias("downloadAsynch")]
    public class DownloadAsyncRequest : Command
    {
        public DownloadAsyncRequest(string input, string[] data)
            : base(input, data)
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
