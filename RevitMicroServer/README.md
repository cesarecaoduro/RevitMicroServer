# Revit Micro Server

Give to Revit the ability to act as a REST server creatingend points in the application context.
file: ![RevitMicroServer](Resources/RevitMicroServer.gif)

## Architecture
The application uses [EmbedIO](https://github.com/unosquare/embedio) to start a tiny server that can answer to rest calls.

### How to

Create a server in the [Utils.cs](Utils/Utils.cs)

```
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
```

Define new controllers [ApplicationController.cs](Controllers/ApplicationController.cs)

```
public sealed class ApplicationController : WebApiController
{
    public ApplicationController(UIApplication application)
    {
        Application = application;
    }

    public UIApplication Application { get; }

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
```

And additional DTOs if needed in the <em>Domain</em> project

```
public class Document
{
    public string Title { get; set; }
    public string Path { get; set; }
}
```

