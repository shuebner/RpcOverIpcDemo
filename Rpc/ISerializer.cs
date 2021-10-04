namespace Rpc
{
    public interface ISerializer
    {
        byte[] Serialize(object obj);

        object Deserialize(byte[] bytes);
    }
}
