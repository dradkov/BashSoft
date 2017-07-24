namespace BashSoft.IO.Commands
{
    using BashSoft.Contracts;
    using BashSoft.Contracts.DatabaseInterfaces;

    public abstract class Command : IExecutable
    {
        private string input;
        private string[] data;
        private IContentComparer judje;
        private IDatabase repository;
        private IDirectoryManager inputOutputmaneger;


        public Command(string input, string[] data, IContentComparer judje, IDatabase repository, IDirectoryManager inputOutputmaneger)
        {
            this.Input = input;
            this.Data = data;
            this.judje = judje;
            this.repository = repository;
            this.inputOutputmaneger = inputOutputmaneger;
                
        }


        protected string Input
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

        protected string[] Data
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
        protected IContentComparer Judge
        {
            get
            {
                return this.judje;
            }

        }
        protected IDatabase Repository
        {
            get
            {
                return this.repository;
            }

        }
        protected IDirectoryManager InputOutputmaneger
        {
            get
            {
                return this.inputOutputmaneger;
            }
        }


        public abstract void Execute();
    }
}
