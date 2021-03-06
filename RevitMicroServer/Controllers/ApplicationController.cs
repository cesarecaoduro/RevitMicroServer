﻿using Autodesk.Revit.UI;
using Domain;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.Utilities;
using EmbedIO.WebApi;
using System.Threading.Tasks;

namespace RevitMicroServer.Controllers
{
    public sealed class ApplicationController : WebApiController
    {
        private UIApplication Application { get; }
        public ApplicationController(UIApplication application)
        {
            Application = application;
        }

        [Route(HttpVerbs.Get, "/application/document")]
        public Document GetActiveDocument()
        {
            return new Document
            {
                Title = Application.ActiveUIDocument.Document.Title,
                Path = Application.ActiveUIDocument.Document.PathName
            };
        }
    }
}
