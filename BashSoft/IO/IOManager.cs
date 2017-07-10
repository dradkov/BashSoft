﻿namespace BashSoft.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using DataInfo;

    public class IOManager
    {
        public void TraverseDirectory(int depth)
        {
            OutputWriter.WriteEmptyLine();
            int initialIdentation = SessionData.currentPath.Split('\\').Length;
            Queue<string> subfolders = new Queue<string>();
            subfolders.Enqueue(SessionData.currentPath);

            while (subfolders.Count != 0)
            {
                string currentFolder = subfolders.Dequeue();
                int identation = currentFolder.Split('\\').Length - initialIdentation;
                //check depth
                if (depth - identation < 0)
                {
                    break;
                }
                //display current folder
                OutputWriter.WriteMessageOnNewLine(string.Format("{0}{1}",
                    new string('-', identation), currentFolder));

                //display files in current folder
                try
                {
                    foreach (var file in Directory.GetFiles(currentFolder))
                    {
                        int indexOfLastSlash = file.LastIndexOf("\\");
                        string fileName = file.Substring(indexOfLastSlash);
                        OutputWriter.WriteMessageOnNewLine(
                            new string('-', indexOfLastSlash) + fileName);
                    }

                    //adding subfolders
                    foreach (string directoryPath in Directory.GetDirectories(currentFolder))
                    {
                        subfolders.Enqueue(directoryPath);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    OutputWriter.DisplayException(
                        ExceptionMessages.UnauthorizedAccessExceptionMessage);
                }
            }
        }

        public void CreateDirectoryInCurrentFolder(string name)
        {
            string path = SessionData.currentPath + "\\" + name;
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException(ExceptionMessages.ForbiddenSymbolsContainedInName);
            }
        }

        public void ChangeCurrentDirectoryRelative(string relativePath)
        {
            if (relativePath == "..")
            {
                try
                {
                    string currentPath = SessionData.currentPath;
                    int lastIndexOfSlash = currentPath.LastIndexOf("\\");
                    string newPath = currentPath.Substring(0, lastIndexOfSlash);
                    SessionData.currentPath = newPath;
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new ArgumentOutOfRangeException("indexOfLastSlash", ExceptionMessages.InvalidDestination);
                }
            }
            else
            {
                string currentPath = SessionData.currentPath;
                currentPath += "\\" + relativePath;
                ChangeCurrentDirectoryAbsolute(currentPath);
            }
        }

        public void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                throw new DirectoryNotFoundException(ExceptionMessages.InvalidPath);
                //OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
                //return;
            }

            SessionData.currentPath = absolutePath;
        }
    }

}