namespace Shuttle.Core.Container
{
    public interface IComponentRegistryBootstrap
    {
        void Register(IComponentRegistry registry);
    }
}