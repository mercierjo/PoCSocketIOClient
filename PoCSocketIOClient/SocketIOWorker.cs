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

            string url = "http://localhost:11004";
            _socketIO = new SocketIO(url);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _socketIO.ConnectAsync();
            while (!stoppingToken.IsCancellationRequested)
            {
                await _socketIO.EmitAsync("echo", res =>
                {
                    long ts = res.GetValue<long>();
                    _logger.LogInformation(ts.ToString());
                });
                await Task.Delay(5000);
            }
        }
    }
}
