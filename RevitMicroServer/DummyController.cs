using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.Utilities;
using EmbedIO.WebApi;
using System.Threading.Tasks;

namespace RevitMicroServer.Controllers
{
    public sealed class DummyController : WebApiController
    {
        [Route(HttpVerbs.Get, "/dummy")]
        public string GetDummy() => "Ok dummy";
    }
}
