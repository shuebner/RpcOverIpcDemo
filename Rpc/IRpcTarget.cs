namespace Rpc
{
    public interface IRpcTarget
    {
        object HandleRequest(ulong messageId, object request);
    }
}
