using NSubstitute;
using NUnit.Framework;
using Interface;
using Rpc.InterfaceStub;
using Rpc;
using InterfaceConsumer;

namespace RpcDemo
{
    public class ClientServerRpcIntegrationTests
    {
        IMyInterface _implementationMock;
        Consumer _consumer;

        [SetUp]
        public void SetUp()
        {
            _implementationMock = Substitute.For<IMyInterface>();
            var serverStub = new MyInterfaceRpcServerStub(_implementationMock);
            var serializer = new MessagePackTypelessSerializer();
            var channel = new SerializingChannel(
                serializer,
                new DirectCallChannel(
                    new RpcServer(
                        serializer,
                        serverStub,
                        () => { })));
            var clientStub = new MyInterfaceRpcClientStub(channel);

            _consumer = new Consumer(clientStub);
        }

        [Test]
        public void GetFoo_returns_rpc_response()
        {
            _implementationMock.GetFoo(Arg.Is<Bar>(b => b.Value == "testvalue")).Returns(new Foo { Value1 = 42, Value2 = "Foo testvalue" });

            var value = _consumer.DoStuffWithFoo("testvalue" );

            Assert.That(value, Is.EqualTo("Foo testvalue"));
        }
    }
}
