using System;
using Moq;
using ReusableLibrary.Abstractions.Caching;
using ReusableLibrary.Supplemental.Collections;

namespace Tickets.Tests.Infrastructure
{
    public abstract class AbstractKeyHelperTest<TInterface, TImpl>
        where TInterface : class
        where TImpl : TInterface
    {
        public void Run(params Action<Mock<ICache>>[] tests)
        {
            tests.ForEach(t =>
            {
                var cacheMock = new Mock<ICache>(MockBehavior.Loose);
                t(cacheMock);
                cacheMock.VerifyAll();
            });
        }

        public virtual TInterface CreateRepository(Mock<ICache> cacheMock)
        {
            return (TInterface)Activator.CreateInstance(typeof(TImpl),
                new Mock<TInterface>(MockBehavior.Loose).Object,
                cacheMock.Object);
        }

        public TInterface SetupRetrieve<TModel>(Mock<ICache> cacheMock, string key)
        {
            return CreateRepository(cacheMock.SetupCacheGet<TModel>(key));
        }

        public TInterface SetupRetrieveAndStore<TModel>(Mock<ICache> cacheMock, string key)
        {
            return CreateRepository(cacheMock.SetupCacheGet<TModel>(key, miss: true).SetupCacheStoreValidFor<TModel>(key));
        }

        public TInterface SetupRemove(Mock<ICache> cacheMock, string key)
        {
            return CreateRepository(cacheMock.SetupCacheRemove(key));
        }

        public TInterface SetupDependencyRemove(Mock<ICache> cacheMock, string key)
        {
            return CreateRepository(cacheMock.SetupCacheDependencyRemove(key));
        }

        public TInterface SetupDependencyAdd(Mock<ICache> cacheMock, string key)
        {
            return CreateRepository(cacheMock.SetupCacheDependencyAdd(key));
        }
    }
}
