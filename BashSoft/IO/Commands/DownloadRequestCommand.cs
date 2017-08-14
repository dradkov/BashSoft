namespace BashSoft.IO.Commands
{
    using BashSoft.Attributes;
    using BashSoft.Exceptions;
    using BashSoft.Network;

    [Alias("download")]
    public class DownloadRequestCommand : Command
    {
        public DownloadRequestCommand(string input, string[] data) : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
               
            }
            string url = this.Data[1];
            DownloadManager.Download(url);
        }
    }
}
