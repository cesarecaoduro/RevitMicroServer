#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using EmbedIO;
using EmbedIO.Actions;
using EmbedIO.WebApi;
using RevitMicroServer.Controllers;
using Swan.Logging;
#endregion

namespace RevitMicroServer
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        private WebServer _server;

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            
            // Access current selection

            return Result.Succeeded;
        }
        

    }
}
