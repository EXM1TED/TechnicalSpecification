﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models.Logs.LogsModels
{
    public enum TypeOfOperation
    {
        DeliveryIdInput = 1,
        DeliveryWeightInput = 2,
        DeliveryDateTimeInput = 3,
        DeliveryRegionNameInput = 4,
        DeliveryChooseAction = 5,
        ChooseOperation = 6,
        ChoosedSecondOperation = 7,
        SendNewRegionToDataBase = 8,
        SendNewDeliveryToDataBase = 9,
    }
}
