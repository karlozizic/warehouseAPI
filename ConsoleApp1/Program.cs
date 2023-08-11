// See https://aka.ms/new-console-template for more information

using ConsoleApp1;

class Program
{
    static async Task Main(string[] args)
    {
        var signalRSubscriber = new SignalRSubscriber("http://localhost:54321/hub");
        await signalRSubscriber.StartAsync();

        Console.WriteLine("Press any key to stop...");
        Console.ReadKey();

        await signalRSubscriber.StopAsync();
    }
}