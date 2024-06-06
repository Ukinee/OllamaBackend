using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Handshake.Controllers;

[ApiController, Route("api")]
[ApiExplorerSettings(IgnoreApi = true)]
public class HandshakeController : ControllerBase
{
    [Route("ws")]
    public async Task Get()
    {
        Console.WriteLine("BBBBBBbbbbbb");

        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            await Handle(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    private static async Task Handle(WebSocket webSocket)
    {
        byte[] buffer = new byte[1024 * 4];

        WebSocketReceiveResult receiveResult;

        do
        {
            receiveResult = await webSocket.ReceiveAsync
            (
                new ArraySegment<byte>(buffer),
                CancellationToken.None
            );
            
            string? response = TryGetResponse(buffer);
            
            if (response != null)
            {
                await webSocket.SendAsync
                (
                    new ArraySegment<byte>(Encoding.UTF8.GetBytes(response)),
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage,
                    CancellationToken.None
                );
            }
            
        } while (receiveResult.CloseStatus.HasValue == false);
        
        await webSocket.CloseAsync
        (
            receiveResult.CloseStatus.Value,
            receiveResult.CloseStatusDescription,
            CancellationToken.None
        );
    }

    private static string GetString(byte[] buffer)
    {
        return Encoding.UTF8.GetString(buffer.TakeWhile(x => x != 0).ToArray());
    }

    private static string? TryGetResponse(byte[] buffer)
    {
        string response = GetString(buffer);
        ClearBuffer(buffer);

        return response == "ping" ? "pong" : response;
    }

    private static void ClearBuffer(byte[] buffer)
    {
        for (int i = 0; i < buffer.Length; i++)
        {
            if(buffer[i] == 0)
                break;
            
            buffer[i] = 0;
        }
    }
}
