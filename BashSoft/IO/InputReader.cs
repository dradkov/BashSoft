namespace BashSoft.IO
{
    using System;
    using DataInfo;
    using BashSoft.Contracts;

    public class InputReader : IReader
    {
        private const string EndCommand = "quit";
        private IInterpreter interpreter;


        public InputReader(IInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public void StartReadingCommands()
        {
            OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
            string input = Console.ReadLine();
            input = input.Trim();

            while (!input.Equals(EndCommand))
            {
               this.interpreter.InterpretCommand(input);
                OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
                input = Console.ReadLine();
                input = input.Trim();
            }
        }
    }
}