using Autodesk.Revit.UI;
using EmbedIO;
using EmbedIO.Actions;
using RevitMicroServer.Controllers;
using RevitMicroServer.Properties;
using Swan.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitMicroServer
{
    class Utils
    {
        public WebServer CreateWebServer(ExternalCommandData commandData)
        {
            var url = string.Format("{0}:{1}", Settings.Default.BASEURL, Settings.Default.PORT);

            var server = new WebServer(o => o
                    .WithUrlPrefix(url)
                    .WithMode(HttpListenerMode.Microsoft))
                // First, we will configure our web server by adding Modules.
                .WithLocalSessionManager()
                .WithWebApi("/api", m => m
                    .RegisterController(() => new ApplicationController(commandData.Application))
                )
                .WithModule(new ActionModule("/", HttpVerbs.Any, ctx => ctx.SendDataAsync(new { Message = "Error" })));
            // Listen for state changes.
            server.StateChanged += (s, e) => $"WebServer New State - {e.NewState}".Info();

            return server;
        }
    }
}
