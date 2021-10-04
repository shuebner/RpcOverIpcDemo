namespace Interface
{
    public interface IMyInterface
    {
        Foo GetFoo(Bar bar);
    }

    public sealed class Foo
    {
        public int Value1 { get; set; }
        public string Value2 { get; set; }
    }

    public sealed class Bar
    {
        public string Value { get; set; }
    }
}
