using System;
using SharedMemory;

namespace Rpc
{
    public sealed class MemoryMappedFileChannel : IBinaryChannel, IDisposable
    {
        readonly RpcBuffer _rpcBuffer;

        public MemoryMappedFileChannel(string fileName)
        {
            _rpcBuffer = new RpcBuffer(fileName);
        }

        public byte[] WaitForResponse(byte[] request)
        {
            var rpcResponse = _rpcBuffer.RemoteRequest(request);
            // TODO: can we improve error handling here?
            if (!rpcResponse.Success)
            {
                throw new InvalidOperationException("request failed");
            }

            return rpcResponse.Data;
        }

        public void Dispose()
        {
            _rpcBuffer.Dispose();
        }
    }
}
