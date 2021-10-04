using InProcessImplementation;
using Rpc;
using Rpc.InterfaceStub;
using SharedMemory;
using System.Threading.Tasks;

namespace Rpc.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            string memoryMappedFileName = args[0];

            var myInterfaceRpcTarget = new MyInterfaceRpcServerStub(
                new Net48Implementation());

            ISerializer serializer = new MessagePackTypelessSerializer();

            var tcs = new TaskCompletionSource<object>();

            var rpcServer = new RpcServer(
                new MessagePackTypelessSerializer(),
                myInterfaceRpcTarget,
                () => tcs.SetResult(null));

            // creates new listening thread internally and returns after opening the memory mapped file
            using (var rpcBuffer = new RpcBuffer(
                memoryMappedFileName,
                rpcServer.WaitForResponse))
            {
                tcs.Task.Wait();

                // leave a little time for the exit response to go through
                // before disposing the buffer
                Task.Delay(100).Wait();
            }
        }
    }
}
