namespace Shuttle.Core.Container.Tests
{
    public class DoSomething : IDoSomething
    {
        public ISomeDependency SomeDependency {
            get { return null; }
        }
    }
}