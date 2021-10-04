using Interface;

namespace InProcessImplementation
{
    public class Net48Implementation : IMyInterface
    {
        public Foo GetFoo(Bar bar)
        {
            return new Foo
            {
                Value1 = 42,
                Value2 = $"Foo {bar.Value}"
            };
        }
    }
}
