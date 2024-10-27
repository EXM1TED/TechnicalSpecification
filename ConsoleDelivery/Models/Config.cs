using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models
{
    public delegate Task SetFilePath(string filePath);
    public static class Config
    {
        public static event SetFilePath? SetPath;
        public static string? FiltredDataFile 
        { 
            get { return _filtredDataFile; } 
            private set { _filtredDataFile = value ?? null; } 
        }
        public static string? ValidationLogsFile 
        {   get { return _validationLogsFile; } 
            private set { _validationLogsFile = value ?? null;} 
        }
        public static string? OperationLogsFile 
        { 
            get { return _operationLogsFile; } 
            private set { _operationLogsFile = value ?? null; } 
        }
        public static Dictionary<string, string> ConfigInfo { get; set; } = new();

        private static string? _filtredDataFile { get; set; }
        private static string? _validationLogsFile { get; set; }
        private static string? _operationLogsFile { get; set; }

        public static void SetPathDataFiltredToConfig(string filePathDataFiltred)
        {
            _filtredDataFile = filePathDataFiltred;
            SetPath += ConfigEditor.SetPathFile;
            SetPath?.Invoke(filePathDataFiltred);
        }

        public static void SetPathValidationLogToConfig(string filePathValidationLog)
        {
            _filtredDataFile = filePathValidationLog;
            SetPath += ConfigEditor.SetPathFile;
            SetPath?.Invoke(filePathValidationLog);
        }

        public static void SetPathOperationLogToConfig(string filePathOperationLog)
        {
            _filtredDataFile = filePathOperationLog;
            SetPath += ConfigEditor.SetPathFile;
            SetPath?.Invoke(filePathOperationLog);
        }

        public static Dictionary<string, string> GetConfigInfo()
        {
            ConfigInfo.Add("FiltredDataFile", FiltredDataFile);
            ConfigInfo.Add("ValidationLogsFile", ValidationLogsFile);
            ConfigInfo.Add("OperationLogsFile", OperationLogsFile);
            return ConfigInfo;
        }
    }
}
