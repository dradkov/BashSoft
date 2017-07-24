namespace BashSoft
{
    using BashSoft.Contracts;
    using BashSoft.Contracts.DatabaseInterfaces;
    using BashSoft.Repository;
    using BashSoft.Testing;
    using IO;

    class StartUp
    {
        public static void Main(string[] args)
        {
            IContentComparer tester = new Tester();
            IDirectoryManager ioManeger = new IOManager();
            IDatabase repo = new StudentRepository(new RepositoryFilter(), new RepositorySorter());
            IInterpreter currentInterpretor = new CommandInterpreter(tester, repo, ioManeger);
            IReader reader = new InputReader(currentInterpretor);

            reader.StartReadingCommands();
        }
    }
}
