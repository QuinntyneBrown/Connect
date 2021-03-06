﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Connect.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace IntegrationTests
{
    public class ScenarioBase
    {
        protected TestServer CreateServer()
        {
            var webHostBuilder = new WebHostBuilder()
                    .UseStartup(typeof(Startup))
                    .UseKestrel()
                    .UseConfiguration(GetConfiguration())
                    .ConfigureAppConfiguration((builderContext, config) =>
                    {
                        config                        
                        .AddInMemoryCollection(new Dictionary<string, string>
                        {
                            { "isTest", "true"},
                            { "Authentication:MaximumUsers", "2" }
                        });
                    });
            
            var testServer = new IntegrationTestServer(webHostBuilder);

            testServer.SeedDatabase();

            return testServer;
        }
        
        protected HubConnection GetHubConnection(HttpMessageHandler httpMessageHandler) 
            => new HubConnectionBuilder()
                            .WithUrl("http://integrationtests/hub",(options) => {
                                options.Transports = HttpTransportType.ServerSentEvents;
                                options.HttpMessageHandlerFactory = h => httpMessageHandler;
                            })
                            .ConfigureLogging(logging => logging.AddConsole())
                            .Build();

        protected IConfiguration GetConfiguration() => new ConfigurationBuilder()
                .AddUserSecrets(typeof(Startup).GetTypeInfo().Assembly)                
                .Build();
    }
}
