namespace Ivy.TestHelper
{
    public interface ITestInterface
    {
        int Integer { get; set; }
    }

    public class TestClass : ITestInterface
    {
        public int Integer { get; set; }
    }

    public interface ITestGenericInterface<T>
    {
        T Attribute { get; set; }
    }

    public class TestGenericClass<T> : ITestGenericInterface<T>
    {
        public T Attribute { get; set; }
    }

    public interface ITestComplexGenericInterface<T1, T2>
    {
        T1 Attribute1 { get; set; }
        T2 Attribute2 { get; set; }
    }

    public class TestComplexGenericClass<T1, T2> : ITestComplexGenericInterface<T1, T2>
    {
        public T1 Attribute1 { get; set; }
        public T2 Attribute2 { get; set; }
    }
}
