using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using EmbedIO;
using System;


namespace RevitMicroServer
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class StartServer : IExternalCommand
    {
        public WebServer WebServer { get; private set; }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var u = new Utils();
            WebServer = u.CreateWebServer(commandData);
            WebServer.RunAsync();
            return Result.Succeeded;
        }
    }
}
