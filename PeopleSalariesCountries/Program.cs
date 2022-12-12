using PeopleSalariesCountries.InputService;
using System.Net;

internal class Program
{

    private static void Main(string[] args)
    {
        ConsoleInputService consoleInputService = ConsoleInputService.Instance;
        consoleInputService.OpenMenu();
    }
}