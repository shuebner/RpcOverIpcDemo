using Interface;
using System;

namespace Rpc.InterfaceStub
{
    public sealed class MyInterfaceRpcClientStub : IMyInterface
    {
        readonly IRequestResponseChannel _channel;

        public MyInterfaceRpcClientStub(IRequestResponseChannel channel)
        {
            _channel = channel;
        }

        public Foo GetFoo(Bar bar) => _channel.GetResponse<GetFooRequest, GetFooResponse>(new GetFooRequest { Bar = bar }).Foo;
    }
}
