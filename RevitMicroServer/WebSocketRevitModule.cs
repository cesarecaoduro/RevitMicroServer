using EmbedIO.WebSockets;
using System.Threading.Tasks;

namespace RevitMicroServer
{
    internal class WebSocketRevitModule : WebSocketModule
    {
        private string v;

        public WebSocketRevitModule(string urlPath) : base(urlPath, true) {}

        protected override Task OnMessageReceivedAsync(IWebSocketContext context, byte[] buffer, IWebSocketReceiveResult result)
        {
            throw new System.NotImplementedException();
        }

        protected override Task OnClientConnectedAsync(IWebSocketContext context) => 
            Task.WhenAll(
                SendAsync(context, "Your are Connected!"),
                SendToOthersAsync(context, "Someone is connected to Revit Web Server."));

        protected override Task OnClientDisconnectedAsync(IWebSocketContext context)
            => SendToOthersAsync(context, "Someone has disconnected from the server.");

        private Task SendToOthersAsync(IWebSocketContext context, string payload)
            => BroadcastAsync(payload, c => c != context);
    }
}