﻿using MongoDB.Bson;
using MongoDB.Driver;

namespace _01_MongoDB;

internal class Program {

    static async Task Main(string[] args) {

        // Конвертация в BSON и обратно
        // TestModel.ShowResult();
        TestModel.ShowSwapTypes();

        // Базовые концепции MongoDB
        // await _01_BASE.ShowResult();
    }
}