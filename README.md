# RpcOverIpcDemo
Demonstrates how to do RPC from a .NET 5 process to a .NET Framework 4.8 process.

**NOTE:**
Some crucial statements in the "Why" section turned out to be false.
Because they were the reason to even create this demo, I left them in for context.
More information on the confusion can be found in [this comment](https://github.com/grpc/grpc-dotnet/issues/1368#issuecomment-962822211).
Now, back to the original context in which the demo was created:

## Why
.NET remoting has long been deprecated in favor of WCF.
But, just like .NET remoting, WCF is no longer available in .NET Core.

Microsoft's recommendation to go forward was gRPC.
Unfortunately gRPC has decided to no longer support .NET Framework servers.
[People are upset](https://github.com/grpc/grpc-dotnet/issues/1368).

We are now missing a recommended default option for doing RPC from a .NET Core / .NET 5 process to a .NET Framework process.
Hence the demo of a relatively simple and unintrusive way to get transparent RPC through a regular .NET interface.

## Extent of the Demo

The Demo covers the simple use case of calling a service method and getting a result back.
It does not deal with exceptions or anything regarding the lifecycle of a remote object.

## Design

The in-process interfaces and classes are ignorant of the RPC chain around them, as they should be.

The RPC stubs are boilerplate and could be generated, just like the request/response pairs.
In principle we could generate the entire interface-specific RPC infrastructure (including a factory that hides the fact that RPC is even happening) from the interface definition alone (like gRPC does).
If I find the time, I may write a [SourceGenerator](https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview) for that as an exercise.

The demo uses [SharedMemory](https://github.com/spazzarama/SharedMemory) as transport and [MessagePack](https://msgpack.org/) as serialization format (via [neuecc's library](https://github.com/neuecc/MessagePack-CSharp)).
Both are hidden behind interfaces and can be easily replaced with other implementations, e. g. pipes as transport, ProtoBuf as serialization format.

The request/response pairs are independent of the serialization.
This works with MessagePack, because between two .NET processes the MessagePack library can use contractless serialization.

## See how it works

Debug through the test project to follow the data.
To debug the real process test on both ends, add a breakpoint in the test method line at which `DoStuffWithFoo` is called.
As soon as it is reached you can attach a separate Visual Studio instance to the Rpc.Server.exe process to debug the server process.

The RPC server is running as a simple `Main` method to show the general principle.
Running as an `IHostedService` may be more convenient in a production app.
