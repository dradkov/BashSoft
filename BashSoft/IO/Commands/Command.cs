namespace BashSoft.IO.Commands
{
    using BashSoft.Attributes;
    using BashSoft.Contracts;
    using BashSoft.Contracts.DatabaseInterfaces;

    public abstract class Command : IExecutable
    {
        private string input;
        private string[] data;
        private IContentComparer judje;
        private IDatabase repository;
        private IDirectoryManager inputOutputManeger;


        protected Command(string input, string[] data)
        {
            this.Input = input;
            this.Data = data;
                   
        }

        public string Input
        {
            get
            {
                return this.input;
            }
            private set
            {
                this.input = value;
            }

        }

        public string[] Data
        {
            get
            {
                return this.data;
            }
            private set
            {
                this.data = value;
            }

        }
        


        public abstract void Execute();
    }
}
