using Microsoft.AspNetCore.SignalR.Client;

namespace ConsoleApp1;

public class SignalRSubscriber
{
    private readonly HubConnection _connection;

    private readonly string _myAccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjb250ZW50IjoiU2VJKzl4c3VWcFNUTGpqc25Gc2g0TVJGRlN0WTVnY29vRTQweEkwRTdmWmQ2anJUYXBKbHN6Z2hRS2o4bzNYd2c5U2xVZUI4Mzk2MFcyZXZtMitYWC8xZzl2Q3NuczJ4WkNRZithSnEzc2pvbFJJazVmL1FTVEFoQ0VZUy9UVjJNVkZ3UHRxTXhPUkRtMW1PcGZaTXpQWWtJQzJYYlRjMXdwb1ZLSnpuanR2cUg3TlZna0dnaHUxNWo2WnR0cTJLZFJvSnZmOVZZUHAybzRZR21mRXloR0srZGZaNDE2bllzN0lRUjYyUXdmZWhkWE1wTWN5U3FndnVoOTEvS0hJS3FoUThLQ3Q0Vk1PSlN3VW5qK1dLaXZ2LzRXMGg4RHkxcHRZVkI4b3ZtbVZpZ2NxNi9TS21RUWtxa0V3OXdOMDFqZCtFNFlWYWlMOVlrYzVrZ0VGQ1VvT09tUXA5NUk2MTlVZVZEaFBPT0s3ZEtTU0MzK2RCUWhIVk1QcHZGckx1V3BFY2FaNFY4RGhFZW01b0Q2MVNURGZVc0owb2ZDSC9mTk9pUDhmbC83Q21VWlVQckZuTWpjd1k5QVh3MU5wOTh1eHhiR1ppUDY0WEt4S3VUYjczYjdkSVhmbGdFOUtqL0JXNXA3S2xIMnlUY1hhWnlxQnp4eFVtWDB2M3hvdDE4dk1YTmhVaHN3OFVqUXdpUyswc1lmbHJHcGE3RWVaY0d5MkkxanB2QWlXWE5wTkZpSHBGcnlHWThNKzNpU1RMVnhaaERCRTlQN3A1d1Rka2VYdVZkM285dWhKTE5TT1ZnM1JzUU85MU9nQXV1NlhFQ212cys3NXVWb1Bjc3NaNUlvclJJbEVIUU9sdlUxem1sUi96VG9VS2lBZk5qQmMxM3k0UXByMzQ4d0ZXc0p4NVJYd0d5RjJXdUVETW5iVm5VR0tVWTlocjhRbDdLWE92eW43MENvYmRLaG1hSm4wanpPb0hSQzhCcWk1Z2xzSmprTXFiVEs0MkJ5bUtVblpSN3cyZ1JvamlTczN6MXlwOHl4V2lmcE5GUGp3U21ZdDdZRTZOTGJ6NE84ako1OUhRZkRVdkZ0ZmlUaHE5R3puZUxWOWhHZ1UwZmJFT0RsUnRCOSs3MkRnU3h6dkZ3Yi81RVlDM0pCc3pyNjZnWFRhMzBQVFdmNVFya25RYmZ6TVNqZVByWFFpNzNLK2RWNW9YSENiSFR2bGNBQnU4bXJYV2V5dVQxSmNrcjhBTVBQek9jaXVnRWoyUnQyTFRBR3kyMGNhNXo3eXZSUTFBNVdoOFVpQXVEeEtkK2t5VExUVXBEaEMwWUJETUdiSFFZbVZMd29pMU9vdE1sSmpPTmVEWmlraGMzNGJ5ZDhyT014SzRNcnUzdm9id0xya1UwN0kxeGZGNjhSR1Q2c3haNnl2Y0YyUkk2US9tNmZRZE9aTThxMVBKaWpTWTl0QjZuYmJoQWl4Ym0xenYrWUt5MDd0cm9FaHJYUElndDlTSncrdmQvNXhwbDlEYktiemNyYnh1TnhJNDMrSC9OMWJDY1YrSkZ5MGVSOVdMWVdZd2R5TjN1dUNVMGJUVmFDaG9YemM0bE1pczRZam5jSUhiMksxTXRUTVdmOWhJN080NkEyaVliQ1ltNjFUTkl1WmREOFpscDkzQmE0d3lwbXpxUVk0MW9jejdDVTc5aG9WbjQvZ2siLCJ0aWQiOiI2NzlhNWFjOS1kNWRhLTRjOWUtOWRkMi1kY2YyZWIwN2NkYmMiLCJpZCI6Ijk4ZmRhZGI2LTA5YWItNDFiZC1hZDBiLTgyZTJmNmQzN2ZjNCIsImV4cCI6MTY4OTc1NDQwOCwiaWF0IjoxNjg5NzUyNjAzLCJpc3MiOiJodHRwczovL2F1dGhvcml6YXRpb24uc3RhZ2Utdm9sY2Fuby5jb20ifQ.xejeolWI88P01ko2vaNckPguN0KiMCXVa82xEkcuE2s";

    public SignalRSubscriber(string hubUrl)
    {
        _connection = new HubConnectionBuilder()
            .WithUrl(hubUrl, options =>
            { 
                options.AccessTokenProvider = () => Task.FromResult(_myAccessToken);
            })
            .Build();
    }

    public async Task StartAsync()
    {
        try
        {
            _connection.On<string>("ReceiveNotification", HandleRequest);
            await _connection.StartAsync();
            Console.WriteLine("SignalR connection started.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("SignalR connection failed: " + ex.Message);
        }
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