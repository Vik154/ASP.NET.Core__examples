using MongoDB.Bson;
using MongoDB.Driver;

namespace _01_MongoDB;

internal class Program {

    static async Task Main(string[] args) {

        // Базовые концепции MongoDB
        await _01_BASE.ShowResult();
    }
}