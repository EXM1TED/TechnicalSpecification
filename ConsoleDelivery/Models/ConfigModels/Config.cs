using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDelivery.Models.ConfigModels;
using Newtonsoft.Json;

namespace ConsoleDelivery.Models.ConfigModels
{
    public delegate Task SetFilePath(ConfigArgs configArgs);
    public static class Config
    {
        public static event SetFilePath? SetPath;
        public static string? FiltredDataFile
        {
            get { return _filtredDataFile; }
            private set { _filtredDataFile = value ?? null; }
        }
        public static string? ValidationLogsFile
        {
            get { return _validationLogsFile; }
            private set { _validationLogsFile = value ?? null; }
        }
        public static string? OperationLogsFile
        {
            get { return _operationLogsFile; }
            private set { _operationLogsFile = value ?? null; }
        }
        public static ConfigArgs ConfigArgs { get; set; } = new();
        public static ConfigArgs? CurrentConfigInfo { get; set; } = new();

        private static string? _filtredDataFile { get; set; }
        private static string? _validationLogsFile { get; set; }
        private static string? _operationLogsFile { get; set; }

        public static void SetPathDataFiltredToConfig(string filePathDataFiltred)
        {
            _filtredDataFile = filePathDataFiltred;
            ConfigArgs.FiltredDataFile = filePathDataFiltred;
            SetPath += ConfigEditor.SetPathFile;
            SetPath?.Invoke(ConfigArgs);
        }

        public static void SetPathValidationLogToConfig(string filePathValidationLog)
        {
            _validationLogsFile = filePathValidationLog;
            ConfigArgs.ValidationLogsFile = filePathValidationLog;
            SetPath += ConfigEditor.SetPathFile;
            SetPath?.Invoke(ConfigArgs);
        }

        public static void SetPathOperationLogToConfig(string filePathOperationLog)
        {
            _operationLogsFile = filePathOperationLog;
            ConfigArgs.OperationLogsFile = filePathOperationLog;
            SetPath += ConfigEditor.SetPathFile;
            SetPath?.Invoke(ConfigArgs);
        }

        public static void GetCurrentConfigInfo()
        {
            string directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string[] path = { directory, "Config.json" };
            string fullPath = Path.Combine(path);
            using (StreamReader streamReader = new(fullPath))
            {
                 using (JsonTextReader reader = new(streamReader))
                {
                    JsonSerializer serializer = new();
                    CurrentConfigInfo = serializer.Deserialize<ConfigArgs>(reader);
                }
            }
        }
    }
}
