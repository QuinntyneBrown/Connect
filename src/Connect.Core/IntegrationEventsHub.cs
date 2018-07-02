using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Connect.Core
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IntegrationEventsHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            Connections._connections.Add(UserName, Context.ConnectionId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Connections._connections.Remove(UserName, Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }
        public string UserName => Context.User.Identity.Name;
    }
}
