using Interface;

namespace Rpc.InterfaceStub
{
    public sealed class MyInterfaceRpcServerStub : IRpcTarget
    {
        readonly IMyInterface _inner;

        public MyInterfaceRpcServerStub(IMyInterface inner)
        {
            _inner = inner;
        }

        object IRpcTarget.HandleRequest(ulong _, object request)
        {
            switch (request)
            {
                case GetFooRequest getFooRequest:
                    return CreateResponse(_inner.GetFoo(getFooRequest.Bar));

                default:
                    return HandleUnknownRequest(request);
            };
        }

        private GetFooResponse CreateResponse(Foo foo) =>
            new GetFooResponse
            {
                Foo = foo
            };

        UnknownRequestResponse HandleUnknownRequest(object request)
        {
            var requestTypeName = request.GetType().Name;

            return new UnknownRequestResponse
            {
                RequestTypeName = requestTypeName
            };
        }
    }
}
