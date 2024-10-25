using ConsoleDelivery.Models;
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
        private static LoggerValidation _loggerValidation { get; set; } = new();
        private static Validation _validation { get; set; } = new(_loggerValidation);
        private static LoggerOperation _loggerOperation { get; set; } = new();
        private static Operation _operation { get; set; } = new(_loggerOperation);

        public static void Main()
        {

            ShowActions();

            int choosedOperation;

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
                        _operation.SetAndLogOperation(new OperationArgs(TypeOfOperation.ChoosedSecondOperation,
                            $"Была выбрана операция {choosedAct}", null, null));
                        switch (choosedAct)
                        {
                            case 1:
                                AddRegion.CreateNewRegion();
                                break;
                            case 0:
                                break;
                        }
                    }
                    AddDelivery.CreateNewDelivery();
                    break;
                case 2:
                    FilterData.DataFilter();
                    break;
                default:
                    Console.WriteLine("Такой операции не существует");
                    break;
            }
            Console.WriteLine();
            Main();
        }
    }
}
