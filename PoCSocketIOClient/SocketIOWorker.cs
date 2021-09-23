using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SocketIOClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PoCSocketIOClient
{
    public class SocketIOWorker : BackgroundService
    {
        private readonly ILogger<SocketIOWorker> _logger;
        private readonly SocketIO _socketIO;

        public SocketIOWorker(ILogger<SocketIOWorker> logger)
        {
            _logger = logger;

            string url = "http://server:port";
            _socketIO = new SocketIO(url);

            _socketIO.On("ping", async res =>
            {
                string message = "a personal message";
                await _socketIO.EmitAsync("pong", message);
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _socketIO.ConnectAsync();
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}
