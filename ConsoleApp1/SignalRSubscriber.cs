using Microsoft.AspNetCore.SignalR.Client;

namespace ConsoleApp1;

public class SignalRSubscriber
{
    private readonly HubConnection _connection;

    public SignalRSubscriber(string hubUrl)
    {
        _connection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .Build();
    }

    public async Task StartAsync()
    {
        _connection.On<string>("ReceiveNotification", HandleRequest);
        await _connection.StartAsync();
        Console.WriteLine("SignalR connection started.");
    }

    public async Task StopAsync()
    {
        await _connection.StopAsync();
        Console.WriteLine("SignalR connection stopped.");
    }

    private void HandleRequest(string notification)
    {
        Console.WriteLine("Received notification: " + notification);
    }
}