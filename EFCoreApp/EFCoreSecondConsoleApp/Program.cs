﻿using EFCoreSecondConsoleApp.DataForFrontend;
using EFCoreSecondConsoleApp.Services;

namespace EFCoreSecondConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BookAndAutherData.ListAll();

            ExampleService exampleService = new ExampleService();
            exampleService.ExampleSaveEntry();

        }
    }
}