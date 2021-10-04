namespace Rpc
{
    public abstract class Request<TRequest, TResponse>
        where TResponse : Response<TRequest, TResponse>
        where TRequest : Request<TRequest, TResponse>
    {
        internal protected Request()
        {
        }
    }

    public abstract class Response<TRequest, TResponse>
        where TResponse : Response<TRequest, TResponse>
        where TRequest : Request<TRequest, TResponse>
    {
        internal protected Response()
        {
        }
    }
}
