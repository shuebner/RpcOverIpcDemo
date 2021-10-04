using Interface;

namespace Rpc.InterfaceStub
{
    public sealed class GetFooRequest : Request<GetFooRequest, GetFooResponse>
    {
        public Bar Bar { get; set; }
    }

    public sealed class GetFooResponse : Response<GetFooRequest, GetFooResponse>
    {
        public Foo Foo { get; set; }
    }
}
