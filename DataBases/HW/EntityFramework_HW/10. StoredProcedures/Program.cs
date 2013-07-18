﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Data;

class Program
{
    static void Main(string[] args)
    {
        using (NorthwindEntities context = new NorthwindEntities())
        {
            var totalIncome = GetTotalIncomes("Exotic Liquids", new DateTime(1970, 1, 1), new DateTime(2005, 1, 1));

            Console.WriteLine(totalIncome);
        }
    }

    static decimal? GetTotalIncomes(string supplierName, DateTime? startDate, DateTime? endDate)
    {
        using (NorthwindEntities northwindEntites = new NorthwindEntities())
        {
            var totalIncomeSet = northwindEntites
                .usp_GetTotalIncome(supplierName, startDate, endDate);

            foreach (var totalIncome in totalIncomeSet)
            {
                
                return totalIncome;
            }
        }

        return null;
    }
}