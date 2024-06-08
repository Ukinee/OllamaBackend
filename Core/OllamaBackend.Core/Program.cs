using Microsoft.OpenApi.Models;

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
            .AddControllers();

        AddSwaggerGen(builder);

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
        // signal er = web socket for chats
        // json web token - authentication
        // tailwind / bootstrap - CDN

        //web socket - real time updating - rabbitMq
    }

    private static void AddSwaggerGen(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen
        (
            option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });

                option.AddSecurityDefinition
                (
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    }
                );

                option.AddSecurityRequirement
                (
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    }
                );
            }
        );
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