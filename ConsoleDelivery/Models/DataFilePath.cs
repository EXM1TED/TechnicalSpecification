using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models
{
    public static class DataFilePath
    {
        public static string? LogsValidationFile {
            get
            {
                return LogsValidationFile;
            }
            private set 
            {
                LogsValidationFile = _logsValidationFile ?? null;
            }
        }
        public static string? LogsOperationsFile { 
            get
            {
                return LogsOperationsFile;
            }
            private set 
            {
                LogsOperationsFile = _logsOperationFile ?? null;
            } 
        }
        public static string? FiltredDataFile
        {
            get
            {
                return FiltredDataFile;
            }
            private set
            {
                FiltredDataFile = _filtredDataFile ?? null; 
            }
        }

        private static string? _logsValidationFile { get; set; }
        private static string? _logsOperationFile { get; set; }
        private static string? _filtredDataFile { get; set; }

        /// <summary>
        /// Данны метод возвращает путь к файлу для отфильтрованных данных по умолчанию
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultFilterDataFile()
        {
            string directoryPath =  Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string[] filePath = {directoryPath, "LogsFiles", "FiltredData.json" };
            return Path.Combine(filePath);
        }

        /// <summary>
        /// Устанавливает путь для файла с отфильтрованными данными
        /// </summary>
        public static void SetFilePathToFiltredData(string filePath)
        {
            
        }


        public static bool CheckPath(string inputPath , out string path)
        {
            bool isTrouble;
            path = string.Empty;
            try
            {
                FileStream fileStream = new(inputPath,
                    FileMode.Open);
                path = inputPath;
                isTrouble = true;
            }
            catch
            {
                isTrouble = false;
            }
            return isTrouble;
        }
    }
}
