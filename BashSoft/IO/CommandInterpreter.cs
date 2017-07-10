namespace BashSoft.IO
{
    using System.Diagnostics;
    using Network;
    using Repository;
    using Testing;
    using DataInfo;
    using System;
    using System.IO;

    public class CommandInterpreter
    {
        private Tester judge;
        private StudentRepository repository;
        private IOManager inputOutputManager;



        public CommandInterpreter(Tester judge, StudentRepository repository, IOManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;

        }

        public void InterpretCommand(string input)
        {
            string[] data = input.Split(' ');
            string command = data[0];

            try
            {
                this.ParseCommand(input, command, data);
            }
           
            catch (DirectoryNotFoundException dnfe)
            {

                OutputWriter.DisplayException(dnfe.Message);
            }
            try
            {
                this.ParseCommand(input, command, data);
            }
            catch (ArgumentOutOfRangeException aore)
            {

                OutputWriter.DisplayException(aore.Message);
            }
            try
            {
                this.ParseCommand(input, command, data);
            }
            catch (ArgumentException ae)
            {

                OutputWriter.DisplayException(ae.Message);
            }
            try
            {
                this.ParseCommand(input, command, data);
            }
            catch (Exception e)
            {

                OutputWriter.DisplayException(e.Message);
            }
        }

        private void ParseCommand(string input, string command, string[] data)
        {
            switch (command)
            {
                case "open":
                    TryOpenFile(input, data);
                    break;
                case "mkdir":
                    TryMakeDirectory(input, data);
                    break;
                case "Is":
                    TryTraverseFolders(input, data);
                    break;
                case "cmp":
                    TryCompareFiles(input, data);
                    break;
                case "cdRel":
                    TryChangePathRelative(input, data);
                    break;
                case "cdAbs":
                    TryChangePathAbsolute(input, data);
                    break;
                case "readDB":
                    TryReadDatabaseFromFile(input, data);
                    break;
                case "help":
                    TryGetHelp(input, data);
                    break;
                case "filter":
                    TryFilterAndTake(input, data);
                    break;
                case "order":
                    TryOrderAndTake(input, data);
                    break;
                case "decOrder"://TODO
                    break;
                case "download":
                    TryDownloadRequestedFile(input, data);
                    break;
                case "downloadAsynch":
                    TryDownloadRequestedFileAsync(input, data);
                    break;
                case "show":
                    TryShowWantedData(input, data);
                    break;
                default:
                    DisplayInvalidCommandMessage(input);
                    break;
            }
        }

        private void DisplayInvalidCommandMessage(string input)
        {
            OutputWriter.DisplayException($"The command '{input}' is invalid");
        }

        private void TryDropDb(string input, string[] data)
        {
            if (data.Length != 1)
            {
                this.DisplayInvalidCommandMessage(input);
                return;
            }
            this.repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database Droped!");
        }
        private void TryOpenFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string fileName = data[1];
                Process.Start(SessionData.currentPath + "\\" + fileName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryMakeDirectory(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string folderName = data[1];
                this.inputOutputManager.CreateDirectoryInCurrentFolder(folderName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryTraverseFolders(string input, string[] data)
        {
            if (data.Length == 1 || data.Length == 2)
            {
                if (data.Length == 1)
                {
                    this.inputOutputManager.TraverseDirectory(0);
                }
                else if (data.Length == 2)
                {
                    int depth;
                    bool hasParsed = int.TryParse(data[1], out depth);
                    if (hasParsed)
                    {
                        this.inputOutputManager.TraverseDirectory(depth);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                    }
                }
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryCompareFiles(string input, string[] data)
        {
            if (data.Length == 3)
            {
                string firstPath = data[1];
                string secondPath = data[2];
                this.judge.CompareContent(firstPath, secondPath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryChangePathRelative(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string relativePath = data[1];
                this.inputOutputManager.ChangeCurrentDirectoryRelative(relativePath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryChangePathAbsolute(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string absolutePath = data[1];
                this.inputOutputManager.ChangeCurrentDirectoryAbsolute(absolutePath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryReadDatabaseFromFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string fileName = data[1];
                this.repository.LoadData(fileName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryGetHelp(string input, string[] data)
        {
            if (data.Length == 1)
            {
                OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "make directory - mkdir: path "));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "traverse directory - ls: depth "));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "comparing files - cmp: path1 path2"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - changeDirREl:relative path"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - changeDir:absolute path"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "read students data base - readDb: path"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file - download: path of file (saved in current directory)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "get help – help"));
                OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
                OutputWriter.WriteEmptyLine();
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryFilterAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                string courseName = data[1];
                string filter = data[2].ToLower();
                string takeCommand = data[3].ToLower();
                string takeQuantity = data[4].ToLower();
                TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity,
            string courseName, string filter)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.repository.FilterAndTake(courseName, filter);
                }
                else
                {
                    int studentsToTake;
                    bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        this.repository.FilterAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeCommandParameter);
            }
        }

        private void TryOrderAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                string courseName = data[1];
                string comparison = data[2].ToLower();
                string takeCommand = data[3].ToLower();
                string takeQuantity = data[4].ToLower();
                TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, comparison);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity,
            string courseName, string comparison)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.repository.OrderAndTake(courseName, comparison);
                }
                else
                {
                    int studentsToTake;
                    bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        this.repository.OrderAndTake(courseName, comparison, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeCommandParameter);
            }
        }

        private void TryDownloadRequestedFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string url = data[1];
                DownloadManager.Download(url);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryDownloadRequestedFileAsync(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string url = data[1];
                DownloadManager.DownloadAsync(url);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryShowWantedData(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string courseName = data[1];
                this.repository.GetStudentsFromCourse(courseName);
            }
            else if (data.Length == 3)
            {
                string courseName = data[1];
                string userName = data[2];
                this.repository.GetStudentScoresFromCourse(courseName, userName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }
    }
}