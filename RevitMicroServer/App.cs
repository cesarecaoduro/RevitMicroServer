#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using EmbedIO;
using EmbedIO.Actions;
using EmbedIO.WebApi;
using RevitMicroServer.Controllers;
using Swan.Logging;
#endregion

namespace RevitMicroServer
{
    class App : IExternalApplication
    {
        public WebServer _server { get; set; }
        public Result OnStartup(UIControlledApplication a)
        {
            var url = "http://localhost:9999/";
            using (_server = CreateWebServer(url))
            {
                // Once we've registered our modules and configured them, we call the RunAsync() method.
                _server.RunAsync();

                //var browser = new System.Diagnostics.Process()
                //{
                //    StartInfo = new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true }
                //};
                //browser.Start();
                // Wait for any key to be pressed before disposing of our web server.
                // In a service, we'd manage the lifecycle of our web server using
                // something like a BackgroundWorker or a ManualResetEvent.
                //Console.ReadKey(true);
            }
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            _server.Dispose();
            return Result.Succeeded;
        }

        // Create and configure our web server.
        private static WebServer CreateWebServer(string url)
        {
            var server = new WebServer(o => o
                    .WithUrlPrefix(url)
                    .WithMode(HttpListenerMode.Microsoft))
                // First, we will configure our web server by adding Modules.
                .WithLocalSessionManager()
                .WithWebApi("/api", m => m
                    .WithController<DummyController>())
                //.WithModule(new WebSocketRevitModule("/revit"))
                //.WithStaticFolder("/", HtmlRootPath, true, m => m
                //    .WithContentCaching(UseFileCache)) // Add static files after other modules to avoid conflicts
                .WithModule(new ActionModule("/", HttpVerbs.Any, ctx => ctx.SendDataAsync(new { Message = "Error" })));

            // Listen for state changes.
            server.StateChanged += (s, e) => $"WebServer New State - {e.NewState}".Info();

            return server;
        }


    }
}
