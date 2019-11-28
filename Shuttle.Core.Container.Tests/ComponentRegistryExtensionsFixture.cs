using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;
using NUnit.Framework;

namespace Shuttle.Core.Container.Tests
{
    [TestFixture]
    public class ComponentRegistryExtensionsFixture
    {
        public class Registry : ComponentRegistry
        {
        }

        [Test]
        public void Should_be_able_to_attempt_register_as_default_singleton()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Setup(m => m.IsRegistered(typeof(ISomeDependency))).Returns(false);

            mock.Object.AttemptRegister<ISomeDependency, SomeDependency>();

            mock.Setup(m => m.IsRegistered(typeof(ISomeDependency))).Returns(true);

            mock.Object.AttemptRegister<ISomeDependency, SomeDependency>();

            mock.Verify(m => m.IsRegistered(It.IsAny<Type>()), Times.Exactly(2));
            mock.Verify(m => m.Register(typeof(ISomeDependency), typeof(SomeDependency), Lifestyle.Singleton),
                Times.Once);
        }

        [Test]
        public void Should_be_able_to_attempt_register_as_singleton()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Setup(m => m.IsRegistered(typeof(ISomeDependency))).Returns(false);

            mock.Object.AttemptRegister<ISomeDependency, SomeDependency>(Lifestyle.Singleton);

            mock.Setup(m => m.IsRegistered(typeof(ISomeDependency))).Returns(true);

            mock.Object.AttemptRegister<ISomeDependency, SomeDependency>(Lifestyle.Singleton);

            mock.Verify(m => m.IsRegistered(It.IsAny<Type>()), Times.Exactly(2));
            mock.Verify(m => m.Register(typeof(ISomeDependency), typeof(SomeDependency), Lifestyle.Singleton),
                Times.Once);
        }

        [Test]
        public void Should_be_able_to_attempt_register_as_transient()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Setup(m => m.IsRegistered(typeof(ISomeDependency))).Returns(false);

            mock.Object.AttemptRegister<ISomeDependency, SomeDependency>(Lifestyle.Transient);

            mock.Setup(m => m.IsRegistered(typeof(ISomeDependency))).Returns(true);

            mock.Object.AttemptRegister<ISomeDependency, SomeDependency>(Lifestyle.Transient);

            mock.Verify(m => m.IsRegistered(It.IsAny<Type>()), Times.Exactly(2));
            mock.Verify(m => m.Register(typeof(ISomeDependency), typeof(SomeDependency), Lifestyle.Transient),
                Times.Once);
        }

        [Test]
        public void Should_be_able_to_attempt_register_implementation_as_default_singleton()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Setup(m => m.IsRegistered(typeof(SomeDependency))).Returns(false);

            mock.Object.AttemptRegister<SomeDependency>();

            mock.Setup(m => m.IsRegistered(typeof(SomeDependency))).Returns(true);

            mock.Object.AttemptRegister<SomeDependency>();

            mock.Verify(m => m.IsRegistered(It.IsAny<Type>()), Times.Exactly(2));
            mock.Verify(m => m.Register(typeof(SomeDependency), typeof(SomeDependency), Lifestyle.Singleton),
                Times.Once);
        }

        [Test]
        public void Should_be_able_to_attempt_register_implementation_as_singleton()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Setup(m => m.IsRegistered(typeof(SomeDependency))).Returns(false);

            mock.Object.AttemptRegister<SomeDependency>(Lifestyle.Singleton);

            mock.Setup(m => m.IsRegistered(typeof(SomeDependency))).Returns(true);

            mock.Object.AttemptRegister<SomeDependency>(Lifestyle.Singleton);

            mock.Verify(m => m.IsRegistered(It.IsAny<Type>()), Times.Exactly(2));
            mock.Verify(m => m.Register(typeof(SomeDependency), typeof(SomeDependency), Lifestyle.Singleton),
                Times.Once);
        }

        [Test]
        public void Should_be_able_to_attempt_register_implementation_as_transient()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Setup(m => m.IsRegistered(typeof(SomeDependency))).Returns(false);

            mock.Object.AttemptRegister<SomeDependency>(Lifestyle.Transient);

            mock.Setup(m => m.IsRegistered(typeof(SomeDependency))).Returns(true);

            mock.Object.AttemptRegister<SomeDependency>(Lifestyle.Transient);

            mock.Verify(m => m.IsRegistered(It.IsAny<Type>()), Times.Exactly(2));
            mock.Verify(m => m.Register(typeof(SomeDependency), typeof(SomeDependency), Lifestyle.Transient),
                Times.Once);
        }

        [Test]
        public void Should_be_able_to_attempt_register_instance()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Setup(m => m.IsRegistered(typeof(ISomeDependency))).Returns(false);

            mock.Object.AttemptRegisterInstance<ISomeDependency>(new SomeDependency());

            mock.Setup(m => m.IsRegistered(typeof(ISomeDependency))).Returns(true);

            mock.Object.AttemptRegisterInstance<ISomeDependency>(new SomeDependency());

            mock.Verify(m => m.IsRegistered(It.IsAny<Type>()), Times.Exactly(2));
            mock.Verify(m => m.RegisterInstance(typeof(ISomeDependency), It.IsAny<SomeDependency>()), Times.Once);
        }

        [Test]
        public void Should_be_able_to_register_as_default_singleton()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Object.Register<ISomeDependency, SomeDependency>();
            mock.Object.Register<ISomeDependency, SomeDependency>();

            mock.Verify(m => m.IsRegistered(It.IsAny<Type>()), Times.Never);
            mock.Verify(m => m.Register(typeof(ISomeDependency), typeof(SomeDependency), Lifestyle.Singleton),
                Times.Exactly(2));
        }

        [Test]
        public void Should_be_able_to_register_as_singleton()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Object.Register<ISomeDependency, SomeDependency>(Lifestyle.Singleton);
            mock.Object.Register<ISomeDependency, SomeDependency>(Lifestyle.Singleton);

            mock.Verify(m => m.IsRegistered(It.IsAny<Type>()), Times.Never);
            mock.Verify(m => m.Register(typeof(ISomeDependency), typeof(SomeDependency), Lifestyle.Singleton),
                Times.Exactly(2));
        }

        [Test]
        public void Should_be_able_to_register_as_transient()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Object.Register<ISomeDependency, SomeDependency>(Lifestyle.Transient);
            mock.Object.Register<ISomeDependency, SomeDependency>(Lifestyle.Transient);

            mock.Verify(m => m.IsRegistered(It.IsAny<Type>()), Times.Never);
            mock.Verify(m => m.Register(typeof(ISomeDependency), typeof(SomeDependency), Lifestyle.Transient),
                Times.Exactly(2));
        }

        [Test]
        public void Should_be_able_to_register_implementation_as_default_singleton()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Object.Register<SomeDependency>();
            mock.Object.Register<SomeDependency>();

            mock.Verify(m => m.IsRegistered(It.IsAny<Type>()), Times.Never);
            mock.Verify(m => m.Register(typeof(SomeDependency), typeof(SomeDependency), Lifestyle.Singleton),
                Times.Exactly(2));
        }

        [Test]
        public void Should_be_able_to_register_implementation_as_singleton()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Object.Register<SomeDependency>(Lifestyle.Singleton);
            mock.Object.Register<SomeDependency>(Lifestyle.Singleton);

            mock.Verify(m => m.IsRegistered(It.IsAny<Type>()), Times.Never);
            mock.Verify(m => m.Register(typeof(SomeDependency), typeof(SomeDependency), Lifestyle.Singleton),
                Times.Exactly(2));
        }

        [Test]
        public void Should_be_able_to_register_implementation_as_transient()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Object.Register<SomeDependency>(Lifestyle.Transient);
            mock.Object.Register<SomeDependency>(Lifestyle.Transient);

            mock.Verify(m => m.IsRegistered(It.IsAny<Type>()), Times.Never);
            mock.Verify(m => m.Register(typeof(SomeDependency), typeof(SomeDependency), Lifestyle.Transient),
                Times.Exactly(2));
        }

        [Test]
        public void Should_be_able_to_register_instance()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Object.RegisterInstance<ISomeDependency>(new SomeDependency());
            mock.Object.RegisterInstance<ISomeDependency>(new SomeDependency());

            mock.Verify(m => m.IsRegistered(It.IsAny<Type>()), Times.Never);
            mock.Verify(m => m.RegisterInstance(typeof(ISomeDependency), It.IsAny<SomeDependency>()), Times.Exactly(2));
        }

        [Test]
        public void Should_be_able_to_use_register_all()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Object.RegisterAll(Assembly.GetExecutingAssembly());

            mock.Verify(m => m.RegisterCollection(It.IsAny<Type>(), It.IsAny<IEnumerable<Type>>(), Lifestyle.Singleton),
                Times.Exactly(1));
            mock.Verify(m => m.Register(typeof(IDoSomething), typeof(DoSomething), Lifestyle.Singleton),
                Times.Exactly(1));
            mock.Verify(
                m => m.Register(typeof(IDoSomethingWithDependency), typeof(DoSomethingWithDependency),
                    Lifestyle.Singleton), Times.Exactly(1));
        }

        [Test]
        public void Should_be_able_to_use_register_suffixed_collection()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Object.RegisterSuffixed(Assembly.GetExecutingAssembly(),
                new[] {"CollectionItem", "CollectionItemA", "CollectionItemB"});

            mock.Verify(m => m.RegisterCollection(It.IsAny<Type>(), It.IsAny<IEnumerable<Type>>(), Lifestyle.Singleton),
                Times.Exactly(1));
            mock.Verify(m => m.Register(typeof(IDoSomething), typeof(DoSomething), Lifestyle.Singleton), Times.Never);
            mock.Verify(
                m => m.Register(typeof(IDoSomethingWithDependency), typeof(DoSomethingWithDependency),
                    Lifestyle.Singleton), Times.Never);
        }

        [Test]
        public void Should_be_able_to_use_register_suffixed_singleton()
        {
            var mock = new Mock<IComponentRegistry>();

            mock.Object.RegisterSuffixed(Assembly.GetExecutingAssembly(),
                new[] {"DoSomething"});

            mock.Verify(m => m.RegisterCollection(It.IsAny<Type>(), It.IsAny<IEnumerable<Type>>(), Lifestyle.Singleton),
                Times.Never);
            mock.Verify(m => m.Register(typeof(IDoSomething), typeof(DoSomething), Lifestyle.Singleton),
                Times.Exactly(1));
            mock.Verify(
                m => m.Register(typeof(IDoSomethingWithDependency), typeof(DoSomethingWithDependency),
                    Lifestyle.Singleton), Times.Never);
        }
    }
}