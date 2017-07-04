namespace BashSoft.IO
{
    using System;
    using DataInfo;

    public class InputReader
    {
        private const string EndCommand = "quit";
        private CommandInterpreter interpreter;


        public InputReader(CommandInterpreter interpreter)
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