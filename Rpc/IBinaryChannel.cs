namespace Rpc
{
    public interface IBinaryChannel
    {
        byte[] WaitForResponse(byte[] request);
    }
}
