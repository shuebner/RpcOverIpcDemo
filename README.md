# RpcOverIpcDemo
Demonstrates how to do RPC from a .NET 5 process to a .NET Framework 4.8 process.

The Demo covers the simple use case of calling a service method and getting a result back.
It does not deal with exceptions or anything regarding the lifecycle of a remote object.

The demo uses [SharedMemory](https://github.com/spazzarama/SharedMemory) as transport and [MessagePack](https://msgpack.org/) as serialization format (via [neuecc's library](https://github.com/neuecc/MessagePack-CSharp)).
Both are hidden behind interfaces and can be easily replaced with other implementations, e. g. pipes as transport, ProtoBuf as serialization format.

Debug through the test project to follow the data.
To debug the real process test on both ends, add a breakpoint in the test method line at which `DoStuffWithFoo` is called.
As soon as it is reached you can attach a separate Visual Studio instance to the Rpc.Server.exe process to debug the server process.
