using ConsoleDelivery.Models;
using ConsoleDelivery.Models.ConfigModels;
using ConsoleDelivery.Models.Logs.LogsModels;
using ConsoleDelivery.Models.Logs.LogsModels.LogOperations;
using ConsoleDelivery.Models.Logs.LogsModels.LogValidations;
using ConsoleDelivery.Models.MainOperations.AddData;
using ConsoleDelivery.Models.MainOperations.FilterData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel.Resolution;
using System;

namespace ConsoleDelivery
{
    public class Programm
    {
        private static LoggerValidation _loggerValidation = new LoggerValidation();
        private static Validation _validation { get; set; } = new(_loggerValidation);
        private static LoggerOperation _loggerOperation = new LoggerOperation();
        private static Operation _operation { get; set; } = new(_loggerOperation);

        public static void Main()
        {
            ShowActions();

            int choosedOperation;

            Console.WriteLine();
            while (!int.TryParse(Console.ReadLine(), out choosedOperation))
            {
                Console.Write("Введите число: ");
                _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryChooseAction,
                    true, "При выборе операции, было введено не число"));
            }
            _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.ChooseOperation,
                false, "Операция была выбрана без ошибок"));

            _operation.SetAndLogOperation(new OperationArgs(TypeOfOperation.ChooseOperation,
                $"Была выбрана операция: {choosedOperation}", null, null));
            ChooseAction(choosedOperation);
        }

        /// <summary>
        /// Данный мето выводит на консоль список операций
        /// </summary>
        private static void ShowActions()
        {
            Console.WriteLine("1. Добавить новый заказ");
            Console.WriteLine("2. Отфильтровать данные");
            Console.WriteLine("3. Указать файл для файлов лоигирования/выгрузки данных (!Доступен только формат Json!)");
        }

        /// <summary>
        /// Данный метод предлягает пользователю операцию, которую он выбрал. Операция вводится натуральным числом
        /// </summary>
        /// <param name="choosedAction"></param>
        private static void ChooseAction(int choosedAction)
        {
            switch (choosedAction)
            {
                case 1:
                    if (Region.GetRegionsList().Count == 0)
                    {
                        Console.WriteLine("Спиоск регионов пустой, сначала нужно добавить новый регион.");
                        AddRegion.CreateNewRegion();
                    }
                    else
                    {
                        Console.Write("Если необходиом добавить новый регион, выбирете команду 1, иначе 0: ");
                        int choosedAct;
                        while (!int.TryParse(Console.ReadLine(), out choosedAct))
                        {
                            Console.Write("Введите число: ");
                        }
                        switch (choosedAct)
                        {
                            case 1:
                                AddRegion.CreateNewRegion();
                                _operation.SetAndLogOperation(new OperationArgs(TypeOfOperation.ChoosedSecondOperation,
                                    $"Была выбрана операция {choosedAct}", null, null));
                                break;
                            case 0:
                                _operation.SetAndLogOperation(new OperationArgs(TypeOfOperation.ChoosedSecondOperation,
                                    $"Была выбрана операция {choosedAct}", null, null));
                                break;
                            default:
                                Console.WriteLine("Такой операции не сущесвтует");
                                Console.WriteLine();
                                Main();
                                _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.ChoosedSecondOperation,
                                    true, $"В выборе нужно ли создать новый регион или нет, была выбрана операция: {choosedAct}"));
                                break;
                        }
                    }
                    AddDelivery.CreateNewDelivery();
                    break;
                case 2:
                    FilterData.DataFilter();
                    break;
                case 3:
                    LoadDataFiles();
                    break;
                default:
                    Console.WriteLine("Такой операции не существует");
                    Console.WriteLine();
                    break;
            }
            Console.WriteLine();
            Main();
        }

        private static void LoadDataFiles()
        {
            Console.WriteLine("Пример полного пути файла: C:\\Users\\chest\\OneDrive\\Рабочий стол\\Тестовое задание\\TechnicalSpecification\\ConsoleDelivery\\LogsFiles\\OperationsLog.json");
            int chooseAct;
            Console.WriteLine("1. Выбрать файл для логирования валидации");
            Console.WriteLine("2. Выбрать файл для логирования операций");
            Console.WriteLine("3. Выбрать файл для выгрузки отфильтрованных данных");
            while (!int.TryParse(Console.ReadLine(), out chooseAct))
            {
                Console.Write("Введите число");
            }
            switch (chooseAct)
            {
                case 1:
                    Console.Write("Укажите полный путь к файлу: ");
                    string fileValidationPath = Console.ReadLine() ?? string.Empty;
                    DataFilePath.SetFilePathToValidationLogs(fileValidationPath);
                    break;
                case 2:
                    Console.Write("Укажите полный путь к файлу: ");
                    string fileOperationPath = Console.ReadLine() ?? string.Empty;
                    DataFilePath.SetFilePathToOperationLogs(fileOperationPath);
                    break;
                case 3:
                    Console.Write("Укажите полный путь к файлу: ");
                    string fileDataFiltredPath = Console.ReadLine() ?? string.Empty;
                    DataFilePath.SetFilePathToFiltredData(fileDataFiltredPath);
                    break;
                default:
                    Console.WriteLine("Такой операции нет");
                    break;
            }
        }
    }
}
