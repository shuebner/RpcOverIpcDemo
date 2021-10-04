using Rpc;

namespace RpcDemo
{
    class DirectCallChannel : IBinaryChannel
    {
        readonly RpcServer _rpcServer;

        public DirectCallChannel(RpcServer rpcServer)
        {
            _rpcServer = rpcServer;
        }

        public byte[] WaitForResponse(byte[] request) => _rpcServer.WaitForResponse(42, request);
    }
}
