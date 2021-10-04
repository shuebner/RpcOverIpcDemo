using System;
using System.Diagnostics;
using Interface;
using InterfaceConsumer;
using NUnit.Framework;
using Rpc;
using Rpc.InterfaceStub;

namespace RpcDemo
{
    public class ClientServerRealProcessTests
    {
        // you need to manually build the server project and set the exe path
        // before executing the test
        const string RpcServerExePath = @"..\..\..\SharedMemoryIpcPoC\Rpc.Server\bin\Debug\net48\Rpc.Server.exe";

        [Test]
        public void FooValue_is_returned()
        {
            var fileName = $"RpcDemo123";
            using MemoryMappedFileChannel memoryChannel = new(fileName);
            var serializingChannel = new SerializingChannel(
                new MessagePackTypelessSerializer(),
                memoryChannel);
            var client = new MyInterfaceRpcClientStub(
                serializingChannel);

            var consumer = new Consumer(client);

            bool serverProcessHasExited = false;
            var serverProcess = new Process();
            serverProcess.StartInfo.FileName = RpcServerExePath;
            serverProcess.Exited += (_, __) => serverProcessHasExited = true;
            serverProcess.EnableRaisingEvents = true;
            serverProcess.StartInfo.Arguments = fileName;
            serverProcess.StartInfo.UseShellExecute = false;
            _ = serverProcess.Start();

            serverProcess.WaitForExit(1000);
            if (serverProcessHasExited)
            {
                throw new InvalidOperationException("there seems to be a problem at startup");
            }

            var value = consumer.DoStuffWithFoo("real process test" );
            Assert.That(value, Is.EqualTo("Foo real process test"));

            var exitResponse = serializingChannel.GetResponse<ExitRequest, ExitResponse>(new());
            Assert.That(exitResponse, Is.Not.Null);

            serverProcess.WaitForExit(500);

            if (!serverProcessHasExited)
            {
                serverProcess.Kill();
            }

            Assert.That(serverProcessHasExited, Is.True);
        }
    }
}