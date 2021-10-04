using Interface;

namespace InterfaceConsumer
{
    public class Consumer
    {
        private readonly IMyInterface myInterface;

        public Consumer(IMyInterface myInterface)
        {
            this.myInterface = myInterface;
        }

        public string DoStuffWithFoo(string arg)
        {
            var foo = myInterface.GetFoo(new Bar { Value = arg });

            return foo.Value2;
        }
    }
}
