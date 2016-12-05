namespace IB.Framework.Core.IoC
{
    public interface IServiceLocator
    {
        IContainer Container { get; }

        void Init(IContainer container);

        void Reset();
    }
}
