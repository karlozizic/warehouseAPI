using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Hubs;

public class NotificationHub : Hub
{
    public async Task SendNotification(string notification)
    {
        await Clients.All.SendAsync("ReceiveNotification", notification);
    }
}