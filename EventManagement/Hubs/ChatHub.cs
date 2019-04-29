using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace EventManagement.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string nick, string message)
        {
            
            await Clients.All.SendAsync("Send", nick, message);
        }
    }
}
