namespace BashSoft
{
    using BashSoft.Repository;
    using BashSoft.Testing;
    using IO;
    class StartUp
    {
        public static void Main(string[] args)
        {
            var tester = new Tester();
            var ioManeger = new IOManager();
            var repo = new StudentRepository(new RepositoryFilter(), new RepositorySorter());
            var currentInterpretor = new CommandInterpreter(tester, repo, ioManeger);
            var reader = new InputReader(currentInterpretor);

            reader.StartReadingCommands();
        }
    }
}
