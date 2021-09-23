﻿using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace SocketIOClient.Transport
{
    public class DefaultClientWebSocket : IClientWebSocket
    {
        public DefaultClientWebSocket()
        {
            _ws = new ClientWebSocket();
        }

        readonly ClientWebSocket _ws;

        public Action<object> ConfigOptions { get; set; }

        public WebSocketState State => _ws.State;

        public async Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
        {
            await _ws.CloseAsync(closeStatus, statusDescription, cancellationToken);
        }

        public async Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
        {
            ConfigOptions?.Invoke(_ws.Options);
            await _ws.ConnectAsync(uri, cancellationToken);
        }

        public async Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
        {
            return await _ws.ReceiveAsync(buffer, cancellationToken);
        }

        public async Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
        {
            await _ws.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
        }

        public void Dispose()
        {
            _ws.Dispose();
        }
    }
}
