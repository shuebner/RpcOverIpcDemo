namespace Rpc
{
    public interface IRequestResponseChannel
    {
        TResponse GetResponse<TRequest, TResponse>(TRequest request)
            where TRequest : Request<TRequest, TResponse>
            where TResponse : Response<TRequest, TResponse>;
    }
}
