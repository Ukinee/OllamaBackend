using Authorization.Services.Implementations;

namespace OllamaBackend;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder
            .Services
            .ConfigureServices(builder.Configuration)
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddControllers();
        
        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        WebSocketOptions webSocketOptions = new WebSocketOptions
        {
            KeepAliveInterval = TimeSpan.FromMinutes(2),
        };

        app.UseWebSockets(webSocketOptions);

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();

        // rabbitMq Queue
        // streaming context
        // json web token - authentication
        // tailwind / bootstrap - CDN

        //web socket - real time updating - rabbitMq
    }

    /*
        const socket = new WebSocket('wss://localhost:7286/api/ws');

        socket.onopen = function(event) {
          console.log('WebSocket is open now.');
          socket.send('Hello Server!');
        };

        socket.onmessage = function(event) {
          console.log('Received from server: ', event.data);
        };

        socket.onclose = function(event) {
          console.log('WebSocket is closed now.');
        };

        socket.onerror = function(error) {
          console.error('WebSocket error: ', error);
        };
    */
}
