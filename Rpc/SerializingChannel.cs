using System;

namespace Rpc
{
    public sealed class SerializingChannel : IRequestResponseChannel
    {
        readonly ISerializer _serializer;
        readonly IBinaryChannel _binaryChannel;

        public SerializingChannel(
            ISerializer serializer,
            IBinaryChannel binaryChannel)
        {
            _serializer = serializer;
            _binaryChannel = binaryChannel;
        }

        public TResponse GetResponse<TRequest, TResponse>(TRequest request)
            where TRequest : Request<TRequest, TResponse>
            where TResponse : Response<TRequest, TResponse>
        {
            var serializedRequest = _serializer.Serialize(request);

            var binaryResponse = _binaryChannel.WaitForResponse(serializedRequest);

            var response = _serializer.Deserialize(binaryResponse);
            if (response is TResponse expectedResponse)
            {
                return expectedResponse;
            }

            throw new InvalidOperationException($"expected response of type {typeof(TResponse).Name}, but received response of type {response.GetType().Name}");
        }
    }
}
