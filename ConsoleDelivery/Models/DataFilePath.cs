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
        public static string? LogsValidationFile
        {
            get
            {
                return _logsValidationFile;
            }

            private set
            {
                _logsValidationFile = value ?? null;
            }
        }
        public static string? LogsOperationsFile { 
            get
            {
                return _logsOperationFile;
            }
            private set 
            {
                _logsOperationFile = value ?? null;
            } 
        }
        public static string? FiltredDataFile
        {
            get
            {
                return _filtredDataFile;
            }
            private set
            {
                _filtredDataFile = value ?? null; 
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

        public static string GetDefaultLogsValid()
        {
            string directoryPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string[] filePath = { directoryPath, "LogsFiles", "LogsValid.json" };
            return Path.Combine(filePath);
        }

        public static string GetDefaultLogsOperationsLog()
        {
            string directoryPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string[] filePath = { directoryPath, "LogsFiles", "OperationsLog.json" };
            return Path.Combine(filePath);
        }

        /// <summary>
        /// Устанавливает путь для файла с отфильтрованными данными
        /// </summary>
        public static void SetFilePathToFiltredData(string filePath)
        {
            string dataFiltredPath = string.Empty;
            while (!CheckPath(filePath, out dataFiltredPath))
            {
                Console.Write("Данный путь не является действительным. Пожалйуста, проверьте указанный путь и повторите попытку: ");
                filePath = Console.ReadLine()!;
            }
            _filtredDataFile = dataFiltredPath;
            Config.SetPathDataFiltredToConfig(dataFiltredPath);
        }

        public static void SetFilePathToValidationLogs(string filePath)
        {
            string dataValidationPath = string.Empty;
            while (!CheckPath(filePath, out dataValidationPath))
            {
                filePath = Console.ReadLine()!;
            }
            _logsValidationFile = dataValidationPath;
            Config.SetPathValidationLogToConfig(dataValidationPath);
        }

        public static void SetFilePathToOperationLogs(string filePath)
        {
            string dataOperationPath = string.Empty;
            while (!CheckPath(filePath, out dataOperationPath))
            {
                filePath = Console.ReadLine()!;
            }
            _logsOperationFile = dataOperationPath;
            Config.SetPathOperationLogToConfig(dataOperationPath);
        }


        public static bool CheckPath(string inputPath , out string path)
        {
            bool isExists;
            path = string.Empty;
            if (File.Exists(inputPath))
            {
                isExists = true;
                path = Path.GetFullPath(inputPath);
            }
            else
            {
                isExists = false;
            }
            return isExists;
        }
    }
}
