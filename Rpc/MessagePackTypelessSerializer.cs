using MessagePack;

namespace Rpc
{
    public class MessagePackTypelessSerializer : ISerializer
    {
        public object Deserialize(byte[] bytes) => MessagePackSerializer.Typeless.Deserialize(bytes);
        public byte[] Serialize(object obj) => MessagePackSerializer.Typeless.Serialize(obj);
    }
}
