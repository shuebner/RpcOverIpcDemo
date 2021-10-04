using System;

namespace Rpc
{
    public sealed class RpcServer
    {
        readonly ISerializer _serializer;
        readonly IRpcTarget _target;
        readonly Action _onExit;

        public RpcServer(
            ISerializer serializer,
            IRpcTarget target,
            Action onExit)
        {
            _serializer = serializer;
            _target = target;
            _onExit = onExit;
        }

        public byte[] WaitForResponse(ulong messageId, byte[] binaryRequest)
        {
            var request = _serializer.Deserialize(binaryRequest);

            var response = request is ExitRequest exitRequest
                ? HandleExit(exitRequest)
                : _target.HandleRequest(messageId, request);

            var binaryResponse = _serializer.Serialize(response);
            return binaryResponse;

        }

        ExitResponse HandleExit(ExitRequest _)
        {
            _onExit();
            return new ExitResponse();
        }
    }
}
