namespace Ivy.IoC.Core
{
    public interface IContainerInstaller
    {
        void Install(IContainerGenerator containerGenerator);
    }
}
