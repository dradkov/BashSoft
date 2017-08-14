namespace BashSoft.IO
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using BashSoft.Attributes;
    using BashSoft.Exceptions;
    using BashSoft.IO.Commands;
    using BashSoft.Contracts;
    using BashSoft.Contracts.DatabaseInterfaces;

    public class CommandInterpreter : IInterpreter
    {
        private IContentComparer judge;
        private IDatabase repository;
        [Inject]
        private IDirectoryManager inputOutputManager;

        public CommandInterpreter(IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;

        }

        public void InterpretCommand(string input)
        {
            string[] data = input.Split();
            string commandName = data[0].ToLower();

            try
            {
                IExecutable command = this.ParseCommand(input, commandName, data);
                command.Execute();
            }

            catch (DirectoryNotFoundException ex)
            {

                OutputWriter.DisplayException(ex.Message);
            }

        }

        private IExecutable ParseCommand(string input, string command, string[] data)
        {

            object[] parametersForConstructors = new object[] { input, data };

            Type typeOfCommand =
                Assembly.GetExecutingAssembly()
                .GetTypes()
                .First(type => type.GetCustomAttributes(typeof(AliasAttribute))
                .Where(atr => atr.Equals(command))
                .ToArray().Length > 0);

            Type typeOfInterpreter = typeof(CommandInterpreter);

            Command exe = (Command)Activator.CreateInstance(typeOfCommand, parametersForConstructors);
            
            FieldInfo[] fieldsOfCommands = 
                typeOfCommand.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            FieldInfo[] fieldsOfInterpreters =
                typeOfInterpreter.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);



            foreach (var fieldCommand in fieldsOfCommands)
            {
                Attribute atribute = fieldCommand.GetCustomAttribute(typeof(InjectAttribute));

                if (atribute != null)
                {
                    if (fieldsOfInterpreters.Any(x=>x.FieldType == fieldCommand.FieldType))
                    {
                        fieldCommand.SetValue(exe, fieldsOfInterpreters.First(x => x.FieldType == fieldCommand.FieldType).GetValue(this));
                    }
                }
            }

            return exe;
           
        }
    }
}