namespace Shuttle.Core.Container.Tests
{
    public class DoSomethingWithDependency : IDoSomethingWithDependency
    {
        public ISomeDependency SomeDependency { get; private set; }

        public DoSomethingWithDependency(ISomeDependency someDependency)
        {
            SomeDependency = someDependency;
        }
    }
}