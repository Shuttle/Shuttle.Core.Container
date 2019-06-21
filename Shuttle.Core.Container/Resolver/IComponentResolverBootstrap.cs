namespace Shuttle.Core.Container
{
    public interface IComponentResolverBootstrap
    {
        void Resolve(IComponentResolver resolver);
    }
}